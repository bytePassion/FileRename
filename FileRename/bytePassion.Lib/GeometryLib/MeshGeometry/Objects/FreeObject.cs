using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.Objects
{

    public class FreeObject : GeometricObject
    {
        private Vec3[] positionsObjectSpace;
        private int[] indecies;

        private IMeshBuilder meshBuilder;

        public FreeObject(Vec3[] positionsObjectSpace, int[] indecies, CartesianCoordinate center = null, Orientation orientation = null) 
            : base(center, orientation)
        {
            PositionsObjectSpace = positionsObjectSpace;
            Indecies = indecies;
        }
        
        public Vec3[] PositionsObjectSpace { set { positionsObjectSpace = value; FireStructureChanged(); } get { return positionsObjectSpace; }}        
        public int[]  Indecies             { set { indecies             = value; FireStructureChanged(); } get { return indecies;             }}

        public override IMeshBuilder MeshBuilder
        {
            get { return meshBuilder ?? (meshBuilder = new FreeObjectBuilder(this)); }
        }

        public override GeometricObjectType Type
        {
            get { return GeometricObjectType.FreeObject; }
        }

        public override GeometricObject Clone()
        {
            return new FreeObject((Vec3[]) PositionsObjectSpace.Clone(), (int[]) Indecies.Clone(), Center, Orientation);
        }
    }

}
