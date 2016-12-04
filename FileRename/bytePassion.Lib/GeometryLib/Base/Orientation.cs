using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.Types.SemanticTypes;


namespace bytePassion.Lib.GeometryLib.Base
{

	public sealed class Orientation
    {

        public static readonly Orientation DefaultOrientation = new Orientation(new Vec3(1, 0, 0),
                                                                                new Vec3(0, 1, 0),
                                                                                new Vec3(0, 0, 1));

        public Orientation(Vec3 cX, Vec3 cY, Vec3 cZ)
        {
            X = cX;
            Y = cY;
            Z = cZ;
        }

        public Vec3 X { get; }
        public Vec3 Y { get; }
        public Vec3 Z { get; }

        public Vec3 ObjectSpaceToWorldSpace(Vec3 v, CartesianCoordinate objectCenter)
        {
            return ObjectSpaceToInertialSpace(v) + (Vec3) objectCenter;
        }

        public CartesianCoordinate ObjectSpaceToWorldSpace(CartesianCoordinate c, CartesianCoordinate objectCenter)
        {
            return (CartesianCoordinate) ObjectSpaceToWorldSpace((Vec3) c, objectCenter);
        }

        private Vec3 ObjectSpaceToInertialSpace(Vec3 v)
        {
            return v.X * X + v.Y * Y + v.Z * Z;
        }

        public CartesianCoordinate WorldSpaceToObjectSpace(CartesianCoordinate c, CartesianCoordinate objectCenter)
        {
            return (CartesianCoordinate) WorldSpaceToObjectSpace((Vec3) c, objectCenter);
        }

        public Vec3 WorldSpaceToObjectSpace(Vec3 v, CartesianCoordinate objectCenter)
        {
            return InertialSpaceToObjectSpace(v - (Vec3) objectCenter);
        }

        private Vec3 InertialSpaceToObjectSpace(Vec3 v)
        {
            var e1 = new Vec3(X.X, Y.X, Z.X);
            var e2 = new Vec3(X.Y, Y.Y, Z.Y);
            var e3 = new Vec3(X.Z, Y.Z, Z.Z);

            return v.X * e1 + v.Y * e2 + v.Z * e3;
        }        
        
        public Orientation RotateAroundXObjectAxis(Angle a)
        {
            var newZ = -a.Sin * Y + a.Cos * Z;
            var newY =  a.Cos * Y + a.Sin * Z;

            return new Orientation(X, newY, newZ);
        }

        public Orientation RotateAroundYObjectAxis(Angle a)
        {
            var newX = -a.Sin * Z + a.Cos * X;
            var newZ =  a.Cos * Z + a.Sin * X;

            return new Orientation(newX, Y, newZ);
        }

        public Orientation RotateAroundZObjectAxis(Angle a)
        {
            var newY = -a.Sin * X + a.Cos * Y;
            var newX =  a.Cos * X + a.Sin * Y;

            return new Orientation(newX, newY, Z);
        }

        public Orientation RotateAroundXWorldAxis(Angle a)
        {
            var newX = X.RotateAroundCoorinateXAxis(a);
            var newY = Y.RotateAroundCoorinateXAxis(a);
            var newZ = Z.RotateAroundCoorinateXAxis(a);

            return new Orientation(newX, newY, newZ);
        }

        public Orientation RotateAroundYWorldAxis(Angle a)
        {
            var newX = X.RotateAroundCoorinateYAxis(a);
            var newY = Y.RotateAroundCoorinateYAxis(a);
            var newZ = Z.RotateAroundCoorinateYAxis(a);

            return new Orientation(newX, newY, newZ);
        }

        public Orientation RotateAroundZWorldAxis(Angle a)
        {
            var newX = X.RotateAroundCoorinateZAxis(a);
            var newY = Y.RotateAroundCoorinateZAxis(a);
            var newZ = Z.RotateAroundCoorinateZAxis(a);

            return new Orientation(newX, newY, newZ);
        }

        public override bool   Equals(object obj) => this.Equals(obj, (o1, o2) => o1.X == o2.X && o1.Y == o2.Y && o1.Z == o2.Z);
        public override string ToString()         => $"Orientation({X}|{Y}|{Z})";
        public override int    GetHashCode()      => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();

        public static bool operator ==(Orientation o1, Orientation o2) => EqualsExtension.EqualsForEqualityOperator(o1, o2);
        public static bool operator !=(Orientation o1, Orientation o2) => !(o1 == o2);

        public Angle[] GetEulerRepresentation()
        {
            double m11 = X.X, m13 = X.Z;
            double m21 = Y.X, m22 = Y.Y, m23 = Y.Z;
            double m31 = Z.X, m33 = Z.Z;

            double h, p, b;

            var sp = -m23;

            if (sp <= -1.0f)            
                p = -System.Math.PI / 2.0;            
            else if (sp >= 1.0)            
                p = System.Math.PI / 2.0;            
            else            
                p = System.Math.Asin(sp);
            
            // Check for the Gimbal lock 
            if (sp > 0.99999)
            {
                b = 0.0f;
                h = System.Math.Atan2(-m31, m11);
            }
            else
            {
                h = System.Math.Atan2(m13, m33);
                b = System.Math.Atan2(m21, m22);
            }

            return new[]
                   {
                       new Angle(new Radians(h)),
                       new Angle(new Radians(p)),
                       new Angle(new Radians(b)),
                   };
        }
    }

}