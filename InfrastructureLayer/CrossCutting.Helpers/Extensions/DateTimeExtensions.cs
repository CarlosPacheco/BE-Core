using System;

namespace CrossCutting.Helpers.Extensions
{
	public static class DateTimeExtensions
	{
		public static TimeSpan ToTimeSpan(this DateTime date)
		{
			return new TimeSpan(0, date.Hour, date.Minute, date.Second, date.Millisecond);
		}

		public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
		{
			int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
			return dt.AddDays(-1 * diff).Date;
		}

		public static DateTime GetNextWeekday(this DateTime start, DayOfWeek day)
		{
			// The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
			int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
			return start.AddDays(daysToAdd);
		}
	}
}
