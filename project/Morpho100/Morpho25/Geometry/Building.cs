using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;

namespace Morpho25.Geometry
{
    public class Building : Entity
    {
        public FaceGroup Geometry { get; }
        public Matrix2d TopMatrix { get; private set; }
        public Matrix2d BottomMatrix { get; private set; }
        public Matrix2d IDmatrix { get; private set; }
        public Matrix3d VoxelMatrix { get; private set; }

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

        public Building(Grid grid, FaceGroup geometry, int id, Material material, string name)
        {
            ID = id;
            Geometry = geometry;
            Material = material ?? CreateMaterial(null, null, null, null);
            Name = name ?? "Building";

            SetMatrix(grid);
            //SetMatrix3d(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d topMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");
            Matrix2d bottomMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");
            Matrix2d idMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);

            IEnumerable<Vector> intersectionTop = EnvimetUtility.Raycasting2D(rays, Geometry, true, false);
            IEnumerable<Vector> intersectionBottom = EnvimetUtility.Raycasting2D(rays, Geometry, false, false);

            SetMatrix(intersectionTop, grid, topMatrix, "");
            SetMatrix(intersectionBottom, grid, bottomMatrix, "");
            SetMatrix(intersectionTop, grid, idMatrix, ID.ToString());

            TopMatrix = topMatrix;
            BottomMatrix = bottomMatrix;
            IDmatrix = idMatrix;
        }

        public IEnumerable<Vector> SetMatrix3d(Grid grid)
        {
            Matrix3d matrix = new Matrix3d(grid.Size.NumX, grid.Size.NumY, grid.Size.NumZ, "0");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);

            var intersections = EnvimetUtility.Raycasting3D(rays, Geometry, false, false);

            var centroids = EnvimetUtility.GetCentroids(grid, intersections);
            SetMatrix(centroids, grid, matrix, ID.ToString());

            VoxelMatrix = matrix;
            return centroids;
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
