using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.Objects;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder
{

    internal class FrustrumOfPyramidBuilder : IMeshBuilder
    {
        readonly FrustrumOfPyramid frustrumOfPyramid;

        public FrustrumOfPyramidBuilder(FrustrumOfPyramid frustrumOfPyramid)
        {
            this.frustrumOfPyramid = frustrumOfPyramid;
        }        

		public Vec3[] GetPositions () {

			var vecLength1 = new Vec3(frustrumOfPyramid.Length1, 0, 0);
			var vecLength2 = new Vec3(frustrumOfPyramid.Length2, 0, 0);
			var vecHeight  = new Vec3(0, frustrumOfPyramid.Height, 0);
			var vecWidth1  = new Vec3(0, 0, frustrumOfPyramid.Width1);
			var vecWidth2  = new Vec3(0, 0, frustrumOfPyramid.Width2);

			return new[] { -vecLength2/2 + vecHeight/2 + vecWidth2/2,
						    vecLength2/2 + vecHeight/2 + vecWidth2/2,
						    vecLength2/2 + vecHeight/2 - vecWidth2/2,
						   -vecLength2/2 + vecHeight/2 - vecWidth2/2,
						   -vecLength1/2 - vecHeight/2 + vecWidth1/2,
						    vecLength1/2 - vecHeight/2 + vecWidth1/2,
						    vecLength1/2 - vecHeight/2 - vecWidth1/2,
						   -vecLength1/2 - vecHeight/2 - vecWidth1/2 };			
		}

		public int[] GetIndecies () => DefaultIndecies;

        public readonly static int[] DefaultIndecies =  { 1,0,4,	1,4,5,			
					 			                          2,1,5,	2,5,6,
					 			                          3,2,7,	2,6,7,
								                          0,3,7,	0,7,4,
								                          2,3,0,	2,0,1,
								                          7,6,5,	7,5,4 };
	}
}
