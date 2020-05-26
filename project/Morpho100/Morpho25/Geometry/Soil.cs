using Morpho25.Utility;
using System;


namespace Morpho25.Geometry
{
    public class Soil : Entity
    {
        public g3.DMesh3 Geometry { get; }

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

        public Soil(g3.DMesh3 geometry, int id, Grid grid, string name)
        {
            ID = id;
            Geometry = geometry;
            Material = CreateMaterial(Material.DEFAULT_SOIL);
            Name = name ?? "SoilGroup";

            SetMatrix(grid);
        }

        public Soil(g3.DMesh3 geometry, int id, string code, Grid grid, string name)
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

            var intersection = EnvimetUtility.Raycasting(Geometry, grid);
            SetMatrix(intersection, grid, matrix, Material.IDs[0]);

            IDmatrix = matrix;
        }

        public override string ToString()
        {
            return String.Format("Soil::{0}::{1}::{2}", Name, ID, Material.IDs[0]);
        }
    }
}
