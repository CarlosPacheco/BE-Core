using System;

namespace CrossCutting.Helpers.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime? TryParse(string text)
        {
            if (DateTime.TryParse(text, out var date))
            {
                return date;
            }

            return null;
        }
    }
}
