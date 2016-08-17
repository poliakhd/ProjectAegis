using System.Linq;
using Caliburn.Micro;
using ProjectAegis.Shop.Models.Core;

namespace ProjectAegis.Shop.Models
{
    using Shared.Extensions;
    using Shared.Interfaces;

    using System.IO;
    using System.Collections.ObjectModel;

    public class Shop : IBinaryModel
    {
        public int TimeStamp { get; set; }
        public int AmountItems { get; set; }

        public BindableCollection<Item> Items { get; set; }
        public BindableCollection<Category> Categories { get; set; }

        public Shop()
        {
            Items = new BindableCollection<Item>();
            Categories = new BindableCollection<Category>();
        }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            #region Parameters

            var fileType = parameters.GetParameter<FileType>(FileType.Client);

            #endregion

            TimeStamp = reader.ReadInt32();
            AmountItems = reader.ReadInt32();

            for (int i = 0; i < AmountItems; i++)
                Items.Add(reader.ReadModelWithParameters<Item>(version, parameters));

            for (int i = 0; i < 8; i++)
                Categories.Add(reader.ReadModelWithParameters<Category>(version, parameters));

            if (fileType == FileType.Server)
            {
                foreach (var category in Categories)
                {
                    int subCategoriesAmount = Items.Where(x => x.CategoryId == Categories.IndexOf(category)).Max(y => y.SubCategoryId);

                    for (int i = 0; i < subCategoriesAmount + 1; i++)
                    {
                        category.SubCategories.Add(new SubCategory());
                    }
                }
            }
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            #region Parameters

            var fileType = parameters.GetParameter<FileType>(FileType.Client);

            #endregion

            writer.Write(TimeStamp);
            writer.Write(Items.Count);
            
            foreach (var item in Items)
                writer.WriteModelWithParameters(item, version, parameters);

            if(fileType == FileType.Server)
                return;

            foreach (var category in Categories)
                writer.WriteModelWithParameters(category, version, parameters);
        }

        #endregion
    }
}