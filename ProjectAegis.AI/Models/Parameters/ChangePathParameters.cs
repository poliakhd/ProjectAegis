namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;

    public class ChangePathParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _worldTag;
        private int _globalPathId;
        private int _pathType;
        private int _speedFlag;

        #endregion

        public int WorldTag
        {
            get { return _worldTag; }
            set
            {
                _worldTag = value;
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
        public int SpeedFlag
        {
            get { return _speedFlag; }
            set
            {
                _speedFlag = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"ChangePath({WorldTag}, {GlobalPathId}, {PathType}, {SpeedFlag});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            WorldTag = reader.ReadInt32();
            GlobalPathId = reader.ReadInt32();
            PathType = reader.ReadInt32();
            SpeedFlag = reader.ReadInt32();
        }
        
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(WorldTag);
            writer.Write(GlobalPathId);
            writer.Write(PathType);
            writer.Write(SpeedFlag);
        }

        #endregion
    }
}