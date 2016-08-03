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

        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public Shop()
        {
            Items = new ObservableCollection<Item>();
            Categories = new ObservableCollection<Category>();
        }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0)
        {
            TimeStamp = reader.ReadInt32();
            AmountItems = reader.ReadInt32();

            for (int i = 0; i < AmountItems; i++)
                Items.Add(reader.ReadModel<Item>(version));

            for (int i = 0; i < 8; i++)
                Categories.Add(reader.ReadModel<Category>(version));
        }

        public void WriteModel(BinaryWriter writer, int version = 0)
        {
            writer.Write(TimeStamp);
            writer.Write(Items.Count);
            
            foreach (var item in Items)
                writer.WriteModel(item, version);

            foreach (var category in Categories)
                writer.WriteModel(category, version);
        }

        #endregion
    }
}