using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using System;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase
{
    public interface IGeometricObject
    {
        event EventHandler SizeChanged;
        event EventHandler StructureChanged;
        event EventHandler PositionChanged;
        event EventHandler OrientationChanged;
		
        GeometricObjectType Type        { get; }
        CartesianCoordinate Center      { get; }
        Orientation         Orientation { get; }
        Mesh3D              Mesh        { get; }
        BoundingBox         BoundingBox { get; }
		IMeshBuilder        MeshBuilder { get; }

		void Move (CartesianCoordinate newPosition);
		void Move (Vec3 deltaPosition);
		
		void RotateAroundXObjectAxis (Angle theta);
        void RotateAroundYObjectAxis (Angle theta);
        void RotateAroundZObjectAxis (Angle theta);

        void RotateAroundXWorldAxis  (Angle delta, CartesianCoordinate rotationAnchor);		
		void RotateAroundYWorldAxis  (Angle delta, CartesianCoordinate rotationAnchor);		
		void RotateAroundZWorldAxis  (Angle delta, CartesianCoordinate rotationAnchor);

		void SetDefaultOrientation ();		
	}
}
