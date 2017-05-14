namespace ProjectAegis.Shop.ViewModels
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Reflection;
    using System.Collections;
    using System.Globalization;
    using System.Collections.Generic;

    using Caliburn.Micro;
    using Localization.Library.Managers;
    using Microsoft.Win32;

    using Models;
    using Models.Base;
    using Models.Messages;
    using Providers.Interfaces;
    using Shared.Library.Extensions;

    public sealed class MainWindowViewModel : 
        Screen, IHandle<AddItemsMessage>
    {
        #region Private Members

        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IItemInformationProvider _itemInformationProvider;

        private Models.Shop _shop;

        private Category _selectedCategory;
        private SubCategory _selectedSubCategory;

        private BindableCollection<SubCategory> _subCategories;

        private BindableCollection<Item> _items;
        private Item _selectedItem;
        private Price _selectedPrice;
        private CultureInfo _currentLanguage;

        #endregion

        public string BuildVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public int Version { get; set; }
        public FileType FileType { get; set; }

        #region Language

        public IEnumerable<CultureInfo> Languages => TranslationManager.Instance.Languages;
        public CultureInfo CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                TranslationManager.Instance.CurrentLanguage = value;
                _currentLanguage = value;

                NotifyOfPropertyChange();
            }
        }

        #endregion

        #region SectionsAvailability

        public bool AddItemsAvalaibility => _shop != null && _shop.Categories?.Count > 0;

        public bool Version144Sections => Version >= 144;
        public bool Version152Sections => Version >= 152;
        public bool Version153Sections => Version >= 153;

        #endregion


        #region Categories

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                SubCategories = value?.SubCategories;

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(SubCategories));
                NotifyOfPropertyChange(nameof(SelectedCategoryName));
            }
        }
        public BindableCollection<Category> Categories
        {
            get { return _shop?.Categories; }
            set
            {
                _shop.Categories = value;
                NotifyOfPropertyChange();
            }
        }
        public string SelectedCategoryName
        {
            get { return _selectedCategory?.Name; }
            set
            {
                _selectedCategory.Name = value;
                NotifyOfPropertyChange();
            }
        }
        public int SelectedCategoryIndex => Categories.IndexOf(SelectedCategory);

        #endregion

        #region SubCategories

        public SubCategory SelectedSubCategory
        {
            get { return _selectedSubCategory; }
            set
            {
                _selectedSubCategory = value;

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(Items));
                NotifyOfPropertyChange(nameof(SelectedSubCategoryName));
            }
        }
        public BindableCollection<SubCategory> SubCategories
        {
            get { return _subCategories; }
            set
            {
                _subCategories = value;
                NotifyOfPropertyChange();
            }
        }
        public string SelectedSubCategoryName
        {
            get { return _selectedSubCategory?.Name; }
            set
            {
                _selectedSubCategory.Name = value;
                NotifyOfPropertyChange();
            }
        }
        public int SelectedSubCategoryIndex => SubCategories.IndexOf(SelectedSubCategory);

        public void AddSubCategory()
        {
            if (_subCategories.Count < 8)
                _subCategories.Add(new SubCategory());

            NotifyOfPropertyChange(nameof(SubCategories));
        }
        public void RemoveSubCategories(IList list)
        {

            var itemsToDelete = new List<Item>();
            var subCategoriesToDelete = list.Cast<SubCategory>().ToList();

            foreach (var subCategory in subCategoriesToDelete)
            {
                itemsToDelete.AddRange(
                    _shop.Items.Where(
                        x =>
                            x.CategoryId == SelectedCategoryIndex &&
                            x.SubCategoryId == SubCategories.IndexOf(subCategory)
                        )
                    );
            }

            _shop.Items.RemoveRange(itemsToDelete);

            #region MoveItems

            var index = -1;

            for (int i = 0; i < _shop.Items.Count; i++)
            {
                if (_shop.Items[i].CategoryId == SelectedCategoryIndex)
                {
                    index++;

                    int subCat = _shop.Items[i].SubCategoryId;
                    int j = i;

                    for (; j < _shop.Items.Count; j++)
                    {
                        if (subCat == _shop.Items[j].SubCategoryId)
                        {
                            while (_shop.Items[j].SubCategoryId > index)
                            {
                                _shop.Items[j].SubCategoryId--;
                            }
                        }
                        else
                            break;
                    }

                    i = j - 1;
                }
            }

            #endregion

            _subCategories.RemoveRange(subCategoriesToDelete);

            NotifyOfPropertyChange(nameof(SubCategories));
            NotifyOfPropertyChange(nameof(Items));
        }

        #endregion


        #region Items

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(Prices));
                NotifyOfPropertyChange(nameof(OwnerNpcs));
            }
        }
        public BindableCollection<Item> Items
        {
            get
            {
                if (_shop != null)
                {
                    _items = _shop.Items.Where(
                        x =>
                            x.CategoryId == Categories.IndexOf(SelectedCategory) &&
                            x.SubCategoryId == SubCategories.IndexOf(SelectedSubCategory)
                        ).ToBindableCollection();

                }
                return _items;
            }
            set { _items = value; }
        }

        public void AddItem()
        {
            _shop.Items.Add(new Item()
            {
                Id = _shop.Items.Max(x => x.Id) + 1,
                CategoryId = SelectedCategoryIndex,
                SubCategoryId = SelectedSubCategoryIndex
            });

            NotifyOfPropertyChange(nameof(Items));
        }
        public void RemoveItems(IList list)
        {
            _shop.Items.RemoveRange(list.Cast<Item>());

            NotifyOfPropertyChange(nameof(Items));
        }

        #endregion

        #region Prices

        public Price SelectedPrice
        {
            get { return _selectedPrice; }
            set
            {
                _selectedPrice = value;
                NotifyOfPropertyChange();
            }
        }
        public BindableCollection<Price> Prices
        {
            get { return _selectedItem?.Prices; }
            set
            {
                _selectedItem.Prices = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        #region OwnerNpcs

        public BindableCollection<int> OwnerNpcs
        {
            get { return _selectedItem?.OwnerNpcs; }
            set
            {
                _selectedItem.OwnerNpcs = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        #region ItemInformation

        public IEnumerable<string> InformationSources => _itemInformationProvider.GetAvailableProviders();

        public string SelectedInformationSource { get; set; }

        public void GetItemInformation()
        {
            if (SelectedItem != null && !string.IsNullOrEmpty(SelectedInformationSource))
            {
                var name = _itemInformationProvider.GetName(SelectedItem.ItemId, SelectedInformationSource);
                if (!string.IsNullOrEmpty(name))
                    SelectedItem.Name = name;

                var descriprion = _itemInformationProvider.GetDescription(SelectedItem.ItemId, SelectedInformationSource);
                if (!string.IsNullOrEmpty(descriprion))
                    SelectedItem.Description = descriprion;
            }
        }

        #endregion

        public MainWindowViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, IItemInformationProvider itemInformationProvider)
        {
            base.DisplayName = TranslationManager.Instance.Translate("MainWindowHeaderText").ToString();

            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _itemInformationProvider = itemInformationProvider;

            NotifyOfPropertyChange(nameof(InformationSources));

            _eventAggregator.Subscribe(this);
                   
            _currentLanguage = TranslationManager.Instance.CurrentLanguage;
           
            NotifyOfPropertyChange(nameof(CurrentLanguage));
            NotifyOfPropertyChange(nameof(Languages));
            NotifyOfPropertyChange(nameof(BuildVersion));
        }

        public void Load(int version, FileType fileType)
        {
            var dialog = new OpenFileDialog()
            {
                FileName = "gshop.data",
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
                    _shop = reader.ReadModelWithParameters<Models.Shop>(version, fileType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            NotifyOfPropertyChange(nameof(Categories));
            NotifyOfPropertyChange(nameof(SubCategories));
            NotifyOfPropertyChange(nameof(Items));

            Version = version;
            FileType = fileType;

            NotifyOfPropertyChange(nameof(AddItemsAvalaibility));

            NotifyOfPropertyChange(nameof(Version144Sections));
            NotifyOfPropertyChange(nameof(Version152Sections));
            NotifyOfPropertyChange(nameof(Version153Sections));
        }

        public void Save()
        {
            var dialog = new SaveFileDialog()
            {
                FileName = "gshop.data",
                Filter = "gshop.data|*.data|All files|*.*"
            };

            var result = dialog.ShowDialog();
            if (result != null && !(bool)result)
                return;

            try
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                using (var reader = new BinaryWriter(stream))
                {
                    reader.WriteModelWithParameters(_shop, Version, FileType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Save(int version, FileType fileType)
        {
            var dialog = new SaveFileDialog()
            {
                FileName = "gshop.data",
                Filter = "gshop.data|*.data|All files|*.*"
            };

            var result = dialog.ShowDialog();
            if (result != null && !(bool)result)
                return;

            try
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                using (var reader = new BinaryWriter(stream))
                {
                    reader.WriteModelWithParameters(_shop, version, fileType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Unload()
        {
            _shop = new Models.Shop();

            NotifyOfPropertyChange(nameof(Categories));
            NotifyOfPropertyChange(nameof(SubCategories));
            NotifyOfPropertyChange(nameof(Items));

            Version = -1;
            FileType = FileType.Client;

            NotifyOfPropertyChange(nameof(AddItemsAvalaibility));

            NotifyOfPropertyChange(nameof(Version144Sections));
            NotifyOfPropertyChange(nameof(Version152Sections));
            NotifyOfPropertyChange(nameof(Version153Sections));
        }

        public void AddItems()
        {
            var intance = IoC.Get<AddItemsWindowViewModel>();
            _eventAggregator.PublishOnUIThread(Categories);

            _windowManager.ShowDialog(intance, null);
        }

        public void Exit()
        {
            base.TryClose();
        }

        #region Implementation of IHandle<AddItemsMessage>

        public void Handle(AddItemsMessage message)
        {
            foreach (var item in message.Items)
            {
                item.Id = _shop.Items.Max(x => x.Id) + 1;
                _shop.Items.Add(item);
            }

            NotifyOfPropertyChange(nameof(Items));
        }

        #endregion
    }
}