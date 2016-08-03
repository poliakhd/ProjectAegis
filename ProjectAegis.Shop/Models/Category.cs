namespace ProjectAegis.Shop.Models
{
    using Shared.Extensions;
    using Shared.Interfaces;

    using System.IO;
    using System.Collections.ObjectModel;

    public class Category : IBinaryModel
    {
        public byte[] Name { get; set; }
        public int SubCategoriesCount { get; set; }

        public ObservableCollection<SubCategory> SubCategories{ get; set; }

        public Category()
        {
            SubCategories = new ObservableCollection<SubCategory>();
        }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0)
        {
            Name = reader.ReadBytes(128).Clear(128);
            SubCategoriesCount = reader.ReadInt32();

            for (int j = 0; j < SubCategoriesCount; j++)
                SubCategories.Add(reader.ReadModel<SubCategory>(version));
        }

        public void WriteModel(BinaryWriter writer, int version = 0)
        {
            writer.Write(Name);
            writer.Write(SubCategoriesCount);

            foreach (var subCategory in SubCategories)
                writer.WriteModel(subCategory, version);
        }

        #endregion
    }
}