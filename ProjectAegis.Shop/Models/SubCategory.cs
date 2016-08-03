namespace ProjectAegis.Shop.Models
{
    using Shared.Extensions;
    using Shared.Interfaces;

    using System.IO;

    public class SubCategory : IBinaryModel
    {
        public byte[] Name { get; set; }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0)
        {
            Name = reader.ReadBytes(128).Clear(128);
        }

        public void WriteModel(BinaryWriter writer, int version = 0)
        {
            writer.Write(Name);
        }

        #endregion
    }
}