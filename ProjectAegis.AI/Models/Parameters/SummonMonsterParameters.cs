namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;

    public class SummonMonsterParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _mobId;
        private int _count;
        private int _targetDistance;
        private int _remainTime;
        private int _dieWithWho;
        private int _pathId;

        #endregion

        public int MobId
        {
            get { return _mobId; }
            set
            {
                _mobId = value;
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
        public int TargetDistance
        {
            get { return _targetDistance; }
            set
            {
                _targetDistance = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int RemainTime
        {
            get { return _remainTime; }
            set
            {
                _remainTime = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int DieWithWho
        {
            get { return _dieWithWho; }
            set
            {
                _dieWithWho = value;
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

        public string Display => $"SummonMonster({MobId}, {Count}, {TargetDistance}, {RemainTime}, {DieWithWho}, {PathId});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            MobId = reader.ReadInt32();
            Count = reader.ReadInt32();
            TargetDistance = reader.ReadInt32();
            RemainTime = reader.ReadInt32();
            DieWithWho = reader.ReadInt32();
            PathId = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(MobId);
            writer.Write(Count);
            writer.Write(TargetDistance);
            writer.Write(RemainTime);
            writer.Write(DieWithWho);
            writer.Write(PathId);
        }

        #endregion
    }
}