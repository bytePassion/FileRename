using System;
using System.Globalization;
using System.Text;
using bytePassion.Lib.FrameworkExtensions;


namespace bytePassion.Lib.TimeLib
{
	public struct Date : IComparable<Date>
    {		
		public static readonly Date Dummy = new Date(0,0,0);
	    
		public Date(DateTime dateTime) 
            : this((byte)dateTime.Day, (byte)dateTime.Month, (ushort)dateTime.Year)
		{						
		}

		public Date(byte day, byte month, ushort year)
		{
			Day   = day;
			Month = month;
			Year  = year;
		}

		public override bool Equals(object obj)
		{					
			return this.Equals(obj, (date1, date2) => date1.Day == date2.Day &&
												      date1.Month == date2.Month &&
													  date1.Year == date2.Year);
		}

		public override int GetHashCode()
		{
			return Year.GetHashCode() ^ Month.GetHashCode() ^ Day.GetHashCode();
		}

		public static bool operator == (Date d1, Date d2) => d1.Equals(d2);
		public static bool operator != (Date d1, Date d2) => !(d1 == d2);

		public static bool operator < (Date d1, Date d2)
	    {
		    if (d1.Year < d2.Year) return true;
			if (d1.Year > d2.Year) return false;

		    if (d1.Month < d2.Month) return true;
			if (d1.Month > d2.Month) return false;

		    if (d1.Day < d2.Day) return true;
			if (d1.Day > d2.Day) return false;

		    return false;
	    }

		public static bool operator > (Date d1, Date d2)
		{
			if (d1.Year > d2.Year) return true;
			if (d1.Year < d2.Year) return false;

			if (d1.Month > d2.Month) return true;
			if (d1.Month < d2.Month) return false;

			if (d1.Day > d2.Day) return true;
			if (d1.Day < d2.Day) return false;

			return false;
		}

	    public static bool operator <= (Date d1, Date d2) => d1 < d2 || d1 == d2;
	    public static bool operator >= (Date d1, Date d2) => d1 > d2 || d1 == d2;
		

		public int CompareTo(Date other)
	    {
			if (this >  other) return 1;
			if (this == other) return 0;
			return -1;
		}

		/// <summary>
		/// return Date as String in format: dd.mm.yyyy
		/// </summary>
		/// <returns>Date in format dd.mm.yyyy</returns>
		public override string ToString()
		{
			var builder = new StringBuilder();

			if (Day < 10)
				builder.Append('0');

			builder.Append(Day);
			builder.Append('.');

			if (Month < 10)
				builder.Append('0');

			builder.Append(Month);
			builder.Append('.');
			builder.Append(Year);

			return builder.ToString();
		}

        public string GetShortDisplayString(CultureInfo cultureInfo)
        {
            var dateTime = new DateTime(Year, Month, Day);
            return $"{dateTime.ToString("d", cultureInfo)} {Year}";
        }

        public string GetDisplayString(CultureInfo cultureInfo)
		{
			var dateTime = new DateTime(Year, Month, Day);
			return $"{dateTime.ToString("m", cultureInfo)} {Year}" ;
		}        

        public string GetDisplayStringWithDayOfWeek(CultureInfo cultureInfo)
        {
            var dateTime = new DateTime(Year, Month, Day);
            return dateTime.ToString("D", cultureInfo);
        }

		public DateTime GetDateTime()
		{
			return new DateTime(Year, Month, Day);
		}

		public byte   Day   { get; }
	    public byte   Month { get; }
	    public ushort Year  { get; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////                                                                                     ////////
        ////////                               static members                                        ////////
        ////////                                                                                     ////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////

	    public static bool IsValidDateString(string s)
	    {
			var elements = s.Split('.');

		    if (elements.Length != 3)
			    return false;

			byte day;
			if (!byte.TryParse(elements[0], out day)) return false;
			if (day > 31) return false;
			
			byte month;
			if (!byte.TryParse(elements[1], out month)) return false;
			if (month > 12) return false;

			ushort year;
			if (!ushort.TryParse(elements[2], out year)) return false;
			if (year < 1800 || year > 2200) return false;

		    return true;
	    }

        public static Date Parse(string s)
		{
			var elements = s.Split('.');

			if (elements.Length != 3)
				throw new FormatException("expected Format: dd.mm.yyyy");

			var day   = byte.Parse(elements[0]);
			var month = byte.Parse(elements[1]);
			var year  = ushort.Parse(elements[2]);

			return new Date(day, month, year);
		}

		public static bool IsDummy(Date d)
		{
			return d == Dummy;
		}

		public static DayOfWeek GetDayOfWeekFrom(Date d)
		{
			return new DateTime(d.Year, d.Month, d.Day).DayOfWeek;
		}
	}
}