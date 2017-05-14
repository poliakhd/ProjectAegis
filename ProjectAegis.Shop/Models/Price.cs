namespace ProjectAegis.Shop.Models
{
    using System;
    using System.IO;
    using Shared.Library.Extensions;
    using Shared.Library.Interfaces;

    public class Price : IBinaryModel
    {
        #region Private Members

        private int _expireDate;
        private int _startDate;
        private int _duration;

        private int _cost;

        #endregion

        public double Cost
        {
            get { return _cost / 100.00; }
            set { _cost = (int)(value * 100); }
        }
        public string ExpireDate
        {
            get { return _expireDate.ToDate("yyyy-MM-dd HH:mm:ss"); }
            set
            {
                try
                {
                    _expireDate = value.ToTimestamp();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        public string Duration
        {
            get { return _duration.ToDate("yyyy-MM-dd HH:mm:ss"); }
            set
            {
                try
                {
                    _duration = value.ToTimestamp();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        public string StartDate
        {
            get { return _startDate.ToDate("yyyy-MM-dd HH:mm:ss"); }
            set
            {
                try
                {
                    _startDate = value.ToTimestamp();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        public int ControlType { get; set; }
        public int Day { get; set; }
        public int Status { get; set; }
        public int Flags { get; set; }
        public int VipLevel { get; set; }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            _cost = reader.ReadInt32();
            _expireDate = reader.ReadInt32();
            _duration = reader.ReadInt32();

            if (version >= 142)
            {
                _startDate = reader.ReadInt32();
                ControlType = reader.ReadInt32();
                Day = reader.ReadInt32();
                Status = reader.ReadInt32();
                Flags = reader.ReadInt32();
            }

            if (version >= 153)
                VipLevel = reader.ReadInt32();
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(_cost);
            writer.Write(_expireDate);
            writer.Write(_duration);

            if (version >= 142)
            {
                writer.Write(_startDate);
                writer.Write(ControlType);
                writer.Write(Day);
                writer.Write(Status);
                writer.Write(Flags);
            }

            if(version >= 153)
                writer.Write(VipLevel);
        }

        #endregion
    }
}