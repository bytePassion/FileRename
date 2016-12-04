using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.Objects
{

    public class FrustrumOfPyramid: GeometricObject
    {
        private double length1;
        private double length2;
        private double height;
        private double width1;
        private double width2;

        private IMeshBuilder meshBuilder;

        public FrustrumOfPyramid (double length1, double length2, double height, double width1, double width2, 
								  CartesianCoordinate center = null, Orientation orientation = null) 
            : base(center, orientation)
        {			
			this.length1 = length1;
			this.length2 = length2;
			this.height  = height;
			this.width1  = width1;
			this.width2  = width2;
		}

        public override IMeshBuilder MeshBuilder
        {
            get { return meshBuilder ?? (meshBuilder = new FrustrumOfPyramidBuilder(this)); }
        }

        public override GeometricObjectType Type
        {
            get { return GeometricObjectType.FrustrumOfPyramid; }
        }

        public double Length1 { set { length1 = value; FireSizeChanged(); } get { return length1; }}
		public double Length2 { set { length2 = value; FireSizeChanged(); } get { return length2; }}
		public double Height  { set { height  = value; FireSizeChanged(); } get { return height;  }}
		public double Width1  { set { width1  = value; FireSizeChanged(); } get { return width1;  }}
		public double Width2  { set { width2  = value; FireSizeChanged(); } get { return width2;  }}

        public override GeometricObject Clone()
        {
            return new FrustrumOfPyramid(Length1, Length2, Height, Width1, Width2, Center, Orientation);
        }
    }
}
