using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class Skill2Parameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _skillId;
        private int _skillLevel;
        private int _levelType;
        private int _skillType;

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
        public int SkillType
        {
            get { return _skillType; }
            set
            {
                _skillType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Level
        {
            get { return _skillLevel; }
            set
            {
                _skillLevel = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int LevelType
        {
            get { return _levelType; }
            set
            {
                _levelType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"Skill2({SkillId}, {SkillType}, {Level}, {LevelType});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            SkillId = reader.ReadInt32();
            SkillType = reader.ReadInt32();
            Level = reader.ReadInt32();
            LevelType = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(SkillId);
            writer.Write(SkillType);
            writer.Write(Level);
            writer.Write(LevelType);
        }

        #endregion
    }
}