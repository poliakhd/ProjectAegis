using System;
using System.IO;
using System.Text;
using Caliburn.Micro;

namespace ProjectAegis.AI.Models
{
    using Shared.Library.Extensions;
    using Shared.Library.Interfaces;

    public class Trigger : PropertyChangedBase, IBinaryModel
    {
        public int Id { get; set; }
        public byte[] TriggerName { get; set; }
        public int Version { get; set; }
        public bool IsActive { get; set; }
        public bool IsRun { get; set; }
        public bool IsAttackValid { get; set; }
        public Condition Condition { get; set; }
        public int ProcedureCount { get; set; }
        public BindableCollection<Procedure> Procedures { get; set; } = new BindableCollection<Procedure>();

        public string TriggerNameEdit
        {
            get
            {
                return TriggerName != null ? Encoding.GetEncoding("GBK").GetString(TriggerName).Replace("\0", "") : "";
            }
            set
            {
                var array = new byte[128];
                var bytes = Encoding.GetEncoding("GBK").GetBytes(value);

                Array.Copy(bytes, array, array.Length > bytes.Length ? bytes.Length : 128);

                TriggerName = array;

                NotifyOfPropertyChange(nameof(TriggerNameDisplay));
            }
        }
        public string TriggerNameDisplay => $"[{Id}] : {TriggerNameEdit}";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Version = reader.ReadInt32();
            Id = reader.ReadInt32();
            IsActive = reader.ReadBoolean();
            IsRun = reader.ReadBoolean();
            IsAttackValid = reader.ReadBoolean();
            TriggerName = reader.ReadBytes(128).Clear(128, Encoding.GetEncoding("GBK"));
            Condition = reader.ReadModel<Condition>();

            ProcedureCount = reader.ReadInt32();
            for (var i = 0; i < ProcedureCount; i++)
                Procedures.Add(reader.ReadModel<Procedure>(Version));
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Version);
            writer.Write(Id);
            writer.Write(Convert.ToByte(IsActive));
            writer.Write(Convert.ToByte(IsRun));
            writer.Write(Convert.ToByte(IsAttackValid));
            writer.Write(TriggerName);
            writer.WriteModel(Condition);
            writer.Write(ProcedureCount);

            foreach (var procedure in Procedures)
                writer.WriteModel(procedure, Version);
        }

        #endregion
    }
}