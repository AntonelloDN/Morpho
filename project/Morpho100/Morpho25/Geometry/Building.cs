using Morpho25.Utility;
using System;

namespace Morpho25.Geometry
{
    public class Building : Entity
    {
        public g3.DMesh3 Geometry { get; }

        public Matrix2d TopMatrix { get; private set; }
        public Matrix2d BottomMatrix { get; private set; }
        public Matrix2d IDmatrix { get; private set; }

        public override Material Material {
            get { return _material; }
            protected set
            {
                if (value.IDs.Length != 4)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} should contain 4 materials. Use 'Building.CreateMaterial' method");

                _material = value;
            }

        }
        public override string Name { get; }

        public Building(g3.DMesh3 geometry, int id, Grid grid, string name = " ")
        {
            ID = id;
            Geometry = geometry;
            Material = CreateMaterial(null, null, null, null);
            Name = name;

            SetMatrix(grid);
        }

        public Building(g3.DMesh3 geometry, int id, Material material, Grid grid, string name = " ")
        {
            ID = id;
            Geometry = geometry;
            Material = material;
            Name = name;

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d topMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");
            Matrix2d bottomMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");
            Matrix2d idMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");

            var intersectionTop = EnvimetUtility.Raycasting(Geometry, grid);
            var intersectionBottom = EnvimetUtility.Raycasting(Geometry, grid, top: false);

            SetMatrix(intersectionTop, grid, topMatrix, "");
            SetMatrix(intersectionBottom, grid, bottomMatrix, "");
            SetMatrix(intersectionTop, grid, idMatrix, ID.ToString());

            TopMatrix = topMatrix;
            BottomMatrix = bottomMatrix;
            IDmatrix = idMatrix;
        }

        public override string ToString()
        {
            string name = (Name != " ") ? Name : "Building";
            return String.Format("{0}::{1}::{2}", name, ID, String.Join(",", Material.IDs));
        }

        public static Material CreateMaterial(string wallCode, string roofCode, string greenWallCode, string greenRoofCode)
        {
            string wall = wallCode ?? Material.DEFAULT_WALL;
            string roof = roofCode ?? Material.DEFAULT_ROOF;
            string greenWall = greenWallCode ?? Material.DEFAULT_GREEN_WALL;
            string greenRoof = greenRoofCode ?? Material.DEFAULT_GREEN_ROOF;

            return new Material(new string[] { wall, roof, greenWall, greenRoof });
        }
    }
}
