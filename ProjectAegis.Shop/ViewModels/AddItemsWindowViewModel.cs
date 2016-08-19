namespace ProjectAegis.Shop.ViewModels
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Collections;

    using Caliburn.Micro;

    using Microsoft.Win32;

    using Models;
    using Models.Messages;

    using Virtualization;

    using Localization.Managers;

    using Shared.Extensions;

    public sealed class AddItemsWindowViewModel : Screen, IHandle<BindableCollection<Category>>
    {
        #region IoC

        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        #endregion

        #region Private Members

        private Elements _elements;
        private ElementsList _selectedList;
        private BindableCollection<Category> _categories;

        #endregion


        #region Lists

        public BindableCollection<ElementsList> Lists => _elements;
        public ElementsList SelectedList
        {
            get { return _selectedList; }
            set
            {
                _selectedList = value;
                NotifyOfPropertyChange(nameof(Elements));
            }
        }

        #endregion

        #region Elements

        public VirtualizingCollection<Element> Elements => _selectedList == null ? null : new VirtualizingCollection<Element>(_selectedList);


        #endregion

        public AddItemsWindowViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            DisplayName = TranslationManager.Instance.Translate("AddItemsWindowHeaderText").ToString();

            _eventAggregator = eventAggregator;
            _windowManager = windowManager;

            _eventAggregator.Subscribe(this);
        }

        public void Load()
        {
            var dialog = new OpenFileDialog()
            {
                FileName = "elements.data",
                Filter = "gshop.data|*.data|All files|*.*"
            };

            var result = dialog.ShowDialog();
            if (result != null && !(bool)result)
                return;

            try
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                using (var reader = new BinaryReader(stream))
                {
                    _elements = reader.ReadModel<Elements>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            NotifyOfPropertyChange(nameof(Lists));
        }
        public void AddItemsToCategory(IList items)
        {
            var itemsAsElements = items.Cast<Element>();

            var instance = IoC.Get<SelectCategoryWindowViewModel>();
            _eventAggregator.PublishOnUIThread(_categories);

            var result = _windowManager.ShowDialog(instance);

            if(result != null && !result.Value)
                return;

            _eventAggregator.PublishOnUIThread(
                new AddItemsMessage()
                {
                    Category = instance.SelectedCategory,
                    SubCategory = instance.SelectedSubCategory,
                    Items = itemsAsElements.Select(x => x.Id)
                }
            );
        }

        #region Implementation of IHandle<BindableCollection<Category>>

        public void Handle(BindableCollection<Category> categories)
        {
            _categories = categories;
            
            _eventAggregator.Unsubscribe(this);
        }

        #endregion
    }
}