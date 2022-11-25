using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Source class.
    /// </summary>
    public class Source : Entity
    {
        /// <summary>
        /// Geometry of the source.
        /// </summary>
        public FaceGroup Geometry { get; }
        /// <summary>
        /// Name of the source.
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Matrix of the source.
        /// </summary>
        public Matrix2d IDmatrix { get; private set; }
        /// <summary>
        /// Material of the source.
        /// </summary>
        public override Material Material
        {
            get { return _material; }
            protected set
            {
                if (value.IDs.Length != 1)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must contains 1 material code.");

                _material = value;
            }

        }
        /// <summary>
        /// Create a new source.
        /// </summary>
        /// <param name="grid">Grid object.</param>
        /// <param name="geometry">Geometry of the source.</param>
        /// <param name="id">Numerical ID.</param>
        /// <param name="code">Code of the material.</param>
        /// <param name="name">Name of the source.</param>
        public Source(Grid grid, FaceGroup geometry, 
            int id, string code = null, 
            string name = null)
        {
            ID = id;
            Geometry = geometry;
            Material = (code != null) 
                ? CreateMaterial(Material.DEFAULT_SOURCE, code) 
                : CreateMaterial(Material.DEFAULT_SOURCE);
            Name = name ?? "SourceGroup";

            SetMatrix(grid);
        }

        private void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroupBbox(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting2D(rays, Geometry, false, false);
            SetMatrix(intersection, grid, matrix, Material.IDs[0]);

            IDmatrix = matrix;
        }
        /// <summary>
        /// String representation of the source.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Source::{0}::{1}::{2}", 
                Name, ID, Material.IDs[0]);
        }
    }
}
