namespace ProjectAegis.Shop.Models
{
    using Shared.Extensions;
    using Shared.Interfaces;

    using System.IO;
    using System.Collections.ObjectModel;

    public class Item : IBinaryModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int ItemId { get; set; }
        public int ItemAmount { get; set; }
        public int Status { get; set; }

        public byte[] Texture { get; set; }
        public byte[] Description { get; set; }
        public byte[] Name { get; set; }
        public byte[] Unk { get; set; }

        public ObservableCollection<Price> Prices { get; set; }

        public int GiftId { get; set; }
        public int GiftAmount { get; set; }
        public int GiftDuration { get; set; }
        public int LogPrice { get; set; }

        public Item()
        {
            Prices = new ObservableCollection<Price>();
        }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0)
        {
            Id = reader.ReadInt32();

            CategoryId = reader.ReadInt32();
            SubCategoryId = reader.ReadInt32();

            Texture = reader.ReadBytes(128);

            ItemId = reader.ReadInt32();
            ItemAmount = reader.ReadInt32();

            var length = version == 126 ? 3 : 4;
            for (int i = 0; i < length; i++)
                Prices.Add(reader.ReadModel<Price>(version));

            if (version == 126)
            {
                Unk = reader.ReadBytes(12);
                Status = reader.ReadInt32();
            }

            Description = reader.ReadBytes(1024);
            Name = reader.ReadBytes(64).Clear(64);

            if (version == 144)
            {
                GiftId = reader.ReadInt32();
                GiftAmount = reader.ReadInt32();
                GiftDuration = reader.ReadInt32();
                LogPrice = reader.ReadInt32();
            }
        }

        public void WriteModel(BinaryWriter writer, int version = 0)
        {
            writer.Write(Id);

            writer.Write(CategoryId);
            writer.Write(SubCategoryId);

            writer.Write(Texture);

            writer.Write(ItemId);
            writer.Write(ItemAmount);

            foreach (var price in Prices)
                writer.WriteModel(price, version);

            if (version == 126)
            {
                writer.Write(Unk);
                writer.Write(Status);
            }

            writer.Write( Description);
            writer.Write( Name);

            if (version == 144)
            {
                writer.Write(GiftId);
                writer.Write(GiftAmount);
                writer.Write(GiftDuration);
                writer.Write(LogPrice);
            }
        }

        #endregion
    }
}