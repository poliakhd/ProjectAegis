using System;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using Microsoft.Win32;
using ProjectAegis.Shared.Extensions;
using ProjectAegis.Shop.Models;

namespace ProjectAegis.Shop.ViewModels
{
    public sealed class MainWindowViewModel : Screen
    {
        #region Private Members

        private Models.Shop _shop;

        private Category _selectedCategory;
        private SubCategory _selectedSubCategory;

        private BindableCollection<SubCategory> _subCategories;

        private BindableCollection<Item> _items;
        private Item _selectedItem;
        private Price _selectedPrice;

        #endregion

        public MainWindowViewModel()
        {
            base.DisplayName = "shopeditor";
        }

        public void Load(int version)
        {
            var dialog = new OpenFileDialog()
            {
                FileName = "gshop.data",
                Filter = "gshop.data|*.data|All files|*.*"
            };

            var result = dialog.ShowDialog();
            if (result != null && !(bool) result)
                return;

            try
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                using (var reader = new BinaryReader(stream))
                {
                    _shop = reader.ReadModel<Models.Shop>(version);
                }
            }
            catch (Exception ex)
            {
                
            }

            NotifyOfPropertyChange(()=>Categories);
            NotifyOfPropertyChange(()=>SubCategories);
            NotifyOfPropertyChange(()=>Items);
        }
        public void Save()
        {

        }
        public void Exit()
        {
            base.TryClose();
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                SubCategories = value?.SubCategories;

                NotifyOfPropertyChange();
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

        public SubCategory SelectedSubCategory
        {
            get { return _selectedSubCategory; }
            set
            {
                _selectedSubCategory = value;
                _items = _shop.Items.Where(
                    x =>
                        x.CategoryId == Categories.IndexOf(SelectedCategory) &&
                        x.SubCategoryId == SubCategories.IndexOf(SelectedSubCategory)
                    ).ToBindableCollection();

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

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value; 
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(Prices));
            }
        }
        public BindableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

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

        public string SelectedCategoryName
        {
            get { return _selectedCategory?.Name; }
            set
            {
                _selectedCategory.Name = value;
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
    }
}