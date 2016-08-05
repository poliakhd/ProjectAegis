namespace ProjectAegis.Shop.Models
{
    using Shared.Interfaces;

    using System;
    using System.IO;

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
            get
            {
                var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dt = dt.AddSeconds(_expireDate);
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                try
                {
                    var dt = DateTime.Parse(value);
                    _expireDate = Convert.ToInt32(dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                }
                catch (Exception)
                {
                }
            }
        }
        public string Duration
        {
            get
            {
                var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dt = dt.AddSeconds(_duration);
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                try
                {
                    var dt = DateTime.Parse(value);
                    _duration = Convert.ToInt32(dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                }
                catch (Exception)
                {
                }
            }
        }
        public string StartDate
        {
            get
            {
                var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dt = dt.AddSeconds(_startDate);
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                try
                {
                    var dt = DateTime.Parse(value);
                    _startDate = Convert.ToInt32(dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                }
                catch (Exception)
                {
                }
            }
        }
        public int ControlType { get; set; }
        public int Day { get; set; }
        public int Status { get; set; }
        public int Flags { get; set; }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0)
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
        }

        public void WriteModel(BinaryWriter writer, int version = 0)
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
        }

        #endregion
    }
}