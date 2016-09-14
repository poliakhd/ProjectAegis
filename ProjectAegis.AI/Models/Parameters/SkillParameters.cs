namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;

    public class SkillParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _skillId;
        private int _skillLevel;

        #endregion

        public int SkillId
        {
            get { return _skillId; }
            set
            {
                _skillId = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int SkillLevel
        {
            get { return _skillLevel; }
            set
            {
                _skillLevel = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"Skill({SkillId}, {SkillLevel});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            SkillId = reader.ReadInt32();
            SkillLevel = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(SkillId);
            writer.Write(SkillLevel);
        }

        #endregion
    }
}