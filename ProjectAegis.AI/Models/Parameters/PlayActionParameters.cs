namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;
    using System.Text;

    using Caliburn.Micro;

    using Interfaces;

    public class PlayActionParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private byte[] _actionName;
        private int _playTimes;
        private int _actionLastTime;
        private int _intervalTime;

        #endregion

        public byte[] ActionName
        {
            get { return _actionName; }
            set
            {
                _actionName = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int PlayTimes
        {
            get { return _playTimes; }
            set
            {
                _playTimes = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int ActionLastTime
        {
            get { return _actionLastTime; }
            set
            {
                _actionLastTime = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int IntervalTime
        {
            get { return _intervalTime; }
            set
            {
                _intervalTime = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"PlayAction({Encoding.GetEncoding("GBK").GetString(ActionName).Replace("\0", "")}, {PlayTimes}, {ActionLastTime}, {IntervalTime});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            ActionName = reader.ReadBytes(128);
            PlayTimes = reader.ReadInt32();
            ActionLastTime = reader.ReadInt32();
            IntervalTime = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(ActionName);
            writer.Write(PlayTimes);
            writer.Write(ActionLastTime);
            writer.Write(IntervalTime);
        }

        #endregion
    }
}