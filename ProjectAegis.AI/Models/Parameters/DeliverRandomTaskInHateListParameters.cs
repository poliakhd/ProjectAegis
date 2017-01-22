using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class DeliverRandomTaskInHateListParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _id;
        private int _type;
        private int _range;
        private int _playerCount;

        #endregion

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Range
        {
            get { return _range; }
            set
            {
                _range = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int PlayerCount
        {
            get { return _playerCount; }
            set
            {
                _playerCount = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"DeliverRandomTaskInHateList({Id}, {Type}, {Range}, {PlayerCount});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Id = reader.ReadInt32();
            Type = reader.ReadInt32();
            Range = reader.ReadInt32();
            PlayerCount = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Id);
            writer.Write(Type);
            writer.Write(Range);
            writer.Write(PlayerCount);
        }

        #endregion
    }
}