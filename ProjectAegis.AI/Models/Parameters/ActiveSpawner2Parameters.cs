using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class ActiveSpawner2Parameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _controlId;
        private int _isActiveSpawner;
        private int _controlIdType;

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
        public int ControlIdType
        {
            get { return _controlIdType; }
            set
            {
                _controlIdType = value;
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

        public string Display => $"ActiveSpawner2({ControlId}, {ControlIdType}, {IsActiveSpawner});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            ControlId = reader.ReadInt32();
            ControlIdType = reader.ReadInt32();
            IsActiveSpawner = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(ControlId);
            writer.Write(ControlIdType);
            writer.Write(IsActiveSpawner);
        }

        #endregion
    }
}