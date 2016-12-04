using bytePassion.Lib.GeometryLib.Base;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase
{

    public interface IMeshBuilder
    {
        Vec3[] GetPositions();
        int[] GetIndecies();
    }
}