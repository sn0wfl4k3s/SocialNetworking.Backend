using System;

namespace CrossCutting.Time
{
    public static class DateTimeUtil
    {
        public static DateTime BrazilDateTimeNow()
            => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
