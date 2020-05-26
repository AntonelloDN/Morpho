using Morpho25.Utility;
using System;


namespace Morpho25.Geometry
{
    public class Terrain : Entity
    {
        public g3.DMesh3 Geometry { get; }

        public override string Name { get; }

        public Matrix2d IDmatrix { get; private set; }

        public override Material Material { get => throw new NotImplementedException();
            protected set => throw new NotImplementedException(); }

        public Terrain(g3.DMesh3 geometry, int id, Grid grid, string name)
        {
            ID = id;
            Geometry = geometry;
            Name = name ?? "TerrainGroup";

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");

            var intersection = EnvimetUtility.Raycasting(Geometry, grid);
            SetMatrix(intersection, grid, matrix, "");

            IDmatrix = matrix;
        }

        public override string ToString()
        {
            return String.Format("Terrain::{0}::{1}", Name, ID);
        }
    }
}
