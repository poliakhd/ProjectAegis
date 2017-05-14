using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class SummonNpcParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _npcId;
        private int _npcType;
        private int _lifeType;
        private int _range;
        private int _pathId;
        private int _pathType;
        private int _npcCount;
        private int _npcCountType;
        private int _lifeId;

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
        public int NpcId
        {
            get { return _npcId; }
            set
            {
                _npcId = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int NpcType
        {
            get { return _npcType; }
            set
            {
                _npcType = value;
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

        public int LifeId
        {
            get { return _lifeId; }
            set
            {
                _lifeId = value;
                NotifyOfPropertyChange(nameof(LifeId));
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
        public int NpcCount
        {
            get { return _npcCount; }
            set
            {
                _npcCount = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int NpcCountType
        {
            get { return _npcCountType; }
            set
            {
                _npcCountType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"SummonNpc({LifeType}, {NpcId}, {NpcType}, {Range}, {LifeId}, {PathId}, {PathType}, {NpcCount}, {NpcCountType});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            LifeType = reader.ReadInt32();
            NpcId = reader.ReadInt32();
            NpcType = reader.ReadInt32();
            Range = reader.ReadInt32();
            LifeId = reader.ReadInt32();
            PathId = reader.ReadInt32();
            PathType = reader.ReadInt32();
            NpcCount = reader.ReadInt32();
            NpcCountType = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(LifeType);
            writer.Write(NpcId);
            writer.Write(NpcType);
            writer.Write(Range);
            writer.Write(LifeId);
            writer.Write(PathId);
            writer.Write(PathType);
            writer.Write(NpcCount);
            writer.Write(NpcCountType);
        }

        #endregion
    }
}