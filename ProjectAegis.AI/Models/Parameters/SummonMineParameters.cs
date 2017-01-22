using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class SummonMineParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _id;
        private int _type;
        private int _lifeType;
        private int _range;
        private int _life;
        private int _count;
        private int _countType;

        #endregion

        public int LifeType
        {
            get { return _lifeType; }
            set
            {
                _lifeType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
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
        public int Life
        {
            get { return _life; }
            set
            {
                _life = value;
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
        public int CountType
        {
            get { return _countType; }
            set
            {
                _countType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"SummonMine({LifeType}, {Type}, {Range}, {Life}, {Count}, {CountType});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            LifeType = reader.ReadInt32();
            Id = reader.ReadInt32();
            Type = reader.ReadInt32();
            Range = reader.ReadInt32();
            Life = reader.ReadInt32();
            Count = reader.ReadInt32();
            CountType = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(LifeType);
            writer.Write(Id);
            writer.Write(Type);
            writer.Write(Range);
            writer.Write(Life);
            writer.Write(Count);
            writer.Write(CountType);
        }

        #endregion
    }
}