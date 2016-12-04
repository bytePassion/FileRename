using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.Objects
{

    public class Cuboid : GeometricObject
    {
        private double length;
        private double height;
        private double width;

        private IMeshBuilder meshBuilder;

        public Cuboid (double length, double height, double width, CartesianCoordinate center = null, Orientation orientation = null) 
            : base(center, orientation)
        {			
			this.length = length;
			this.height = height;			
			this.width  = width;
		}

		public override IMeshBuilder MeshBuilder
        {
			get { return meshBuilder ?? (meshBuilder = new CuboidBuilder(this)); }
		}

        public override GeometricObjectType Type
        {
            get { return GeometricObjectType.Cuboid; }
        }

        public double Length { set { length = value; FireSizeChanged(); } get { return length; }}
		public double Height { set { height = value; FireSizeChanged(); } get { return height; }}
		public double Width  { set { width  = value; FireSizeChanged(); } get { return width;  }}

		public override GeometricObject Clone ()
        {
			return new Cuboid(Length, Height, Width, Center, Orientation);
		}
	}
}
