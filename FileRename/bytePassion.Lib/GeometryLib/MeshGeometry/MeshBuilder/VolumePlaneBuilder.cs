using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.Objects;
using System;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBuilder
{

    internal class VolumePlaneBuilder : IMeshBuilder
    {
        private const double PlaneThickness      =  0.05;
        private const double TesselationQuadSize = 50.0;

        private readonly VolumePlane plane;

        public VolumePlaneBuilder(VolumePlane plane)
        {
            this.plane = plane;
        }
                       
        public Vec3[] GetPositions()
        {           
            var divisionFactorX = (int) Math.Floor(plane.Length / TesselationQuadSize) + 1;
            var divisionFactorZ = (int) Math.Floor(plane.Width  / TesselationQuadSize) + 1;

            int pointsX = divisionFactorX + 1;
            int pointsZ = divisionFactorZ + 1;
            int pointsPerPlane = pointsX * pointsZ;

            var vecLength = new Vec3(plane.Length, 0, 0);
            var vecHeight = new Vec3(0, PlaneThickness, 0);
            var vecWidth = new Vec3(0, 0, plane.Width);

            var stepX = new Vec3(plane.Length / divisionFactorX, 0, 0);
            var stepZ = new Vec3(0, 0, plane.Width / divisionFactorZ);

            var positions = new Vec3[pointsPerPlane * 2];

            var currentVec = -vecLength / 2 + vecHeight / 2 - vecWidth / 2;
            for (var x = 0; x < pointsX; x++)
            {

                var xtmp = currentVec;

                for (var z = 0; z < pointsZ; z++)
                {

                    positions[x * pointsZ + z] = currentVec;

                    currentVec += stepZ;
                }
                currentVec = xtmp + stepX;
            }

            for (var i = 0; i < pointsPerPlane; i++)
            {
                positions[i + pointsPerPlane] = positions[i] - vecHeight;
            }            

            return positions;
        }

        public int[] GetIndecies()
        {
            var divisionFactorX = (int)Math.Floor(plane.Length / TesselationQuadSize) + 1;
            var divisionFactorZ = (int)Math.Floor(plane.Width / TesselationQuadSize) + 1;

            int pointsX = divisionFactorX + 1;
            int pointsZ = divisionFactorZ + 1;
            int pointsPerPlane = pointsX * pointsZ;

            var indecies = new int[(pointsX - 1) * (pointsZ - 1) * 12 + (pointsX - 1) * 12 + (pointsZ - 1) * 12];           

            for (var x = 0; x < pointsX - 1; x++)
                for (var z = 0; z < pointsZ - 1; z++)
                {
                    var position = x * (pointsZ - 1) + z;

                    var upleft = x * pointsZ + z;

                    indecies[position * 12 + 0] = upleft;
                    indecies[position * 12 + 1] = upleft + 1;
                    indecies[position * 12 + 2] = upleft + pointsZ + 1;

                    indecies[position * 12 + 3] = upleft + pointsZ + 1;
                    indecies[position * 12 + 4] = upleft + pointsZ;
                    indecies[position * 12 + 5] = upleft;


                    indecies[position * 12 + 6] = pointsPerPlane + upleft;
                    indecies[position * 12 + 7] = pointsPerPlane + upleft + pointsZ + 1;
                    indecies[position * 12 + 8] = pointsPerPlane + upleft + 1;

                    indecies[position * 12 + 9] = pointsPerPlane + upleft + pointsZ + 1;
                    indecies[position * 12 + 10] = pointsPerPlane + upleft;
                    indecies[position * 12 + 11] = pointsPerPlane + upleft + pointsZ;
                }

            var startIndex = (pointsX - 1) * (pointsZ - 1) * 12;

            for (var x = 0; x < pointsX - 1; x++)
            {
                var upLeft1 = x * (pointsZ) + pointsZ - 1;
                var upRight1 = x * (pointsZ) + pointsZ - 1 + pointsZ;
                var downLeft1 = x * (pointsZ) + pointsZ - 1 + pointsPerPlane;
                var downRight1 = x * (pointsZ) + pointsZ - 1 + pointsZ + pointsPerPlane;

                var upLeft2 = x * (pointsZ);
                var upRight2 = x * (pointsZ) + pointsZ;
                var downLeft2 = x * (pointsZ) + pointsPerPlane;
                var downRight2 = x * (pointsZ) + pointsZ + pointsPerPlane;

                indecies[startIndex + x * 12 + 0] = upLeft1;
                indecies[startIndex + x * 12 + 1] = downLeft1;
                indecies[startIndex + x * 12 + 2] = downRight1;

                indecies[startIndex + x * 12 + 3] = downRight1;
                indecies[startIndex + x * 12 + 4] = upRight1;
                indecies[startIndex + x * 12 + 5] = upLeft1;

                indecies[startIndex + x * 12 + 6] = upLeft2;
                indecies[startIndex + x * 12 + 7] = downRight2;
                indecies[startIndex + x * 12 + 8] = downLeft2;

                indecies[startIndex + x * 12 + 9] = downRight2;
                indecies[startIndex + x * 12 + 10] = upLeft2;
                indecies[startIndex + x * 12 + 11] = upRight2;
            }

            startIndex += (pointsX - 1) * 12;

            for (var z = 0; z < pointsZ - 1; z++)
            {
                var upLeft1 = z;
                var upRight1 = z + 1;
                var downLeft1 = z + pointsPerPlane;
                var downRight1 = z + 1 + pointsPerPlane;

                var upLeft2 = (pointsX - 1) * pointsZ + z + 1;
                var upRight2 = (pointsX - 1) * pointsZ + z;
                var downLeft2 = (pointsX - 1) * pointsZ + z + 1 + pointsPerPlane;
                var downRight2 = (pointsX - 1) * pointsZ + z + pointsPerPlane;

                indecies[startIndex + z * 12 + 0] = upLeft1;
                indecies[startIndex + z * 12 + 1] = downLeft1;
                indecies[startIndex + z * 12 + 2] = downRight1;

                indecies[startIndex + z * 12 + 3] = downRight1;
                indecies[startIndex + z * 12 + 4] = upRight1;
                indecies[startIndex + z * 12 + 5] = upLeft1;

                indecies[startIndex + z * 12 + 6] = upLeft2;
                indecies[startIndex + z * 12 + 7] = downLeft2;
                indecies[startIndex + z * 12 + 8] = downRight2;

                indecies[startIndex + z * 12 + 9] = downRight2;
                indecies[startIndex + z * 12 + 10] = upRight2;
                indecies[startIndex + z * 12 + 11] = upLeft2;
            }

            return indecies;
        }
    }
}
