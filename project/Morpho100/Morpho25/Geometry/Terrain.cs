using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Terrain class.
    /// </summary>
    public class Terrain : Entity
    {
        /// <summary>
        /// Geometry of the terrain.
        /// </summary>
        public FaceGroup Geometry { get; }
        /// <summary>
        /// Name of the terrain.
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Matrix 2D of the terrain.
        /// </summary>
        public Matrix2d IDmatrix { get; private set; }
        /// <summary>
        /// Collection of string based on Pixel and ID.
        /// </summary>
        public List<string> TerrainIDrows { get; private set; }
        /// <summary>
        /// Location of the terrain in the grid.
        /// </summary>
        public List<Pixel> Pixels { get; private set; }
        /// <summary>
        /// Material of the terrain.
        /// </summary>
        public override Material Material
        {
            get => throw new NotImplementedException();
            protected set => throw new NotImplementedException();
        }
        /// <summary>
        /// Create a new terrain.
        /// </summary>
        /// <param name="grid">Grid object.</param>
        /// <param name="geometry">Geometry of the terrain.</param>
        /// <param name="id">Numerical ID.</param>
        /// <param name="name">Name of the terrain.</param>
        public Terrain(Grid grid, FaceGroup geometry,
            int id, string name = null)
        {
            ID = id;
            Geometry = geometry;
            Name = name ?? "TerrainGroup";
            TerrainIDrows = new List<string>();
            Pixels = new List<Pixel>();

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting2D(rays, Geometry, true, false);
            // 2D Part
            SetMatrix(intersection, grid, matrix, "");
            // 3D Part
            CalculatePixels(grid, intersection);

            IDmatrix = matrix;
        }

        private void CalculatePixels(Grid grid, IEnumerable<Vector> intersection)
        {
            var voxels = new List<Vector>();
            foreach (var pt in intersection)
            {
                var h = (pt.z < 0) ? 0 : pt.z;
                var zList = Util.FilterByMinMax(grid.Zaxis, h, 0);

                foreach(var v in zList)
                {
                    voxels.Add(new Vector(pt.x, pt.y, Convert.ToSingle(v)));
                }
            }
            Pixels = voxels
                .Select(_ => _.ToPixel(grid))
                .ToList();

            TerrainIDrows = GetTerrainRows()
                .ToList();
        }

        private IEnumerable<string> GetTerrainRows()
        {
            foreach (var px in Pixels)
            {
                yield return String.Format("{0},{1},{2},{3}", 
                    px.I, px.J, px.K, "1.00000");
            }
        }
        /// <summary>
        /// String representation of the terrain.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Terrain::{0}::{1}", Name, ID);
        }
    }
}
