namespace ProjectAegis.Shop.Models
{
    using System;

    using System.IO;
    using System.Text;

    using Caliburn.Micro;

    using Base;
    using Shared.Library.Extensions;
    using Shared.Library.Interfaces;

    public class Category : PropertyChangedBase, IBinaryModel
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
        public int SubCategoriesCount { get; set; }

        public BindableCollection<SubCategory> SubCategories{ get; set; }

        public Category()
        {
            _name = "NULL".ToBytes("Unicode", 128);
            SubCategories = new BindableCollection<SubCategory>();
        }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            #region Parameters

            var fileType = parameters.GetParameter<FileType>(FileType.Client);

            #endregion

            if (fileType == FileType.Client)
            {
                _name = reader.ReadBytes(128).Clear(128);

                SubCategoriesCount = reader.ReadInt32();
            }

            if(SubCategoriesCount < 0 || SubCategoriesCount > 1000)
                throw new FileLoadException();

            for (int j = 0; j < SubCategoriesCount; j++)
                SubCategories.Add(reader.ReadModelWithParameters<SubCategory>(version, parameters));
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            #region Parameters

            var fileType = parameters.GetParameter<FileType>(FileType.Client);

            #endregion

            if (fileType == FileType.Client)
                writer.Write(_name);

            writer.Write(SubCategoriesCount);

            foreach (var subCategory in SubCategories)
                writer.WriteModelWithParameters(subCategory, version, parameters);
        }

        #endregion
    }
}