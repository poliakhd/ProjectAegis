using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class SummonMonster2Parameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _dispearCondition;
        private int _monsterId;
        private int _monsterType;
        private int _range;
        private int _life;
        private int _pathId;
        private int _pathType;
        private int _monsterCount;
        private int _monsterCountType;

        #endregion

        public int DispearCondition
        {
            get { return _dispearCondition; }
            set
            {
                _dispearCondition = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int MonsterId
        {
            get { return _monsterId; }
            set
            {
                _monsterId = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int MonsterType
        {
            get { return _monsterType; }
            set
            {
                _monsterType = value;
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
        public int PathId
        {
            get { return _pathId; }
            set
            {
                _pathId = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int PathType
        {
            get { return _pathType; }
            set
            {
                _pathType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int MonsterCount
        {
            get { return _monsterCount; }
            set
            {
                _monsterCount = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int MonsterCountType
        {
            get { return _monsterCountType; }
            set
            {
                _monsterCountType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"SummonMonster2({DispearCondition}, {MonsterId}, {MonsterType}, {Range}, {Life}, {PathId}, {PathType}, {MonsterCount}, {MonsterCountType});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            DispearCondition = reader.ReadInt32();
            MonsterId = reader.ReadInt32();
            MonsterType = reader.ReadInt32();
            Range = reader.ReadInt32();
            Life = reader.ReadInt32();
            PathId = reader.ReadInt32();
            PathType = reader.ReadInt32();
            MonsterCount = reader.ReadInt32();
            MonsterCountType = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(DispearCondition);
            writer.Write(MonsterId);
            writer.Write(MonsterType);
            writer.Write(Range);
            writer.Write(Life);
            writer.Write(PathId);
            writer.Write(PathType);
            writer.Write(MonsterCount);
            writer.Write(MonsterCountType);
        }

        #endregion
    }
}