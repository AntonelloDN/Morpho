using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Plant 2D class.
    /// </summary>
    public class Plant2d : Entity
    {
        /// <summary>
        /// Geometry of the plant 2D.
        /// </summary>
        public FaceGroup Geometry { get; }
        /// <summary>
        /// Name of the plant 2D.
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Matrix 2D of the plant 2D.
        /// </summary>
        public Matrix2d IDmatrix { get; private set; }
        /// <summary>
        /// Material of the plant 2D.
        /// </summary>
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
        /// <summary>
        /// Create a new plant 2D.
        /// </summary>
        /// <param name="grid">Grid object.</param>
        /// <param name="geometry">Geometry of the plant 2D.</param>
        /// <param name="id">Numerical ID.</param>
        /// <param name="code">Code of the material.</param>
        /// <param name="name">Name of the plant 2D.</param>
        public Plant2d(Grid grid, FaceGroup geometry, 
            int id, string code = null, string name = null)
        {
            ID = id;
            Geometry = geometry;
            Material = (code != null) 
                ? CreateMaterial(Material.DEFAULT_PLANT_2D, code) 
                : CreateMaterial(Material.DEFAULT_PLANT_2D);
            Name = name ?? "PlantGroup";

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroup(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting2D(rays, Geometry, false, false);
            SetMatrix(intersection, grid, matrix, Material.IDs[0]);

            IDmatrix = matrix;
        }

        /// <summary>
        /// String representation of the plant 2D.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Plant2D::{0}::{1}::{2}", 
                Name, ID, Material.IDs[0]);
        }

    }
}
