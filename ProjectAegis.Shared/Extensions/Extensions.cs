namespace ProjectAegis.Shared.Library.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Caliburn.Micro;
    using Interfaces;

    public static class Extensions
    {
        #region Object

        public static T GetParameter<T>(this object[] parameters, T defaulValue)
        {
            var obj = parameters.FirstOrDefault(x => x is T);
            return obj == null ? defaulValue : (T)obj;
        }

        #endregion

        #region String

        public static byte[] ToBytes(this string value, string code, int bytes)
        {
            var encoding = Encoding.GetEncoding(code);

            byte[] target = new byte[bytes];
            byte[] source = encoding.GetBytes(value);

            Array.Copy(source, target, target.Length > source.Length ? source.Length : target.Length);

            return target;
        }

        public static int ToTimestamp(this string date)
        {
            var dt = DateTime.Parse(date);
            return Convert.ToInt32(dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
        }

        #endregion

        #region Int

        public static string ToDate(this int timestamp, string format)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dt = dt.AddSeconds(timestamp);
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        #endregion

        #region Collections

        public static BindableCollection<T> ToBindableCollection<T>(this IEnumerable<T> source)
        {
            return new BindableCollection<T>(source);
        }

        #endregion

        #region Byte

        public static byte[] Clear(this byte[] source, int lenght)
        {
            var stringRepresentaion = Encoding.Unicode.GetString(source);
            var clearPath = stringRepresentaion.Substring(0, stringRepresentaion.IndexOf("\0", StringComparison.CurrentCulture));

            var clearBytes = new byte[lenght];

            Array.Copy(Encoding.Unicode.GetBytes(clearPath), clearBytes, Encoding.Unicode.GetBytes(clearPath).Length);

            return clearBytes;
        }

        #endregion

        #region Binary

        public static TModel ReadModel<TModel>(this BinaryReader reader, int version = 0)
            where TModel : IBinaryModel, new()
        {
            var model = new TModel();
            model.ReadModel(reader, version);
            return model;
        }
        public static void WriteModel<TModel>(this BinaryWriter writer, TModel model, int version = 0)
            where TModel : IBinaryModel
        {
            model.WriteModel(writer, version);
        }

        public static TModel ReadModelWithParameters<TModel>(this BinaryReader reader, int version = 0, params object[] prameters)
            where TModel : IBinaryModel, new()
        {
            var model = new TModel();
            model.ReadModel(reader, version, prameters);
            return model;
        }
        public static void WriteModelWithParameters<TModel>(this BinaryWriter writer, TModel model, int version = 0, params object[] prameters)
            where TModel : IBinaryModel
        {
            model.WriteModel(writer, version, prameters);
        }

        #endregion
    }
}