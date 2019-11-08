using System;

namespace CrossCutting.Helpers.Extensions
{
	public static class DateTimeExtended
	{
		public static TimeSpan ToTimeSpan(this DateTime date)
		{
			return new TimeSpan(0, date.Hour, date.Minute, date.Second, date.Millisecond);
		}
	}
}
