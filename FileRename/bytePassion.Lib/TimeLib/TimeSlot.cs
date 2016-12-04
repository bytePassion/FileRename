namespace bytePassion.Lib.TimeLib
{
	public class TimeSlot
	{
		public TimeSlot (Time begin, Time end)
		{
			Begin = begin;
			End   = end;
		}

		public Time Begin { get; }
		public Time End   { get; }
	}
}
