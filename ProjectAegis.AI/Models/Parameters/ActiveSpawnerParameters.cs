namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;


    public class ActiveSpawnerParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _controlId;
        private int _isActiveSpawner;

        #endregion

        public int ControlId
        {
            get { return _controlId; }
            set
            {
                _controlId = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int IsActiveSpawner
        {
            get { return _isActiveSpawner; }
            set
            {
                _isActiveSpawner = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"ActiveSpawner({ControlId}, {IsActiveSpawner});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            ControlId = reader.ReadInt32();
            IsActiveSpawner = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(ControlId);
            writer.Write(IsActiveSpawner);
        }

        #endregion
    }
}