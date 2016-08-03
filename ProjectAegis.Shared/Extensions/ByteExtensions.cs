namespace ProjectAegis.Shared.Extensions
{
    using System;
    using System.Text;

    public static class ByteExtensions
    {
        public static byte[] Clear(this byte[] source, int lenght)
        {
            var stringRepresentaion = Encoding.Unicode.GetString(source);
            var clearPath = stringRepresentaion.Substring(0, stringRepresentaion.IndexOf("\0", StringComparison.CurrentCulture));

            var clearBytes = new byte[lenght];

            Array.Copy(Encoding.Unicode.GetBytes(clearPath), clearBytes, Encoding.Unicode.GetBytes(clearPath).Length);

            return clearBytes;
        }
    }
}