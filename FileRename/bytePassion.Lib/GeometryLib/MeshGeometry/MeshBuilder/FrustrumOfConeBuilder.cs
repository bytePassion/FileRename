using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.Objects;
using bytePassion.Lib.GeometryLib.Utils;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder
{

    internal class FrustrumOfConeBuilder : IMeshBuilder
    {
        private readonly FrustrumOfCone frustrumOfCone;

        public FrustrumOfConeBuilder(FrustrumOfCone fc)
        {
            frustrumOfCone = fc;
        }        

        public Vec3[] GetPositions()
        {
            var vecHeight = new Vec3(0, frustrumOfCone.Height, 0);
            var positions = new Vec3[frustrumOfCone.PointCount * 2 + 2];

            positions[0] = vecHeight / 2;
            positions[frustrumOfCone.PointCount * 2 + 1] = -vecHeight / 2;

            TesselationToolbox.GetCirclePointsOnXZPlane(vecHeight / 2, frustrumOfCone.Radius2,
                                                        frustrumOfCone.PointCount, ref positions, 1);
            TesselationToolbox.GetCirclePointsOnXZPlane(-vecHeight / 2, frustrumOfCone.Radius1,
                                                        frustrumOfCone.PointCount, ref positions,
                                                        frustrumOfCone.PointCount + 1);
            return positions;
        }

        public int[] GetIndecies()
        {
            var indecies = new int[frustrumOfCone.PointCount * 12];

            TesselationToolbox.GetIndeciesForTriangeFan(0, 1, frustrumOfCone.PointCount, ref indecies, 0, true);
            TesselationToolbox.GetIndeciesForTriangeFan(2 * frustrumOfCone.PointCount + 1,
                                                        frustrumOfCone.PointCount + 1, frustrumOfCone.PointCount,
                                                        ref indecies, frustrumOfCone.PointCount * 3, false);

            TesselationToolbox.GetIndeciesForTriangeStripBetweenTwoPointCircles(1, frustrumOfCone.PointCount + 1,
                                                                                frustrumOfCone.PointCount, ref indecies,
                                                                                frustrumOfCone.PointCount * 6);
            return indecies;
        }
    }

}
