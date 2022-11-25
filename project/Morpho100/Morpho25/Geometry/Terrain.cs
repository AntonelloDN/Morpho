using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;

namespace Morpho25.Geometry
{
    public class Terrain : Entity
    {
        public FaceGroup Geometry { get; }

        public override string Name { get; }

        public Matrix2d IDmatrix { get; private set; }

        public override Material Material { get => throw new NotImplementedException();
            protected set => throw new NotImplementedException(); }

        public Terrain(Grid grid, FaceGroup geometry, int id, string name)
        {
            ID = id;
            Geometry = geometry;
            Name = name ?? "TerrainGroup";

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting2D(rays, Geometry, true, false);
            SetMatrix(intersection, grid, matrix, "");

            IDmatrix = matrix;
        }

        public override string ToString()
        {
            return String.Format("Terrain::{0}::{1}", Name, ID);
        }
    }
}
