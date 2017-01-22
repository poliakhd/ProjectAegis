using System.IO;

namespace ProjectAegis.AI.Models.Parameters
{
    using Caliburn.Micro;

    using Interfaces;

    public class DeliverFactionPvpPointsParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _type;

        #endregion

        public int Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"DeliverFactionPvpPoints({Type});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Type = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Type);
        }

        #endregion
    }
}