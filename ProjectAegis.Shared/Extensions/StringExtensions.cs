using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjectAegis.Shared.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToBytes(this string value, string code, int bytes)
        {
            var encoding = Encoding.GetEncoding(code);

            byte[] target = new byte[bytes];
            byte[] source = encoding.GetBytes(value);

            Array.Copy(source, target, target.Length> source.Length ? source.Length : target.Length);

            return target;
        }

        public static int ToTimestamp(this string date)
        {
            var dt = DateTime.Parse(date);
            return Convert.ToInt32(dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
        }
    }
}