namespace ProjectAegis.Shop.ViewModels
{
    using Caliburn.Micro;

    using Models;

    using Localization.Managers;

    public class SelectCategoryWindowViewModel :
        Screen, IHandle<BindableCollection<Category>>
    {
        #region Private Members

        private readonly IEventAggregator _eventAggregator;
        private BindableCollection<Category> _categories;
        private Category _selectedCategory;
        private SubCategory _selectedSubCategory;

        #endregion


        #region Categories

        public BindableCollection<Category> Categories => _categories;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(nameof(SubCategories));
            }
        }

        #endregion

        #region SubCategories

        public BindableCollection<SubCategory> SubCategories => _selectedCategory?.SubCategories;
        public SubCategory SelectedSubCategory
        {
            get { return _selectedSubCategory; }
            set
            {
                _selectedSubCategory = value;
                NotifyOfPropertyChange(nameof(CanAddItemsToCategory));
            }
        }

        #endregion


        public SelectCategoryWindowViewModel(IEventAggregator eventAggregator)
        {
            this.DisplayName = TranslationManager.Instance.Translate("SelectCategoryHeaderText").ToString();

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void AddItemsToCategory()
        {
            TryClose(true);
        }
        public bool CanAddItemsToCategory => SelectedCategory != null && SelectedSubCategory != null;

        #region Implementation of IHandle<BindableCollection<Category>>

        public void Handle(BindableCollection<Category> categories)
        {
            _categories = categories;
            NotifyOfPropertyChange(nameof(Categories));
        }

        #endregion
    }
}
