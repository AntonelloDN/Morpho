using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Building class.
    /// </summary>
    public class Building : Entity
    {
        /// <summary>
        /// Geometry of the building.
        /// </summary>
        public FaceGroup Geometry { get; }
        
        /// <summary>
        /// 2D Matrix from the top.
        /// </summary>
        public Matrix2d TopMatrix { get; private set; }

        /// <summary>
        /// 2D Matrix from the bottom.
        /// </summary>
        public Matrix2d BottomMatrix { get; private set; }

        /// <summary>
        /// 2D Matrix with building ID.
        /// </summary>
        public Matrix2d IDmatrix { get; private set; }

        /// <summary>
        /// Collection of string based on Pixel and ID.
        /// </summary>
        public List<string> BuildingIDrows { get; private set; }

        /// <summary>
        /// Collection of string based on Pixel and wall/roof materials.
        /// </summary>
        public List<string> BuildingWallRows { get; private set; }

        /// <summary>
        /// Collection of string based on Pixel and green wall/ green roof materials.
        /// </summary>
        public List<string> BuildingGreenWallRows { get; private set; }

        /// <summary>
        /// Material of the building.
        /// </summary>
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

        /// <summary>
        /// Name of the building.
        /// </summary>
        public override string Name { get; }

        /// <summary>
        /// Create a building.
        /// </summary>
        /// <param name="grid">Grid object.</param>
        /// <param name="geometry">Geometry of the building.</param>
        /// <param name="id">Numerical ID.</param>
        /// <param name="material">Material of the building.</param>
        /// <param name="name">Optional name.</param>
        public Building(Grid grid, FaceGroup geometry,
            int id, Material material = null, string name = null)
        {
            ID = id;
            Geometry = geometry;
            Material = material ?? CreateMaterial(null, null, null, null);
            Name = name ?? "Building";

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d topMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");
            Matrix2d bottomMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");
            Matrix2d idMatrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);

            IEnumerable<Vector> intersectionTop = EnvimetUtility.Raycasting2D(rays, Geometry, true);
            IEnumerable<Vector> intersectionBottom = EnvimetUtility.Raycasting2D(rays, Geometry, false);

            SetMatrix(intersectionTop, grid, topMatrix, "");
            SetMatrix(intersectionBottom, grid, bottomMatrix, "");
            SetMatrix(intersectionTop, grid, idMatrix, ID.ToString());

            TopMatrix = topMatrix;
            BottomMatrix = bottomMatrix;
            IDmatrix = idMatrix;
        }

        private IEnumerable<string> GetBuildingRows(List<Pixel> pixels)
        {
            //pixels = pixels
            //    .OrderBy(_ => _.I)
            //    .OrderBy(_ => _.J)
            //    .OrderBy(_ => _.K)
            //    .ToList();
            foreach (var px in pixels)
            {
                yield return String.Format("{0},{1},{2},{3},{4}", px.I, px.J, px.K, 1, ID);
            }
        }

        private IEnumerable<string> GetBuildingRows(List<Pixel> pixels,
            Grid grid, string wall, string roof)
        {
            var nullPx = new Pixel(0, 0, 0);
            var one = 1;

            // Matrix with default values
            var matrix = new Matrix3d<string[]>(grid.Size.NumX + one,
                grid.Size.NumY + one, grid.SequenceZ.Count() + one);

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

                // Limits
                var rlx = px.I + 1 < grid.Size.NumX;
                var rly = px.J + 1 < grid.Size.NumY;

                if (li == nullPx) matrix[px.I, px.J, px.K][0] = wallMat;
                if (lj == nullPx) matrix[px.I, px.J, px.K][1] = wallMat;
                if (bk == nullPx) matrix[px.I, px.J, px.K][2] = roofMat;
                if (rlx && ri == nullPx) matrix[px.I + 1, px.J, px.K][0] = wallMat;
                if (rly && rj == nullPx) matrix[px.I, px.J + 1, px.K][1] = wallMat;
                if (tk == nullPx) matrix[px.I, px.J, px.K + 1][2] = roofMat;
            });
            var rows = EnvimetUtility.GetStringRows(matrix);
            return rows;
        }

        private static void ShiftBuildings(Grid grid, List<Pixel> pixels,
            List<Pixel> terrainPixels)
        {
            var zLimit = grid.Size.NumZ - 1;
            // Envimet Spaces behavior
            if (terrainPixels.Any())
            {
                Parallel.For(0, pixels.Count, i =>
                {
                    var offset = 0;
                    var pixel = new Pixel(0, 0, 0);
                    var query = terrainPixels
                        .Where(_ => _.I == pixels[i].I && _.J == pixels[i].J);
                    if (query.Any()) offset = query.Select(_ => _.K).Max();
                    var x = pixels[i].I;
                    var y = pixels[i].J;
                    var z = pixels[i].K + offset;


                    pixel.I = x;
                    pixel.J = y;
                    pixel.K = (z >= zLimit) ? zLimit : z;
                    pixels[i] = pixel;
                });
            }
        }

        private static void ShiftBuildings(Grid grid, List<Pixel> pixels,
            List<Pixel> terrainPixels, int offset = 0)
        {
            var zLimit = grid.Size.NumZ - 1;
            if (terrainPixels.Any())
            {
                Parallel.For(0, pixels.Count, i =>
                {
                    var pixel = new Pixel(0, 0, 0);
                    var x = pixels[i].I;
                    var y = pixels[i].J;
                    var z = pixels[i].K + offset;

                    pixel.I = x;
                    pixel.J = y;
                    pixel.K = (z >= zLimit) ? zLimit : z;
                    pixels[i] = pixel;
                });
            };
        }

        private IEnumerable<Pixel> GetPixels(Grid grid)
        {
            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);
            var intersections = EnvimetUtility.Raycasting3D(rays, Geometry, false, false);
            var centroids = EnvimetUtility.GetCentroids(grid, intersections);
            var pixels = centroids.Select(_ => _.ToPixel(grid)).ToList();

            return pixels;
        }

        /// <summary>
        /// Generate model 3D. It should run after generating buildings and terrains.
        /// </summary>
        /// <param name="grid">Morpho Grid.</param>
        /// <param name="terrainPixels">Pixels of the terrain.</param>
        public void SetMatrix3d(Grid grid,
            List<Pixel> terrainPixels = null,
            bool shiftEachVoxel = false)
        {
            Reset3Dmatrix();

            var pixels = GetPixels(grid).ToList();
            if (!pixels.Any()) return;

            var tPixels = FilterPixels(terrainPixels, pixels);
            if (shiftEachVoxel)
            {
                ShiftBuildings(grid, pixels, tPixels);
            }
            else
            {
                var offset = 0;
                if (tPixels.Any())
                {
                    var groups = tPixels.GroupBy(_ => new { I = _.I, J = _.J });
                    offset = groups.Select(_ => _.Select(i => i.K).Max()).Min();
                }
                ShiftBuildings(grid, pixels, tPixels, offset);
            }

            BuildingIDrows.AddRange(GetBuildingRows(pixels));
            var wallDB = GetBuildingRows(pixels, grid,
                Material.IDs[0], Material.IDs[1]);
            BuildingWallRows.AddRange(wallDB);

            if (Material.IDs[2] != Material.DEFAULT_GREEN_WALL ||
                Material.IDs[3] != Material.DEFAULT_GREEN_WALL)
            {
                var greeningsDB = GetBuildingRows(pixels, grid,
                    Material.IDs[0], Material.IDs[1]);
                BuildingGreenWallRows.AddRange(greeningsDB);
            }
        }

        private List<Pixel> FilterPixels(List<Pixel> terrainPixels, 
            List<Pixel> pixels)
        {
            var allI = pixels.Select(_ => _.I).Distinct().ToList();
            var allj = pixels.Select(_ => _.J).Distinct().ToList();

            var tPixels = new List<Pixel>();
            foreach (var pixel in terrainPixels)
            {
                if (!allI.Contains(pixel.I)) continue;
                if (!allj.Contains(pixel.J)) continue;
                tPixels.Add(pixel);
            }

            return tPixels;
        }

        private void Reset3Dmatrix()
        {
            BuildingWallRows = new List<string>();
            BuildingGreenWallRows = new List<string>();
            BuildingIDrows = new List<string>();
        }

        /// <summary>
        /// String representation of a building.
        /// </summary>
        /// <returns>String representation.</returns>
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
