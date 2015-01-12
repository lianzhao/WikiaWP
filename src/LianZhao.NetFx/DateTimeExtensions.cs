using System;

namespace LianZhao
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixTimeStamp(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp);
        }
    }
}