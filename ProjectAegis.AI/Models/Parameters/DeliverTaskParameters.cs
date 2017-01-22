using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class DeliverTaskParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _id;
        private int _type;

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

        public string Display => $"DeliverTask({Id}, {Type});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Id = reader.ReadInt32();
            Type = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Id);
            writer.Write(Type);
        }

        #endregion
    }
}