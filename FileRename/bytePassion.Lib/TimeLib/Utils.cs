using System;

namespace bytePassion.Lib.TimeLib
{
	public static class Utils
	{
		public static DateTime GetDateTime (Date day, Time time)
		{
			return new DateTime(day.Year, day.Month, day.Day,
								time.Hour, time.Minute, time.Second);
		}		
	}
}
