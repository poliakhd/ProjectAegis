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
    }
}