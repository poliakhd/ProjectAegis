namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;

    public class CreateTimerParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _timerId;
        private int _interval;
        private int _count;

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
        public int Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"CreateTimer({TimerId}, {Interval}, {Count});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            TimerId = reader.ReadInt32();
            Interval = reader.ReadInt32();
            Count = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(TimerId);
            writer.Write(Interval);
            writer.Write(Count);
        }

        #endregion
    }
}