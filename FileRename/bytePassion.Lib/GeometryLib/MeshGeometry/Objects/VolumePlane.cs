using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.Objects
{

    public class VolumePlane: GeometricObject
    {
        private double length;
        private double width;

        private IMeshBuilder meshBuilder;

        public VolumePlane (double length, double width, CartesianCoordinate center = null, Orientation orientation = null): 
			base(center, orientation)  {
			
			this.length = length;
			this.width  = width;
		}

        public override IMeshBuilder MeshBuilder
        {
            get { return meshBuilder ?? (meshBuilder = new VolumePlaneBuilder(this)); }
        }

        public override GeometricObjectType Type
        {
            get { return GeometricObjectType.Plane; }
        }

        public double Length { set { length = value; FireStructureChanged(); } get { return length; }}
		public double Width  { set { width  = value; FireStructureChanged(); } get { return width;  }}

        public override GeometricObject Clone()
        {
            return new VolumePlane(Length, Width, Center, Orientation);
        }
    }
}
