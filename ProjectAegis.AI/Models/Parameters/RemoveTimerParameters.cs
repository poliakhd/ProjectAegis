namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;

    public class RemoveTimerParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _timerId;

        #endregion

        public int TimerId
        {
            get { return _timerId; }
            set
            {
                _timerId = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"RemoveTimer({TimerId});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            TimerId = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(TimerId);
        }

        #endregion
    }
}