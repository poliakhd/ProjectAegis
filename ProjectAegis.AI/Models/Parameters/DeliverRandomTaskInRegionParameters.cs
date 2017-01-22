using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;
using ProjectAegis.Shared.Extensions;

namespace ProjectAegis.AI.Models.Parameters
{
    public class Point : IParameter
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public string Display => $" Point({X}; {Y}; {Z});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }

        #endregion
    }

    public class DeliverRandomTaskInRegionParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private int _id;
        private Point _min;
        private Point _max;

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
        public Point Min
        {
            get { return _min; }
            set
            {
                _min = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public Point Max
        {
            get { return _max; }
            set
            {
                _max = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $" DeliverRandomTaskInRegion({Id}, {Min.Display}, {Max.Display});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Id = reader.ReadInt32();
            Min = reader.ReadModel<Point>();
            Max = reader.ReadModel<Point>();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Id);
            writer.WriteModel(Min);
            writer.WriteModel(Max);
        }

        #endregion
    }
}