namespace ProjectAegis.AI.Models
{
    using System.IO;

    using Caliburn.Micro;

    using Extensions;
    using Interfaces;
    using Types;
    using Shared.Interfaces;
    using Parameter = Parameters.Parameter;


    public class Procedure : PropertyChangedBase, IBinaryModel
    {
        #region Private members

        private ParameterType _type;
        private TargetParameterType _targetType;

        #endregion

        public IParameter Parameters { get; set; }
        public IParameter TargetParameters { get; set; }

        public ParameterType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                Parameters = Parameter.Resolve(value);

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(Parameters));
            }
        }
        public TargetParameterType TargetType
        {
            get { return _targetType; }
            set
            {
                _targetType = value;

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(TargetParameters));
            }
        }

        public string Show => $"{Parameters} -> {TargetType}";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Type = (ParameterType)reader.ReadInt32();
            Parameters = reader.ReadParameters(Type, version);
            TargetType = (TargetParameterType)reader.ReadInt32();

            if (TargetType == TargetParameterType.ClassCombo)
                TargetParameters = reader.ReadTargetParameters(TargetType);
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write((int)Type);
            writer.WriteParameters(Parameters, Type, version);
            writer.Write((int)TargetType);

            if (TargetType == TargetParameterType.ClassCombo)
                writer.WriteTargetParameters(TargetParameters, TargetType);
        }

        #endregion
    }
}