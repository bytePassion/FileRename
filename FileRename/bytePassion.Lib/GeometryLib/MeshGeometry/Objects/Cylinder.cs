using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.Objects
{

    public class Cylinder : GeometricObject
    {
        private double radius;
        private double height;

        private int pointCount;
        private IMeshBuilder meshBuilder;

        public Cylinder (double radius, double height, int pointCount=50, CartesianCoordinate center = null, Orientation orientation=null)
            : base(center, orientation)
        {			
			this.radius = radius;
			this.height = height;
			this.pointCount = pointCount;			
		}

        public override IMeshBuilder MeshBuilder
        {
            get { return meshBuilder ?? (meshBuilder = new CylinderBuilder(this)); }
        }

        public override GeometricObjectType Type
        {
            get { return GeometricObjectType.Cylinder; }
        }        

        public double Radius     { set { radius     = value; FireSizeChanged();      } get { return radius;     }}
		public double Height     { set { height     = value; FireSizeChanged();      } get { return height;     }}
		public int    PointCount { set { pointCount = value; FireStructureChanged(); } get { return pointCount; }}

		public override GeometricObject Clone ()
        {
			return new Cylinder(Radius, Height, PointCount, Center, Orientation);
		}
	}
}
