using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;
using System;


namespace bytePassion.Lib.GeometryLib.MeshGeometry
{

    public class BoundingBox : DisposingObject
    {
        public event EventHandler MinMaxChanged;

        private readonly IGeometricObject geometricObject;
        private readonly bool keepUpdated;


        public BoundingBox(IGeometricObject geometricObject, bool keepUpdated)
        {
            this.geometricObject = geometricObject;
            this.keepUpdated = keepUpdated;

            ComputeBoundingBox(geometricObject.Mesh, true);

            if (keepUpdated)
                geometricObject.Mesh.WorldSpacePositionsChanged += OnMeshChanged;
        }

        private void ComputeBoundingBox(Mesh3D mesh, bool isInitial)
        {
            var minmax = mesh.MinMaxWorldCoordinates;

            Min = new CartesianCoordinate(minmax[0], minmax[2], minmax[4]);
            Max = new CartesianCoordinate(minmax[1], minmax[3], minmax[5]);

            if (!isInitial)
             FireMinMaxChanged();
        }

        private void OnMeshChanged(object sender, EventArgs eventArgs)
        {
            ComputeBoundingBox((Mesh3D) sender, false);
        }

        public CartesianCoordinate Min { get; private set; }
        public CartesianCoordinate Max { get; private set; }

        public CartesianCoordinate Center
        {
            get { return (CartesianCoordinate) (((Vec3) Max + (Vec3) Min) / 2.0); }
        }

        public bool IsIntersecting(BoundingBox b)
        {
            return AreBoxesIntersecting(this, b);            
        }

        protected override void CleanUp()
        {
            if (keepUpdated)
                geometricObject.Mesh.WorldSpacePositionsChanged -= OnMeshChanged;
        }

        private void FireMinMaxChanged()
        {
            MinMaxChanged?.Invoke(this, new EventArgs());
        }

        private static bool AreBoxesIntersecting(BoundingBox b1, BoundingBox b2)
        {
            // TODO: improvable

            bool intersecX = false;
            bool intersecY = false;
            bool intersecZ = false;

            if (b1.Min.X < b2.Min.X)
                if (b1.Max.X > b2.Min.X)
                    intersecX = true;

            if (b2.Min.X < b1.Min.X)
                if (b2.Max.X > b1.Min.X)
                    intersecX = true;

            if (b1.Min.Y < b2.Min.Y)
                if (b1.Max.Y > b2.Min.Y)
                    intersecY = true;

            if (b2.Min.Y < b1.Min.Y)
                if (b2.Max.Y > b1.Min.Y)
                    intersecY = true;

            if (b1.Min.Z < b2.Min.Z)
                if (b1.Max.Z > b2.Min.Z)
                    intersecZ = true;

            if (b2.Min.Z < b1.Min.Z)
                if (b2.Max.Z > b1.Min.Z)
                    intersecZ = true;

            return intersecX && intersecY && intersecZ;
        }
    }

}
