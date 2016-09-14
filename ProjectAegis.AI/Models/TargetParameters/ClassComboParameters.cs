namespace ProjectAegis.AI.Models.TargetParameters
{
    using System.IO;

    using Interfaces;

    public class ClassComboParameters : IParameter
    {
        public int ComboState { get; set; }
        public string Display => $"ClassCombo({ComboState});";

        #region Implementation of IBinaryModel
        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            ComboState = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(ComboState);
        }

        #endregion
    }
}