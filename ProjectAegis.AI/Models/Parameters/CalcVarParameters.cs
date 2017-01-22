using System.IO;
using Caliburn.Micro;
using ProjectAegis.AI.Models.Interfaces;

namespace ProjectAegis.AI.Models.Parameters
{
    public class CalcVarParameters : PropertyChangedBase, IParameter
    {
        #region PrivateMembers

        private int _dst;
        private int _dstType;
        private int _src;
        private int _srcType;
        private int _operation;
        private int _src2;
        private int _src2Type;

        #endregion

        public int Dst
        {
            get { return _dst; }
            set
            {
                _dst = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int DstType
        {
            get { return _dstType; }
            set
            {
                _dstType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Src
        {
            get { return _src; }
            set
            {
                _src = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int SrcType
        {
            get { return _srcType; }
            set
            {
                _srcType = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Src2
        {
            get { return _src2; }
            set
            {
                _src2 = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
        public int Src2Type
        {
            get { return _src2Type; }
            set
            {
                _src2Type = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }

        public string Display => $"CalcVar({Dst}, {DstType}, {Src}, {SrcType}, {Operation}, {Src2}, {Src2Type});";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Dst = reader.ReadInt32();
            DstType = reader.ReadInt32();
            Src = reader.ReadInt32();
            SrcType = reader.ReadInt32();
            Operation = reader.ReadInt32();
            Src2 = reader.ReadInt32();
            Src2Type = reader.ReadInt32();
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Dst);
            writer.Write(DstType);
            writer.Write(Src);
            writer.Write(SrcType);
            writer.Write(Operation);
            writer.Write(Src2);
            writer.Write(Src2Type);
        }

        #endregion
    }
}