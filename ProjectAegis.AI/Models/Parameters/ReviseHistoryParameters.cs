namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;

    public class ReviseHistoryParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _value;
        private int _key;

        #endregion

        public int Key
        {
            get { return _key; }
            set
            {
                _key = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"ReviseHistory({Key}, {Value});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Key = reader.ReadInt32();
            Value = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Key);
            writer.Write(Value);
        }

        #endregion
    }
}