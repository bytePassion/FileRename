using System;
using bytePassion.Lib.FrameworkExtensions;


namespace bytePassion.Lib.TimeLib
{

	public struct Duration
	{
		public static readonly Duration Zero = new Duration(0);

		public Duration(TimeSpan timeSpan)
		{
			Seconds = (uint) timeSpan.TotalSeconds;
		}

		public Duration(uint seconds)
		{
			Seconds = seconds;
		}

		public Duration (Time t1, Time t2)
		{
			Seconds = ((uint)(Math.Abs((int)t1.SecondsFromDayBegin-(int)t2.SecondsFromDayBegin)));
		}

		public uint Seconds { get; }

		public override bool   Equals (object obj) => this.Equals(obj, (duration1, duration2) => duration1.Seconds == duration2.Seconds);
		public override int    GetHashCode ()      => Seconds.GetHashCode();
		public override string ToString ()         => Seconds.ToString();

		public static bool operator ==(Duration d1, Duration d2) => d1.Equals(d2);
		public static bool operator !=(Duration d1, Duration d2) => !(d1 == d2);
		public static bool operator < (Duration d1, Duration d2) => d1.Seconds < d2.Seconds;
		public static bool operator > (Duration d1, Duration d2) => d1.Seconds > d2.Seconds;
		public static bool operator <=(Duration d1, Duration d2) => d1 < d2 || d1 == d2;
		public static bool operator >=(Duration d1, Duration d2) => d1 > d2 || d1 == d2;

		public static Duration operator +(Duration a1, Duration a2) => new Duration(a1.Seconds + a2.Seconds);						
		public static Duration operator *(Duration a,  uint     d)  => new Duration(a.Seconds*d);
		public static Duration operator *(double   d,  Duration a)  => a*d;
		public static Duration operator *(uint     d,  Duration a)  => a*d;
		public static Duration operator /(Duration a,  double   d)  => a*(1.0/d);
		public static double   operator /(Duration a1, Duration a2) => (double)a1.Seconds/a2.Seconds;

		public static Duration operator -(Duration a1, Duration a2)
		{
			if (a2 > a1)
				throw new InvalidOperationException("Duration cannot be negative");

			return new Duration(a1.Seconds - a2.Seconds);
		}

		public static Duration operator *(Duration a, double d)
		{
			if (d<0)
				throw new InvalidOperationException("Duration cannot be negative");

			return new Duration((uint) Math.Floor(a.Seconds * d));
		}
	}
}
