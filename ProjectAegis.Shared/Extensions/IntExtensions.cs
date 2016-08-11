using System;

namespace ProjectAegis.Shared.Extensions
{
    public static class IntExtensions
    {
        public static string ToDate(this int timestamp, string format)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dt = dt.AddSeconds(timestamp);
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}