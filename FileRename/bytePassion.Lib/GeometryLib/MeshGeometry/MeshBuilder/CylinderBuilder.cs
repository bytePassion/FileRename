using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.Objects;
using bytePassion.Lib.GeometryLib.Utils;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder
{

    internal class CylinderBuilder : IMeshBuilder
    {
        private readonly Cylinder cylinder;

        public CylinderBuilder(Cylinder cylinder)
        {
            this.cylinder = cylinder;
        }       

        public Vec3[] GetPositions()
        {
            var vecHeight = new Vec3(0, cylinder.Height, 0);
            var positions = new Vec3[cylinder.PointCount * 2 + 2];

            positions[0] = vecHeight / 2;
            positions[cylinder.PointCount * 2 + 1] = -vecHeight / 2;

            TesselationToolbox.GetCirclePointsOnXZPlane(vecHeight / 2, cylinder.Radius, cylinder.PointCount,
                                                        ref positions, 1);
            TesselationToolbox.GetCirclePointsOnXZPlane(-vecHeight / 2, cylinder.Radius, cylinder.PointCount,
                                                        ref positions, cylinder.PointCount + 1);
            return positions;
        }

        public int[] GetIndecies()
        {
            var indecies = new int[cylinder.PointCount * 12];

            TesselationToolbox.GetIndeciesForTriangeFan(0, 1, cylinder.PointCount, ref indecies, 0, true);
            TesselationToolbox.GetIndeciesForTriangeFan(2 * cylinder.PointCount + 1, cylinder.PointCount + 1,
                                                        cylinder.PointCount, ref indecies, cylinder.PointCount * 3,
                                                        false);

            TesselationToolbox.GetIndeciesForTriangeStripBetweenTwoPointCircles(1, cylinder.PointCount + 1,
                                                                                cylinder.PointCount, ref indecies,
                                                                                cylinder.PointCount * 6);
            return indecies;
        }
    }
}
