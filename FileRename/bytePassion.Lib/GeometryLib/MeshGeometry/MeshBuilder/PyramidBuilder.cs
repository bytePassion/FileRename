using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.Objects;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder
{

    internal class PyramidBuilder : IMeshBuilder
    {
        readonly Pyramid pyramid;

        public PyramidBuilder (Pyramid pyramid)
        {
			this.pyramid = pyramid;
		}
        
		public Vec3[] GetPositions ()
        {
			var vecLength = new Vec3(pyramid.Length, 0, 0);
			var vecHeight = new Vec3(0, pyramid.Height, 0);
			var vecWidth  = new Vec3(0, 0, pyramid.Width);

			return new [] {  vecHeight/2 ,
							-vecLength/2 - vecHeight/2 + vecWidth/2,
							 vecLength/2 - vecHeight/2 + vecWidth/2,
							 vecLength/2 - vecHeight/2 - vecWidth/2,
							-vecLength/2 - vecHeight/2 - vecWidth/2 };			
		}

		public int[] GetIndecies () => DefaultIndecies;

        public static readonly int[] DefaultIndecies = { 0,1,2,		
						 		                         0,2,3,
						 		                         0,3,4,
								                         0,4,1,
								                         4,3,2,	
								                         4,2,1 };	
	}
}
