using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;
using System;
using System.Collections.Generic;


namespace bytePassion.Lib.GeometryLib.MeshGeometry.MeshBase
{

    public class Mesh3D : DisposingObject
    {
        public event EventHandler WorldSpacePositionsChanged;

        private readonly IMeshBuilder meshBuilder;
        private readonly IGeometricObject geoObject;

        private double[] minMaxWorldCoordinates;       

        public Mesh3D(IGeometricObject geoObject)
        {

            this.geoObject = geoObject;
            meshBuilder = geoObject.MeshBuilder;

            PositionsObjectSpace = meshBuilder.GetPositions();
            Indecies = meshBuilder.GetIndecies();

            BuildWorldSpaceMash(geoObject.Orientation, geoObject.Center);

            geoObject.SizeChanged        += OnGeoObjectSizeChanged;
            geoObject.StructureChanged   += OnGeoObjectStructureChanged;
            geoObject.PositionChanged    += OnGeoObjectOrientationOrPositionChanged;
            geoObject.OrientationChanged += OnGeoObjectOrientationOrPositionChanged;
        }
       
        private void OnGeoObjectOrientationOrPositionChanged(object sender, EventArgs eventArgs)
        {
            var go = (GeometricObject) sender;

            BuildWorldSpaceMash(go.Orientation, go.Center);            
        }

        private void OnGeoObjectStructureChanged(object sender, EventArgs eventArgs)
        {
            var go = (GeometricObject) sender;

            PositionsObjectSpace = meshBuilder.GetPositions();
            Indecies = meshBuilder.GetIndecies();
            BuildWorldSpaceMash(go.Orientation, go.Center);            
        }

        private void OnGeoObjectSizeChanged(object sender, EventArgs eventArgs)
        {
            var go = (GeometricObject) sender;

            PositionsObjectSpace = meshBuilder.GetPositions();
            BuildWorldSpaceMash(go.Orientation, go.Center);            
        }

        public Vec3[] PositionsObjectSpace { get; private set; }
        public Vec3[] PositionsWorldSpace  { get; private set; }
        public int[]  Indecies             { get; private set; }

        private void BuildWorldSpaceMash(Orientation orientation, CartesianCoordinate position)
        {
            PositionsWorldSpace = new Vec3[PositionsObjectSpace.Length];

            for (int i = 0; i < PositionsObjectSpace.Length; i++)
            {
                PositionsWorldSpace[i] = orientation.ObjectSpaceToWorldSpace(PositionsObjectSpace[i], position);
            }

            minMaxWorldCoordinates = null;

            FireWorldSpacePositionChanged();
        }


        private static double[] ComputeMinMax(IReadOnlyList<Vec3> positions)
        {
            double minX = positions[0].X;
            double minY = positions[0].Y;
            double minZ = positions[0].Z;

            double maxX = positions[0].X;
            double maxY = positions[0].Y;
            double maxZ = positions[0].Z;

            foreach (var vec in positions)
            {
                if (vec.X < minX) minX = vec.X;     if (vec.X > maxX) maxX = vec.X;
                if (vec.Y < minY) minY = vec.Y;     if (vec.Y > maxY) maxY = vec.Y;
                if (vec.Z < minZ) minZ = vec.Z;     if (vec.Z > maxZ) maxZ = vec.Z;
            }

            return new[]
                   {
                       minX, maxX,
                       minY, maxY,
                       minZ, maxZ
                   };
        }       
               
        public double[] MinMaxWorldCoordinates
        {
            get { return minMaxWorldCoordinates ?? (minMaxWorldCoordinates = ComputeMinMax(PositionsWorldSpace)); }
        }

        private void FireWorldSpacePositionChanged()
        {
            WorldSpacePositionsChanged?.Invoke(this, new EventArgs());
        }

        protected override void CleanUp()
        {
            geoObject.SizeChanged        -= OnGeoObjectSizeChanged;
            geoObject.StructureChanged   -= OnGeoObjectStructureChanged;
            geoObject.PositionChanged    -= OnGeoObjectOrientationOrPositionChanged;
            geoObject.OrientationChanged -= OnGeoObjectOrientationOrPositionChanged;
        }
    }
}
