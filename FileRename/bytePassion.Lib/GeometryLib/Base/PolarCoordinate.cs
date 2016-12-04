using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.GeometryLib.Utils;
using bytePassion.Lib.Types.SemanticTypes;


namespace bytePassion.Lib.GeometryLib.Base
{

	public sealed class PolarCoordinate
    {
        public static readonly PolarCoordinate Origin = new PolarCoordinate(0.0, Angle.Zero, Angle.Zero);

        public PolarCoordinate(double radius, Angle theta, Angle phi)
        {

            Radius = radius;
            Theta = theta;
            Phi = phi;
        }

        public double Radius { get; }
        public Angle Theta   { get; }
        public Angle Phi     { get; }

        public override bool Equals(object obj) => this.Equals(obj, (p1, p2) => GeometryLibUtils.DoubleEquals(p1.Radius, p2.Radius) &&
                                                                                p1.Theta == p2.Theta &&
                                                                                p1.Phi == p2.Phi);

        public override string ToString() => $"PC({GeometryLibUtils.DoubleFormat(Radius)}, {Theta}, {Phi})";

        public override int GetHashCode() => Radius.GetHashCode() ^ Phi.GetHashCode() ^ Theta.GetHashCode();

        public PolarCoordinate AddTheta(Angle deltaTheta)
        {
            return new PolarCoordinate(Radius, Theta + deltaTheta, Phi);
        }

        public PolarCoordinate AddPhi(Angle deltaPhi)
        {
            if (Phi + deltaPhi > new Angle(new Degree(90)))
                return new PolarCoordinate(Radius, Theta + new Angle(new Degree(180)), new Angle(new Degree(180)) - (Phi + deltaPhi));

            if (Phi + deltaPhi < new Angle(new Degree(-90)))
                return new PolarCoordinate(Radius, Theta + new Angle(new Degree(180)), new Angle(new Degree(-180)) - (Phi + deltaPhi));

            return new PolarCoordinate(Radius, Theta, Phi + deltaPhi);
        }

        public PolarCoordinate AddRadius(double deltaRadius)
        {
            return new PolarCoordinate(Radius + deltaRadius, Theta, Phi);
        }

        public static bool operator ==(PolarCoordinate p1, PolarCoordinate p2) => EqualsExtension.EqualsForEqualityOperator(p1, p2);
        public static bool operator !=(PolarCoordinate p1, PolarCoordinate p2) => !(p1 == p2);

        public static PolarCoordinate operator +(PolarCoordinate p, Vec3 v)
        {
            CartesianCoordinate c = p;
            return c + v;
        }

        public static Vec3 operator -(PolarCoordinate p1, PolarCoordinate p2)
        {
            return (CartesianCoordinate) p1 - (CartesianCoordinate) p2;
        }

        public static implicit operator CartesianCoordinate(PolarCoordinate p)
        {
            var x = p.Radius * p.Phi.Cos * p.Theta.Cos;
            var y = p.Radius * p.Phi.Sin;
            var z = p.Radius * p.Phi.Cos * -p.Theta.Sin;

            return new CartesianCoordinate(x, y, z);
        }

        public static explicit operator Vec3(PolarCoordinate p)
        {
            var x = p.Radius * p.Phi.Cos * p.Theta.Cos;
            var y = p.Radius * p.Phi.Sin;
            var z = p.Radius * p.Phi.Cos * -p.Theta.Sin;

            return new Vec3(x, y, z);
        }
    }
}