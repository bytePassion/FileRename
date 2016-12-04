using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.Objects;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder
{

    internal class CuboidBuilder : IMeshBuilder
    {
        private readonly Cuboid cuboid;

        public CuboidBuilder (Cuboid cuboid)
        {
			this.cuboid = cuboid;
		}                

		public Vec3[] GetPositions ()
        {
			var vecLength = new Vec3(cuboid.Length, 0, 0);
			var vecHeight = new Vec3(0, cuboid.Height, 0);
			var vecWidth  = new Vec3(0, 0, cuboid.Width);

			return new[] { -vecLength/2 + vecHeight/2 + vecWidth/2,
						    vecLength/2 + vecHeight/2 + vecWidth/2,
						    vecLength/2 + vecHeight/2 - vecWidth/2,
						   -vecLength/2 + vecHeight/2 - vecWidth/2,
						   -vecLength/2 - vecHeight/2 + vecWidth/2,
						    vecLength/2 - vecHeight/2 + vecWidth/2,
						    vecLength/2 - vecHeight/2 - vecWidth/2,
						   -vecLength/2 - vecHeight/2 - vecWidth/2 };			
		}

        public int[] GetIndecies() => DefaultIndecies;

        public readonly static int[] DefaultIndecies = { 1,0,4,	    1,4,5,			
						 		                         2,1,5,	    2,5,6,
						 		                         3,2,7,	    2,6,7,
								                         0,3,7,	    0,7,4,
								                         2,3,0,	    2,0,1,
								                         7,6,5,	    7,5,4 };	
	}
}