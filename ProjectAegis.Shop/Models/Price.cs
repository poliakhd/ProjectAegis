namespace ProjectAegis.Shop.Models
{
    using Shared.Interfaces;

    using System.IO;

    public class Price : IBinaryModel
    {
        public int Cost { get; set; }
        public int ExpireDate { get; set; }
        public int Duration { get; set; }
        public int StartDate { get; set; }
        public int ControlType { get; set; }
        public int Day { get; set; }
        public int Status { get; set; }
        public int Flags { get; set; }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0)
        {
            Cost = reader.ReadInt32();
            Status = reader.ReadInt32();
            Duration = reader.ReadInt32();

            if (version >= 142)
            {
                StartDate = reader.ReadInt32();
                ControlType = reader.ReadInt32();
                Day = reader.ReadInt32();
                Status = reader.ReadInt32();
                Flags = reader.ReadInt32();
            }
        }

        public void WriteModel(BinaryWriter writer, int version = 0)
        {
            writer.Write(Cost);
            writer.Write(ExpireDate);
            writer.Write(Duration);

            if (version >= 142)
            {
                writer.Write(StartDate);
                writer.Write(ControlType);
                writer.Write(Day);
                writer.Write(Status);
                writer.Write(Flags);
            }
        }

        #endregion
    }
}