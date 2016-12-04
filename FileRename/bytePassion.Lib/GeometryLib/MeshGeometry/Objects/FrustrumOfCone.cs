using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.Objects
{

    public class FrustrumOfCone : GeometricObject
    {
        private double radius1;
        private double radius2;
        private double height;

        private int pointCount;

        private IMeshBuilder meshBuilder;

        public FrustrumOfCone (double radius1, double radius2, double height, int pointCount, CartesianCoordinate center = null, Orientation orientation = null) 
            : base(center, orientation)
        {			
			this.radius1 = radius1;
			this.radius2 = radius2;			
			this.height  = height;
			this.pointCount = pointCount;
		}

        public override IMeshBuilder MeshBuilder
        {
            get { return meshBuilder ?? (meshBuilder = new FrustrumOfConeBuilder(this)); }
        }

        public override GeometricObjectType Type
        {
            get { return GeometricObjectType.FrustrumOfCone; }
        }
       
		public double Radius1    { set { radius1    = value; FireSizeChanged();      } get { return radius1;    }}
		public double Radius2    { set { radius2    = value; FireSizeChanged();      } get { return radius2;    }}
		public double Height     { set { height     = value; FireSizeChanged();      } get { return height;     }}
		public int    PointCount { set { pointCount = value; FireStructureChanged(); } get { return pointCount; }}

        public override GeometricObject Clone()
        {
            return new FrustrumOfCone(Radius1, Radius2, Height, PointCount, Center, Orientation);
        }
    }
}
