using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morpho25.Geometry
{
    public class Building : Entity
    {
        public FaceGroup Geometry { get; }
        public Matrix2d TopMatrix { get; private set; }
        public Matrix2d BottomMatrix { get; private set; }
        public Matrix2d IDmatrix { get; private set; }

        public bool IsDetailed { get; private set; }
        public List<string> BuildingIDrows { get; private set; }
        public List<string> BuildingWallRows { get; private set; }
        public List<string> BuildingGreenWallRows { get; private set; }

        public override Material Material
        {
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

        public Building(Grid grid, FaceGroup geometry,
            int id, Material material, string name, bool isDetailed = false)
        {
            ID = id;
            Geometry = geometry;
            Material = material ?? CreateMaterial(null, null, null, null);
            Name = name ?? "Building";
            IsDetailed = isDetailed;
            BuildingWallRows = new List<string>();
            BuildingGreenWallRows = new List<string>();
            BuildingIDrows = new List<string>();

            SetMatrix(grid);
            if (IsDetailed) SetMatrix3d(grid);
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

        private IEnumerable<string> GetBuildingRows(List<Pixel> pixels)
        {
            foreach (var px in pixels)
            {
                yield return String.Format("{0},{1},{2},{3},{4}", px.I, px.J, px.K, 1, ID);
            }
        }

        private IEnumerable<string> GetBuildingRows(List<Pixel> pixels,
            Grid grid, string wall, string roof)
        {
            var nullPx = new Pixel(0, 0, 0);
            var temp = new string[pixels.Count];

            // Matrix with default values
            var matrix = new Matrix3d<string[]>(grid.Size.NumX,
                grid.Size.NumY, grid.SequenceZ.Count());
            for (int i = 0; i < matrix.GetLengthX(); i++)
                for (int j = 0; j < matrix.GetLengthY(); j++)
                    for (int k = 0; k < matrix.GetLengthZ(); k++)
                        matrix[i, j, k] = new string[] { null, null, null };


            Parallel.For(0, pixels.Count, i =>
            {
                var line = string.Empty;
                var px = pixels[i];

                var li = pixels.FirstOrDefault(_ => _.I == px.I - 1 && _.J == px.J && _.K == px.K);
                var lj = pixels.FirstOrDefault(_ => _.I == px.I && _.J == px.J - 1 && _.K == px.K);
                var bk = pixels.FirstOrDefault(_ => _.I == px.I && _.J == px.J && _.K == px.K - 1);
                var ri = pixels.FirstOrDefault(_ => _.I == px.I + 1 && _.J == px.J && _.K == px.K);
                var rj = pixels.FirstOrDefault(_ => _.I == px.I && _.J == px.J + 1 && _.K == px.K);
                var tk = pixels.FirstOrDefault(_ => _.I == px.I && _.J == px.J && _.K == px.K + 1);

                var wallMat = (wall != Material.DEFAULT_GREEN_WALL) ? wall : null;
                var roofMat = (roof != Material.DEFAULT_GREEN_ROOF) ? roof : null;

                if (li == nullPx) matrix[px.I, px.J, px.K][0] = wallMat;
                if (lj == nullPx) matrix[px.I, px.J, px.K][1] = wallMat;
                if (bk == nullPx) matrix[px.I, px.J, px.K][2] = roofMat;
                if (ri == nullPx) matrix[px.I, px.J, px.K][0] = wallMat;
                if (rj == nullPx) matrix[px.I, px.J, px.K][1] = wallMat;
                if (tk == nullPx) matrix[px.I, px.J, px.K][2] = roofMat;
            });
            var rows = EnvimetUtility.GetStringRows(matrix);
            return rows;
        }

        public IEnumerable<Vector> SetMatrix3d(Grid grid)
        {
            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);
            var intersections = EnvimetUtility.Raycasting3D(rays, Geometry, false, false);
            var centroids = EnvimetUtility.GetCentroids(grid, intersections);
            var pixels = centroids.Select(_ => _.ToPixel(grid)).ToList();

            // ID
            BuildingIDrows.AddRange(GetBuildingRows(pixels));

            // WallDB
            var wallDB = GetBuildingRows(pixels, grid, Material.IDs[0], Material.IDs[1]);
            BuildingWallRows.AddRange(wallDB);

            if (Material.IDs[2] != Material.DEFAULT_GREEN_WALL ||
                Material.IDs[3] != Material.DEFAULT_GREEN_WALL)
            {
                var greeningsDB = GetBuildingRows(pixels, grid, Material.IDs[0], Material.IDs[1]);
                BuildingGreenWallRows.AddRange(greeningsDB);
            }

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
