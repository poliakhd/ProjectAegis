using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using ProjectAegis.Localization.Managers;
using ProjectAegis.Shared.Extensions;
using ProjectAegis.Shop.Models;
using ProjectAegis.Shop.Models.Core;

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
        private CultureInfo _currentLanguage;

        #endregion

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

        public bool GiftSectionAvailability => Version >= 144;
        public bool OwnerNpcsSectionAvailability => Version >= 152;

        #endregion

        public MainWindowViewModel()
        {
            base.DisplayName = "shopeditor";

            _currentLanguage = TranslationManager.Instance.CurrentLanguage;
           
            NotifyOfPropertyChange(nameof(CurrentLanguage));
            NotifyOfPropertyChange(nameof(Languages));
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

            NotifyOfPropertyChange(nameof(GiftSectionAvailability));
            NotifyOfPropertyChange(nameof(OwnerNpcsSectionAvailability));
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

            NotifyOfPropertyChange(nameof(GiftSectionAvailability));
            NotifyOfPropertyChange(nameof(OwnerNpcsSectionAvailability));
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
            if(_subCategories.Count < 8)
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
    }
}