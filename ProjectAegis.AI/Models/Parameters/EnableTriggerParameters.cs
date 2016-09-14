namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;

    public class EnableTriggerParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _triggerId;

        #endregion

        public int TriggerId
        {
            get { return _triggerId; }
            set
            {
                _triggerId = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"EnableTrigger({TriggerId});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            TriggerId = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(TriggerId);
        }

        #endregion
    }
}