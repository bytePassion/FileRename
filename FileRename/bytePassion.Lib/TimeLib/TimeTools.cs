using System;

namespace bytePassion.Lib.TimeLib
{
	public static class TimeTools
	{
		public static Tuple<Date, Time> GetCurrentTimeStamp()
		{
			var currentTime = DateTime.Now;

			return new Tuple<Date, Time>(new Date(currentTime), new Time(currentTime));
		}

		public static Date Today()
		{
			return new Date(DateTime.Now);
		}
	}
}
