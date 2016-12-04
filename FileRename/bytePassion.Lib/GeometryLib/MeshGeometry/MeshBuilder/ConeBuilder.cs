using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.Objects;
using bytePassion.Lib.GeometryLib.Utils;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder
{

    internal class ConeBuilder : IMeshBuilder
    {
        private readonly Cone cone;

        public ConeBuilder(Cone cone)
        {
            this.cone = cone;
        }
        
        public Vec3[] GetPositions()
        {

            var vecHeight = new Vec3(0, cone.Height, 0);
            var positions = new Vec3[cone.PointCount + 2];

            positions[0] = vecHeight / 2;
            positions[cone.PointCount + 1] = -vecHeight / 2;

            TesselationToolbox.GetCirclePointsOnXZPlane(-vecHeight / 2, cone.Radius, cone.PointCount, ref positions, 1);

            return positions;
        }

        public int[] GetIndecies()
        {
            var indecies = new int[cone.PointCount * 6];

            TesselationToolbox.GetIndeciesForTriangeFan(0, 1, cone.PointCount, ref indecies, 0, true);
            TesselationToolbox.GetIndeciesForTriangeFan(cone.PointCount + 1, 1, cone.PointCount, ref indecies,
                                                        cone.PointCount * 3, false);

            return indecies;
        }
    }
}