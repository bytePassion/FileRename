using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.Types.SemanticTypes;


namespace bytePassion.Lib.GeometryLib.Utils
{

    public class TesselationToolbox
    {
        public static void GetCirclePointsOnXZPlane(Vec3 height, double radius, int pointCount, ref Vec3[] positions, int writeBegin)
        {
            var stepAround = new Angle(new Degree(180)) / (pointCount / 2.0);
            var step = new PolarCoordinate(radius, new Angle(new Degree(-90)), Angle.Zero);

            for (var i = 0; i < pointCount; i++)
            {
                positions[writeBegin + i] = (Vec3) (CartesianCoordinate) step + height;
                step = step.AddTheta(-stepAround);
            }
        }

        public static void GetIndeciesForTriangeFan(int indexMiddle, int startFan, int triangleCount, ref int[] indecies, int writeBegin, bool topView)
        {
            if (topView)
                for (var i = 0; i < triangleCount; i++)
                {
                    indecies[writeBegin + i * 3 + 0] = indexMiddle;
                    indecies[writeBegin + i * 3 + 2] = startFan + i;
                    if (i < triangleCount - 1)
                        indecies[writeBegin + i * 3 + 1] = startFan + i + 1;
                    else
                        indecies[writeBegin + i * 3 + 1] = startFan;
                }
            else
                for (var i = 0; i < triangleCount; i++)
                {
                    indecies[writeBegin + i * 3 + 0] = indexMiddle;

                    if (i < triangleCount - 1)
                        indecies[writeBegin + i * 3 + 2] = startFan + i + 1;
                    else
                        indecies[writeBegin + i * 3 + 2] = startFan;

                    indecies[writeBegin + i * 3 + 1] = startFan + i;
                }
        }

        public static void GetIndeciesForTriangeStripBetweenTwoPointCircles(int indexStartCircle1, int indexStartCircle2,
                                                                            int pointCountPerCercle,
                                                                            ref int[] indecies, int writeBegin)
        {
            for (var i = 0; i < pointCountPerCercle; i++)
            {
                indecies[writeBegin + i * 6 + 0] = indexStartCircle1 + i;
                indecies[writeBegin + i * 6 + 2] = indexStartCircle2 + i;

                if (i < pointCountPerCercle - 1)
                    indecies[writeBegin + i * 6 + 1] = indexStartCircle1 + i + 1;
                else
                    indecies[writeBegin + i * 6 + 1] = indexStartCircle1;

                if (i < pointCountPerCercle - 1)
                    indecies[writeBegin + i * 6 + 3] = indexStartCircle1 + i + 1;
                else
                    indecies[writeBegin + i * 6 + 3] = indexStartCircle1;

                indecies[writeBegin + i * 6 + 5] = indexStartCircle2 + i;

                if (i < pointCountPerCercle - 1)
                    indecies[writeBegin + i * 6 + 4] = indexStartCircle2 + i + 1;
                else
                    indecies[writeBegin + i * 6 + 4] = indexStartCircle2;
            }
        }

        public static int[] SwitchTriangles(int[] indecies)
        {
            var switchedIndecies = (int[]) indecies.Clone();

            for (var i = 0; i < switchedIndecies.Length; i += 3)
            {
                switchedIndecies[i] = indecies[i + 1];
                switchedIndecies[i + 1] = indecies[i];

                i += 3;
            }

            return switchedIndecies;
        }
    }
}
