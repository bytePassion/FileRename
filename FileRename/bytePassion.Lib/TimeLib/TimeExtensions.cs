
using System;


namespace bytePassion.Lib.TimeLib
{
	public static class TimeExtensions
	{
		public static Date DayBefore(this Date date) => AddDay(date, -1);
		public static Date DayAfter (this Date date) => AddDay(date, +1);

		private static Date AddDay(Date date, int addedDays)
		{
			var dateAsDateTime = new DateTime(date.Year, date.Month, date.Day);
			var dayBevore = dateAsDateTime.AddDays(addedDays);
			return new Date(dayBevore);
		}
	}
}
