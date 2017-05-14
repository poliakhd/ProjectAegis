using System.IO;
using Caliburn.Micro;

namespace ProjectAegis.AI.Models
{
    using Shared.Library.Extensions;
    using Shared.Library.Interfaces;

    public class Controller : IBinaryModel
    {
        public int Id { get; set; }
        public int Signature { get; set; }
        public int TriggersCount { get; set; }
        public BindableCollection<Trigger> Triggers { get; set; } = new BindableCollection<Trigger>();

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Signature = reader.ReadInt32();
            Id = reader.ReadInt32();
            TriggersCount = reader.ReadInt32();

            for (int i = 0; i < TriggersCount; i++)
                Triggers.Add(reader.ReadModel<Trigger>());
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Signature);
            writer.Write(Id);
            writer.Write(TriggersCount);

            foreach (var trigger in Triggers)
                writer.WriteModel(trigger);
        }

        #endregion
    }
}