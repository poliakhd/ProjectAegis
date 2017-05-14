using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    using Shared.Library.Extensions;

    public class ClearTowerTaskInRegionParameters : PropertyChangedBase, IParameter
    {
        #region Private Members

        private Point _min;
        private Point _max;

        #endregion

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

        public string Display => $"ClearTowerTaskInRegion({Min.Display}, {Max.Display});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Min = reader.ReadModel<Point>();
            Max = reader.ReadModel<Point>();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.WriteModel(Min);
            writer.WriteModel(Max);
        }

        #endregion
    }
}