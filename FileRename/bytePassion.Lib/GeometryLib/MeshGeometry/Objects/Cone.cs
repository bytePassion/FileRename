using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.Objects
{

    public class Cone : GeometricObject
    {

        private double height;
        private double radius;
        private IMeshBuilder meshBuilder;

        private int pointCount;

        public Cone(double radius, double height, int pointCount, CartesianCoordinate center = null, Orientation orientation = null) 
            : base(center, orientation)
        {

            this.height = height;
            this.radius = radius;
            this.pointCount = pointCount;
        }
        
        public override IMeshBuilder MeshBuilder
        {
            get { return meshBuilder ?? (meshBuilder = new ConeBuilder(this)); }
        }

        public override GeometricObjectType Type
        {
            get { return GeometricObjectType.Cone; }
        }
        
        public double Height     { set { height     = value; FireSizeChanged();      } get { return height;     }}
        public double Radius     { set { radius     = value; FireSizeChanged();      } get { return radius;     }}
        public int    PointCount { set { pointCount = value; FireStructureChanged(); } get { return pointCount; }}

        public override GeometricObject Clone()
        {
            return new Cone(Radius, Height, PointCount, Center, Orientation);
        }
    }
}
