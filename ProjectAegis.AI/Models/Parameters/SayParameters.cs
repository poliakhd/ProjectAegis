namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;
    using System.Text;

    using Caliburn.Micro;

    using Interfaces;

    public class SayParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _messageSize;
        private byte[] _message;
        private int _mask;

        #endregion

        public int MessageSize
        {
            get { return _messageSize; }
            set
            {
                _messageSize = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public byte[] Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Mask
        {
            get { return _mask; }
            set
            {
                _mask = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string MessageDisplay
        {
            get
            {
                return Message != null ? Encoding.Unicode.GetString(Message).Replace("\0", "") : "";
            }
            set
            {
                Message = Encoding.Unicode.GetBytes(value);
                MessageSize = Message.Length;
            }
        }

        public string Display => $"Say({MessageSize}, {Encoding.Unicode.GetString(Message ?? (Message = new byte[0])).Replace("\0", "")});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            MessageSize = reader.ReadInt32();
            Message = reader.ReadBytes(MessageSize);

            if (version > 10)
                Mask = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(MessageSize);
            writer.Write(Message);

            if (version > 10)
                writer.Write(Mask);
        }

        #endregion
    }
}