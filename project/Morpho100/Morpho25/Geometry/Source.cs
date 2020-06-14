using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;

namespace Morpho25.Geometry
{
    public class Source : Entity
    {
        public FaceGroup Geometry { get; }

        public override string Name { get; }

        public Matrix2d IDmatrix { get; private set; }

        public override Material Material
        {
            get { return _material; }
            protected set
            {
                if (value.IDs.Length != 1)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must contains 1 material code.");

                _material = value;
            }

        }

        public Source(FaceGroup geometry, int id, Grid grid, string name = " ")
        {
            ID = id;
            Geometry = geometry;
            Material = CreateMaterial(Material.DEFAULT_SOURCE);
            Name = name ?? "SourceGroup";

            SetMatrix(grid);
        }

        public Source(FaceGroup geometry, int id, string code, Grid grid, string name = " ")
        {
            ID = id;
            Geometry = geometry;
            Material = CreateMaterial(Material.DEFAULT_SOURCE, code);
            Name = name ?? "SourceGroup";

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting(rays, Geometry, false, false);
            SetMatrix(intersection, grid, matrix, Material.IDs[0]);

            IDmatrix = matrix;
        }

        public override string ToString()
        {
            return String.Format("Source::{0}::{1}::{2}", Name, ID, Material.IDs[0]);
        }
    }
}
