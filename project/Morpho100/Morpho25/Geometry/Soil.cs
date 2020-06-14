using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;

namespace Morpho25.Geometry
{
    public class Soil : Entity
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
                          $"{nameof(value)}  must contains 1 material code.");

                _material = value;
            }

        }

        public Soil(FaceGroup geometry, int id, Grid grid, string name)
        {
            ID = id;
            Geometry = geometry;
            Material = CreateMaterial(Material.DEFAULT_SOIL);
            Name = name ?? "SoilGroup";

            SetMatrix(grid);
        }

        public Soil(FaceGroup geometry, int id, string code, Grid grid, string name)
        {
            ID = id;
            Geometry = geometry;
            Material = CreateMaterial(Material.DEFAULT_SOIL, code);
            Name = name ?? "SoilGroup";

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, Material.DEFAULT_SOIL);

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting(rays, Geometry, false, false);
            SetMatrix(intersection, grid, matrix, Material.IDs[0]);

            IDmatrix = matrix;
        }

        public override string ToString()
        {
            return String.Format("Soil::{0}::{1}::{2}", Name, ID, Material.IDs[0]);
        }
    }
}
