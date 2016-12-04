using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.Objects;
using bytePassion.Lib.GeometryLib.Utils;
using System;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder
{

    public class SphereBuilder : IMeshBuilder
    {
        private readonly Sphere sphere;

        public SphereBuilder(Sphere sphere)
        {
            this.sphere = sphere;
        }
        
        public Vec3[] GetPositions()
        {

            var positions = new Vec3[sphere.CircleCount * sphere.PointsPerCircle + 2];
            var vecHeight = new Vec3(0, sphere.Radius, 0);

            positions[0] = vecHeight;
            positions[sphere.CircleCount * sphere.PointsPerCircle + 1] = -vecHeight;

            var step = (2 * vecHeight) / (sphere.CircleCount + 1);

            for (var i = 0; i < sphere.CircleCount; i++)
            {
                var vecHeightTmp = vecHeight - (i + 1) * step;
                var radiusTmp = Math.Sqrt(sphere.Radius * sphere.Radius - vecHeightTmp.Length * vecHeightTmp.Length);

                TesselationToolbox.GetCirclePointsOnXZPlane(vecHeightTmp,
                                                            radiusTmp,
                                                            sphere.PointsPerCircle,
                                                            ref positions,
                                                            i * sphere.PointsPerCircle + 1);
            }

            return positions;
        }

        public int[] GetIndecies()
        {
            var indecies = new int[(sphere.PointsPerCircle * 2) * 3 + (sphere.CircleCount - 1) * (2 * sphere.PointsPerCircle) * 3];

            TesselationToolbox.GetIndeciesForTriangeFan(0, 1, sphere.PointsPerCircle, ref indecies, 0, true);
            TesselationToolbox.GetIndeciesForTriangeFan(sphere.CircleCount * sphere.PointsPerCircle + 1,
                                                        ((sphere.CircleCount - 1) * sphere.PointsPerCircle) + 1,
                                                        sphere.PointsPerCircle, ref indecies, sphere.PointsPerCircle * 3,
                                                        false);

            for (var i = 0; i < sphere.CircleCount - 1; i++)
                TesselationToolbox.GetIndeciesForTriangeStripBetweenTwoPointCircles(i * sphere.PointsPerCircle + 1,
                                                                                    (i + 1) * sphere.PointsPerCircle + 1,
                                                                                    sphere.PointsPerCircle,
                                                                                    ref indecies,
                                                                                    sphere.PointsPerCircle * 6 +
                                                                                    i * sphere.PointsPerCircle * 6);
            return indecies;
        }
    }

}
