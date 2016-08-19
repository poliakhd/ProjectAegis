namespace ProjectAegis.Shop.Models
{
    using System.IO;
    using System;
    using System.Text;

    using Caliburn.Micro;

    using Base;

    using Shared.Extensions;
    using Shared.Interfaces;

    public class SubCategory : PropertyChangedBase, IBinaryModel
    {
        #region Private Members

        private byte[] _name;

        #endregion

        public string Name
        {
            get { return Encoding.Unicode.GetString(_name).Replace("\0", ""); }
            set
            {
                var en = Encoding.Unicode;

                var endSource = new byte[1024];
                var temp = en.GetBytes(value.Replace("\0", ""));

                Array.Copy(temp, endSource, temp.Length > endSource.Length ? endSource.Length : temp.Length);

                _name = endSource;

                NotifyOfPropertyChange();
            }
        }

        public SubCategory()
        {
            _name = "NULL".ToBytes("Unicode", 128);
        }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            #region Parameters

            var fileType = parameters.GetParameter<FileType>(FileType.Client);

            #endregion

            if (fileType == FileType.Client)
                _name = reader.ReadBytes(128).Clear(128);
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            #region Parameters

            var fileType = parameters.GetParameter<FileType>(FileType.Client);

            #endregion

            if (fileType == FileType.Client)
                writer.Write(_name);
        }

        #endregion
    }
}