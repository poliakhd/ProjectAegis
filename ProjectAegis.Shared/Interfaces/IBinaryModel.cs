namespace ProjectAegis.Shared.Interfaces
{
    using System.IO;

    public interface IBinaryModel
    {
        void ReadModel(BinaryReader reader, int version = 0);
        void WriteModel(BinaryWriter writer, int version = 0);
    }
}