using Morpho25.Utility;
using MorphoGeometry;
using System;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Plant 3D class.
    /// </summary>
    public class Plant3d : Entity
    {
        private const int SHIFT = 1;

        /// <summary>
        /// Geometry of the plant 3D.
        /// </summary>
        public Vector Geometry { get; }

        /// <summary>
        /// Material of the plant 3D.
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
        /// Name of the plant 3D.
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Location of the plant 3D in the grid.
        /// </summary>
        public Pixel Pixel { get; private set; }
        /// <summary>
        /// Create a new plant 3D.
        /// </summary>
        /// <param name="grid">Grid object.</param>
        /// <param name="geometry">Geometry of the plant 3D.</param>
        /// <param name="code">Code of the material.</param>
        /// <param name="name">Name of the plant 3D.</param>
        public Plant3d(Grid grid, Vector geometry, 
            string code = null, string name = null)
        {
            Geometry = geometry;
            Material = (code != null) 
                ? CreateMaterial(Material.DEFAULT_PLANT_3D, code) 
                : CreateMaterial(Material.DEFAULT_PLANT_3D);
            Name = name ?? "PlantGroup";

            SetPixel(grid);
        }

        private void SetPixel(Grid grid)
        {
            Pixel = new Pixel
            {
                I = Util.ClosestValue(grid.Xaxis, Geometry.x) + SHIFT,
                J = Util.ClosestValue(grid.Yaxis, Geometry.y) + SHIFT,
                K = 0
            };
        }
        /// <summary>
        /// String representation of the plant 3D.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Plant3D::{0}::{1}::{2}", 
                Name, Material.IDs[0], String.Join(",", 
                Pixel.I, Pixel.J, Pixel.K));
        }

    }
}
