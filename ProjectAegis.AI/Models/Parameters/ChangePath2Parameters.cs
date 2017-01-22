using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class ChangePath2Parameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _worldId;
        private int _globalPathId;
        private int _pathType;
        private int _speedFlag;
        private int _patrolType;

        #endregion

        public int WorldId
        {
            get { return _worldId; }
            set
            {
                _worldId = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int GlobalPathId
        {
            get { return _globalPathId; }
            set
            {
                _globalPathId = value;
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
        public int PatrolType
        {
            get { return _patrolType; }
            set
            {
                _patrolType = value; 
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int SpeedFlag
        {
            get { return _speedFlag; }
            set
            {
                _speedFlag = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"ChangePath2({WorldId}, {GlobalPathId}, {PathType}, {PatrolType}, {SpeedFlag});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            WorldId = reader.ReadInt32();
            GlobalPathId = reader.ReadInt32();
            PathType = reader.ReadInt32();
            PatrolType = reader.ReadInt32();
            SpeedFlag = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(WorldId);
            writer.Write(GlobalPathId);
            writer.Write(PathType);
            writer.Write(PatrolType);
            writer.Write(SpeedFlag);
        }

        #endregion
    }
}