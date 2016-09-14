namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;
    
    public class AttackParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _attackStategy;

        #endregion

        public int AttackStategy
        {
            get { return _attackStategy; }
            set
            {
                _attackStategy = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"Attack({AttackStategy});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            AttackStategy = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(AttackStategy);
        }

        #endregion
    }
}