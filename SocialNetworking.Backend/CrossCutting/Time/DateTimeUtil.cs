using System;
using System.Linq;

namespace CrossCutting.Time
{
    public static class DateTimeUtil
    {

        public static DateTime BrazilDateTimeNow()
        {
            TimeZoneInfo zone = TimeZoneInfo.GetSystemTimeZones().First(z => z.Id.ToLower().Contains("south america"));

            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(zone.Id));
        }
    }
}
