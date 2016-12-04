using System;
using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.Types.SemanticTypes;
using static bytePassion.Lib.GeometryLib.Utils.GeometryLibUtils;

namespace bytePassion.Lib.GeometryLib.Base
{
	public struct Vec2
	{        
		public static readonly Vec2 Zero = new Vec2(new XCoord(0.0), new YCoord(0.0));

		public Vec2(Point from, Point to) 
			: this(new XCoord(to.XCoord-from.XCoord), new YCoord(to.YCoord-from.YCoord))
		{			
		}

		public Vec2(Vec2 v)
			: this(v.X, v.Y)
		{			
		}

        public Vec2(XCoord x, YCoord y)
        {
            X = x;
            Y = y;
        }

	    public XCoord X { get; }
		public YCoord Y { get; }

		public double Length        => Math.Sqrt(X*X + Y*Y);
		public double SquaredLength =>           X*X + Y*Y;

	    public override bool   Equals(object obj) => this.Equals(obj, (v1, v2) => DoubleEquals(v1.X, v2.X) && DoubleEquals(v1.Y, v2.Y));
	    public override string ToString()         => $"Vec2({DoubleFormat(X)}|{DoubleFormat(Y)})";
	    public override int    GetHashCode()      => X.GetHashCode() ^ Y.GetHashCode();

	    public static bool operator > (Vec2 v1, Vec2 v2) => v1.Length > v2.Length;
		public static bool operator < (Vec2 v1, Vec2 v2) => v1.Length < v2.Length;
		public static bool operator >=(Vec2 v1, Vec2 v2) => (v1 == v2) || (v1 > v2);
		public static bool operator <=(Vec2 v1, Vec2 v2) => (v1 == v2) || (v1 < v2);
		public static bool operator ==(Vec2 v1, Vec2 v2) => v1.Equals(v2);
		public static bool operator !=(Vec2 v1, Vec2 v2) => !(v1 == v2);

		public static Vec2   operator +(Vec2 v1, Vec2 v2) => new Vec2(new XCoord(v1.X + v2.X), new YCoord(v1.Y + v2.Y));
		public static Vec2   operator -(Vec2 v1)          => new Vec2(new XCoord(-v1.X), new YCoord(-v1.Y));
		public static Vec2   operator -(Vec2 v1, Vec2 v2) => v1 + (-v2);
		public static double operator *(Vec2 v1, Vec2 v2) => v1.X * v2.X + v1.Y * v2.Y;
		public static Vec2   operator *(Vec2 v, double d) => new Vec2(new XCoord(v.X * d), new YCoord(v.Y * d));
		public static Vec2   operator /(Vec2 v, double d) => v * (1.0 / d);
		public static Vec2   operator *(double d, Vec2 v) => v * d;

	    public Vec2 Normalize() => this / Length;

		public Vec2 ProjectTo(Vec2 v)
		{
            return (new Vec2(v)) * ((this * v) / (v.Length * v.Length));
		}

		public Angle GetAngleTo(Vec2 v)
		{			
			// TODO: test this method

			var v1 = this;
			var v2 = v;

			var a1 = new Angle(new Radians(Math.Atan2(v1.Y, v1.X)));
			var a2 = new Angle(new Radians(Math.Atan2(v2.Y, v2.X)));

			var res = a2 - a1;

//			if (res.Value < -180.0)
//				res += new Angle(360.0);
//
//			if (res.Value > 180.0)
//				res -= new Angle(-360.0);

			return res;
		}

		public static Angle GetAngleBetween(Vec2 v1, Vec2 v2)
		{
			return v1.GetAngleTo(v2);
		}
	}
}
