using System;

namespace PM3Events.Api.Utilities
{
    internal static class DateTimeUtilities
    {
        public static DateTime GetFirstDateOfCurrentMonth()
        {
            return new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        }

        public static DateTime GetDecemberLastDateOfCurrentYear()
        {
            return new DateTime(DateTime.UtcNow.Year, 12, 31);
        }
    }
}
