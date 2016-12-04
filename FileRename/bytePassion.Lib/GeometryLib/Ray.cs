using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.GeometryLib.Base;


namespace bytePassion.Lib.GeometryLib
{
	public sealed class Ray
    {
        public Ray(CartesianCoordinate c1, CartesianCoordinate c2) 
            : this(c1, c2 - c1)
        {            
        }

        public Ray(CartesianCoordinate origin, Vec3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public CartesianCoordinate Origin    { get; }
        public Vec3                Direction { get; }

        public override bool   Equals(object obj) => this.Equals(obj, (r1, r2) => r1.Direction.Normalize() == r2.Direction.Normalize() && r1.Origin == r2.Origin);
        public override string ToString()         => $"Ray(t) = {Origin} + t * {Direction}";
        public override int    GetHashCode()      => Origin.GetHashCode() ^ Direction.GetHashCode();

        public static bool operator ==(Ray r1, Ray r2) => EqualsExtension.EqualsForEqualityOperator(r1, r2);
        public static bool operator !=(Ray r1, Ray r2) => !(r1 == r2);

        public CartesianCoordinate GetPointOnRay(double t)
        {
            return t < 0 
                ? null 
                : Origin + Direction * t;
        }

        public double GetTEquivalentOfInterval(double interval)
        {
            return interval / Direction.Length;
        }

        public Ray RayWithNewLenthToDirectionVector(double newLength)
        {
            return new Ray(Origin, Direction.Normalize() * newLength);
        }
    }
}
