using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
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
                MessageBox.Show(ex.Message);
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
            }
        }
        public BindableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void AddItem()
        {
            _items.Add(new Item()
            {
                Id = _shop.Items.Max(x => x.Id) + 1,
                CategoryId = SelectedCategoryIndex,
                SubCategoryId = SelectedSubCategoryIndex
            });

            NotifyOfPropertyChange(nameof(Items));
        }
        public void RemoveItems(IList list)
        {
            _items.RemoveRange(list.Cast<Item>());

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
    }
}