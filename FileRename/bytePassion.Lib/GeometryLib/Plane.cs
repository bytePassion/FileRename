using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.GeometryLib.Base;
using static bytePassion.Lib.GeometryLib.Utils.GeometryLibUtils;


namespace bytePassion.Lib.GeometryLib
{
	public class Plane
    {
        public Plane(Vec3 normal, double distance)
        {
            Normal = normal;
            Distance = distance;
        }

        public Plane(CartesianCoordinate c1, CartesianCoordinate c2, CartesianCoordinate c3)
        {
            Normal = ((c2 - c1) % (c3 - c1)).Normalize();
            Distance = ((Vec3) c1) * Normal;
        }

        public Vec3   Normal   { get; }
        public double Distance { get; }

        public override bool   Equals(object obj) => this.Equals(obj, (p1, p2) => p1.IsParallelTo(p2) && DoubleEquals(p1.Distance, p2.Distance));
        public override string ToString()         => $"Plane: x * {Normal} = {DoubleFormat(Distance)}";
        public override int    GetHashCode()      => Normal.GetHashCode() ^ Distance.GetHashCode();

        private bool IsParallelTo(Plane p) => Normal.Equals(p.Normal) || Normal.Equals(-p.Normal);

        public static bool operator ==(Plane p1, Plane p2) => EqualsExtension.EqualsForEqualityOperator(p1, p2);
        public static bool operator !=(Plane p1, Plane p2) => !(p1 == p2);
    }
}