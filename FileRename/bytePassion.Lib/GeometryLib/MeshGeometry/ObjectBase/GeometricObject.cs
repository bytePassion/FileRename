using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase;
using bytePassion.Lib.Types.Clonable;
using System;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase
{

    public abstract class GeometricObject : DisposingObject,
                                            IGeometricObject,                                            
                                            IGenericClonable<GeometricObject>
    {

        public event EventHandler SizeChanged;
        public event EventHandler StructureChanged;
        public event EventHandler PositionChanged;
        public event EventHandler OrientationChanged;

        protected GeometricObject(CartesianCoordinate center, Orientation orientation)
        {
            this.center = center ?? CartesianCoordinate.Origin;
            this.orientation = orientation ?? Orientation.DefaultOrientation;
        }

        public abstract GeometricObjectType Type { get; }
        public abstract IMeshBuilder MeshBuilder { get; }

        public abstract GeometricObject Clone();

        private CartesianCoordinate center;
        protected Orientation orientation;
        private Mesh3D mesh;
        private BoundingBox boundingbox;

        public CartesianCoordinate Center
        {
            get { return center; }
            private set
            {
                if (value != center)
                {
                    center = value;
                    FirePositionChanged();
                }
            }
        }

        public virtual Orientation Orientation
        {
            get { return orientation; }
            private set
            {
                if (value != orientation)
                {
                    orientation = value;
                    FireOrientationChanged();
                }
            }
        }        
       
        public Mesh3D Mesh
        {
            get { return mesh ?? (mesh = new Mesh3D(this)); }           
        }        

        public BoundingBox BoundingBox
        {
            get { return boundingbox ?? (boundingbox = new BoundingBox(this, true)); }
        }

        public void Move(CartesianCoordinate newPosition)
        {
            Center = newPosition;
        }

        public void Move(Vec3 deltaPosition)
        {
            Center += deltaPosition;
        }

        public void RotateAroundXWorldAxis(Angle delta, CartesianCoordinate rotationAnchor)
        {
            var centerDisplacement = Center - rotationAnchor;
            Orientation = orientation.RotateAroundXWorldAxis(delta);

            var rotatedCenterDisplacement = centerDisplacement.RotateAroundCoorinateXAxis(delta);
            Center = rotationAnchor + rotatedCenterDisplacement;
        }

        public void RotateAroundYWorldAxis(Angle delta, CartesianCoordinate rotationAnchor)
        {
            var centerDisplacement = Center - rotationAnchor;
            Orientation = orientation.RotateAroundYWorldAxis(delta);

            var rotatedCenterDisplacement = centerDisplacement.RotateAroundCoorinateYAxis(delta);
            Center = rotationAnchor + rotatedCenterDisplacement;
        }

        public void RotateAroundZWorldAxis(Angle delta, CartesianCoordinate rotationAnchor)
        {
            var centerDisplacement = Center - rotationAnchor;
            Orientation = orientation.RotateAroundZWorldAxis(delta);

            var rotatedCenterDisplacement = centerDisplacement.RotateAroundCoorinateZAxis(delta);
            Center = rotationAnchor + rotatedCenterDisplacement;
        }

        public void RotateAroundXObjectAxis(Angle theta) { Orientation = orientation.RotateAroundXObjectAxis(theta); }
        public void RotateAroundYObjectAxis(Angle theta) { Orientation = orientation.RotateAroundYObjectAxis(theta); }
        public void RotateAroundZObjectAxis(Angle theta) { Orientation = orientation.RotateAroundZObjectAxis(theta); }

        public void SetDefaultOrientation() { Orientation = Orientation.DefaultOrientation; }

        protected override void CleanUp()
        {
            mesh?.Dispose();
            boundingbox?.Dispose();
        }

        protected void FireSizeChanged()
        {
            SizeChanged?.Invoke(this, new EventArgs());
        }

        protected void FireStructureChanged()
        {
            StructureChanged?.Invoke(this, new EventArgs());
        }

        protected void FirePositionChanged()
        {
            PositionChanged?.Invoke(this, new EventArgs());
        }

        protected void FireOrientationChanged()
        {
            OrientationChanged?.Invoke(this, new EventArgs());
        }
    }
}
