using System;
using System.Text;
using bytePassion.Lib.FrameworkExtensions;


namespace bytePassion.Lib.TimeLib
{
	public struct Time : IComparable<Time>
    {
		public static readonly Time Dummy = new Time(0,0);

		private const uint SecondsOfADay = 86400;
       
		public Time(Time time) 
			: this(time.Hour, time.Minute, time.Second)
		{						
		}

		public Time(DateTime time) 
			: this((byte) time.Hour, (byte) time.Minute, (byte) time.Second)
		{			
		}

		public Time (uint secondsFromDayBegin)
		{

			if (secondsFromDayBegin > 86399)
				throw new ArgumentException("parameter secondsFromDayBegin has to be in range from 0 to 86399");

			var timeSeconds = secondsFromDayBegin % 60;
			var restTime    = (secondsFromDayBegin - timeSeconds) / 60;
			var timeMinutes = restTime % 60;
			var timeHour    = (restTime - timeMinutes) / 60;

			Hour   = (byte) timeHour;
			Minute = (byte) timeMinutes;
			Second = (byte) timeSeconds;

			SecondsFromDayBegin = secondsFromDayBegin;
		}

		public Time(byte hour, byte minute, byte second=0)
		{
			if (hour   > 23) throw new ArgumentException("parameter hour has to be in the range from 0 to 23");
			if (minute > 59) throw new ArgumentException("parameter minute has to be in the range from 0 to 59");
			if (second > 59) throw new ArgumentException("parameter second has to be in the range from 0 to 59");

			Hour   = hour;
			Minute = minute;
			Second = second;

			SecondsFromDayBegin = (uint)(hour * 3600 + minute * 60 + second);
		}

	    public byte Hour   { get; }
	    public byte Minute { get; }
		public byte Second { get; }

		public uint SecondsFromDayBegin { get; }

	    public static bool operator ==(Time t1, Time t2) => EqualsExtension.EqualsForEqualityOperator(t1, t2);
	    public static bool operator !=(Time t1, Time t2) => !(t1 == t2);

	    public static bool operator <(Time t1, Time t2) => t1.SecondsFromDayBegin < t2.SecondsFromDayBegin;
	    public static bool operator >(Time t1, Time t2) => t1.SecondsFromDayBegin > t2.SecondsFromDayBegin;

	    public static bool operator <=(Time t1, Time t2) => t1 < t2 || t1 == t2;
	    public static bool operator >=(Time t1, Time t2) => t1 > t2 || t1 == t2;

	    public static Time operator +(Time t, Duration d) => new Time((t.SecondsFromDayBegin + d.Seconds) % SecondsOfADay);
	    public static Time operator -(Time t, Duration d) => new Time((t.SecondsFromDayBegin - d.Seconds) % SecondsOfADay);

	    public int CompareTo (Time other)
		{
			if (this > other) return 1;
			if (this == other) return 0;
			return -1;
		}

	    public override bool Equals (object obj)
		{
			return this.Equals(obj, (time1, time2) => time1.SecondsFromDayBegin == time2.SecondsFromDayBegin);
		}

		public override int GetHashCode ()
		{
			return Hour.GetHashCode() ^ Minute.GetHashCode() ^ Second.GetHashCode();
		}	    

	    public override string ToString ()
		{
			var builder = new StringBuilder();

			if (Hour < 10)
				builder.Append('0');

			builder.Append(Hour);
			builder.Append(':');

			if (Minute < 10)
				builder.Append('0');

			builder.Append(Minute);
			builder.Append(':');

			if (Second < 10)
				builder.Append('0');

			builder.Append(Second);

			return builder.ToString();
		}

		public string ToStringMinutesAndHoursOnly()
		{
			var builder = new StringBuilder();

			if (Hour < 10)
				builder.Append('0');

			builder.Append(Hour);
			builder.Append(':');

			if (Minute < 10)
				builder.Append('0');

			builder.Append(Minute);

			return builder.ToString();
		}

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////                                                                                     ////////
        ////////                               static members                                        ////////
        ////////                                                                                     ////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////

		public static bool IsValidTimeString(string s)
		{
			var elements = s.Split(':');			

			if (elements.Length == 2)
			{
				byte hour;
				if (!byte.TryParse(elements[0], out hour)) return false;
				if (hour > 23) return false;

				byte minutes;
				if (!byte.TryParse(elements[1], out minutes)) return false;
				if (minutes > 59) return false;

				return true;
			}
			else if (elements.Length == 3)
			{
				byte hour;
				if (!byte.TryParse(elements[0], out hour)) return false;
				if (hour > 23) return false;

				byte minutes;
				if (!byte.TryParse(elements[1], out minutes)) return false;
				if (minutes > 59) return false;

				byte seconds;
				if (!byte.TryParse(elements[2], out seconds)) return false;
				if (seconds > 59) return false;
				
				return true;
			}
			else
			{
				return false;
			}
		}

        public static Time Parse(string s)
		{
			var elements = s.Split(':');

			if (elements.Length == 2)
			{
				var hour   = byte.Parse(elements[0]);
				var minute = byte.Parse(elements[1]);

				return new Time(hour, minute);
			}

			if (elements.Length == 3)
			{
				var hour    = byte.Parse(elements[0]);
				var minute  = byte.Parse(elements[1]);
				var seconds = byte.Parse(elements[2]);

				return new Time(hour, minute, seconds);
			}

			throw new FormatException("expected Format: hh:mm or hh:mm:ss");
		}

		public static bool IsDummy(Time t)
		{
			return t == Dummy;
		}
    }	
}
