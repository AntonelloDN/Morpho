using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Morpho25.Geometry
{
    public class Terrain : Entity
    {
        public FaceGroup Geometry { get; }

        public override string Name { get; }

        public Matrix2d IDmatrix { get; private set; }

        public List<string> TerrainIDrows { get; private set; }

        public List<Pixel> Pixels { get; private set; }

        public override Material Material
        {
            get => throw new NotImplementedException();
            protected set => throw new NotImplementedException();
        }

        public Terrain(Grid grid, FaceGroup geometry,
            int id, string name)
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
                var zList = Util.FilterByMinMax(grid.Xaxis, h, 0);

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
                yield return String.Format("{0},{1},{2},{3}", px.I, px.J, px.K, "1.00000");
            }
        }

        public override string ToString()
        {
            return String.Format("Terrain::{0}::{1}", Name, ID);
        }
    }
}
