using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.Objects
{

    public class Sphere: GeometricObject
    {
        private double radius;

        private int circleCount;
        private int pointsPerCircle;

        private IMeshBuilder meshBuilder;

        public Sphere (double radius, int circleCount, int pointsPerCircle, CartesianCoordinate center = null, Orientation orientation = null) 
            : base(center, orientation)
        {			
			this.radius = radius;
			this.pointsPerCircle = pointsPerCircle;
			this.circleCount = circleCount;
		}

        public override IMeshBuilder MeshBuilder
        {
            get { return meshBuilder ?? (meshBuilder = new SphereBuilder(this)); }
        }

        public override GeometricObjectType Type
        {
            get { return GeometricObjectType.Sphere; }
        }

        public double Radius          {	set { radius          = value; FireSizeChanged();      } get { return radius;          }}
		public int    PointsPerCircle { set { pointsPerCircle = value; FireStructureChanged(); } get { return pointsPerCircle; }}
		public int    CircleCount     { set { circleCount     = value; FireStructureChanged(); } get { return circleCount;     }}

        public override GeometricObject Clone()
        {
            return new Sphere(Radius, CircleCount, PointsPerCircle, Center, Orientation);
        }
    }
}
