using System.IO;
using Caliburn.Micro;
using ProjectAegis.Shared.Extensions;
using ProjectAegis.Shared.Interfaces;

namespace ProjectAegis.AI.Models
{
    public class Policy : IBinaryModel
    {
        public int Signature { get; set; }
        public int ControllersCount { get; set; }
        public BindableCollection<Controller> Controllers { get; set; } = new BindableCollection<Controller>();

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Signature = reader.ReadInt32();
            ControllersCount = reader.ReadInt32();

            for (int i = 0; i < ControllersCount; i++)
                Controllers.Add(reader.ReadModel<Controller>());
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Signature);
            writer.Write(ControllersCount);

            foreach (var controller in Controllers)
                writer.WriteModel(controller);
        }

        #endregion
    }
}   