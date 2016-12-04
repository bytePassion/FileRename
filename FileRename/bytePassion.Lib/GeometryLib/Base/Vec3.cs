using System;
using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.Types.SemanticTypes;
using static bytePassion.Lib.GeometryLib.Utils.GeometryLibUtils;


namespace bytePassion.Lib.GeometryLib.Base
{
	public class Vec3
    {
        public static readonly Vec3 Ex = new Vec3(1, 0, 0);
        public static readonly Vec3 Ey = new Vec3(0, 1, 0);
        public static readonly Vec3 Ez = new Vec3(0, 0, 1);

        public static readonly Vec3 Zero = new Vec3(0, 0, 0);

        public Vec3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double Length        => Math.Sqrt(X*X + Y*Y + Z*Z);
        public double SquaredLength =>			 X*X + Y*Y + Z*Z;

        public override bool Equals(object obj) => this.Equals(obj, (v1, v2) => DoubleEquals(v1.X, v2.X) &&
                                                                                DoubleEquals(v1.Y, v2.Y) &&
                                                                                DoubleEquals(v1.Z, v2.Z));

        public override string ToString() => $"Vec3({DoubleFormat(X)}|{DoubleFormat(Y)}|{DoubleFormat(Z)})";

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();

        public static bool operator ==(Vec3 v1, Vec3 v2) => EqualsExtension.EqualsForEqualityOperator(v1, v2);
        public static bool operator !=(Vec3 v1, Vec3 v2) => !(v1 == v2);

        public static bool operator > (Vec3 v1, Vec3 v2) => (v1 != v2) && (v1.Length > v2.Length);
        public static bool operator >=(Vec3 v1, Vec3 v2) => (v1 == v2) || (v1 > v2);
        public static bool operator < (Vec3 v1, Vec3 v2) => (v1 != v2) && (v1.Length < v2.Length);
        public static bool operator <=(Vec3 v1, Vec3 v2) => (v1 == v2) || (v1 < v2);
        
        public static Vec3   operator +(Vec3 v1, Vec3 v2)  => new Vec3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Vec3   operator -(Vec3 v1, Vec3 v2)  => v1 + -v2;
        public static Vec3   operator -(Vec3 v)            => new Vec3(-v.X, -v.Y, -v.Z);
        public static double operator *(Vec3 v1, Vec3 v2)  => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        public static Vec3   operator *(Vec3 v, double d)  => new Vec3(v.X * d, v.Y * d, v.Z * d);
        public static Vec3   operator *(double d, Vec3 v)  => v * d;
        public static Vec3   operator /(Vec3 v,  double d) => v * (1 / d);
        public static bool   operator |(Vec3 v1, Vec3 v2)  => DoubleEquals(v1 * v2, 0.0, Constants.VectorPerpendicularPrecision);


        /// <summary>
        /// Crossproduct of v1 and v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vec3 operator %(Vec3 v1, Vec3 v2) => new Vec3(v1.Y * v2.Z - v1.Z * v2.Y,
                                                                    v1.Z * v2.X - v1.X * v2.Z,
                                                                    v1.X * v2.Y - v1.Y * v2.X);

        public static explicit operator CartesianCoordinate(Vec3 v)
        {
            return new CartesianCoordinate(v.X, v.Y, v.Z);
        }

        public static explicit operator PolarCoordinate(Vec3 v)
        {
            return (CartesianCoordinate) v;
        }

        public Vec3 Normalize()
        {
            return this / Length;
        }

        public Vec3 ProjectTo(Vec3 v)
        {
            return v * (this * v / (v.Length * v.Length));
        }

        public Angle GetAngleTo(Vec3 v)
        {
            return new Angle(new Radians(Math.Acos(this * v / (Length * v.Length))));
        }

        public static Angle GetAngleBetween(Vec3 v1, Vec3 v2)
        {
            return v1.GetAngleTo(v2);
        }

        public Vec3 RotateAroundCoorinateXAxis(Angle delta) => new Vec3(X,
                                                                        Y * delta.Cos - Z * delta.Sin,
                                                                        Y * delta.Sin + Z * delta.Cos);

        public Vec3 RotateAroundCoorinateYAxis(Angle delta) => new Vec3(X * delta.Cos + Z * delta.Sin,
                                                                        Y,
                                                                       -X * delta.Sin + Z * delta.Cos);

        public Vec3 RotateAroundCoorinateZAxis(Angle delta) => new Vec3(X * delta.Cos - Y * delta.Sin,
                                                                        X * delta.Sin + Y * delta.Cos,
                                                                        Z);
    }
}