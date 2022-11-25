using Morpho25.Utility;
using MorphoGeometry;
using System;


namespace Morpho25.Geometry
{
    /// <summary>
    /// Receptor class.
    /// </summary>
    public class Receptor : Entity
    {
        /// <summary>
        /// Geometry of the receptor.
        /// </summary>
        public Vector Geometry { get; }
        /// <summary>
        /// Name of the receptor.
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Location of the receptor in the grid.
        /// </summary>
        public Pixel Pixel { get; private set; }
        /// <summary>
        /// Material of the receptor.
        /// </summary>
        public override Material Material
        {
            get => throw new NotImplementedException();
            protected set => throw new NotImplementedException();
        }
        /// <summary>
        /// Create a new receptor.
        /// </summary>
        /// <param name="grid">Grid object.</param>
        /// <param name="geometry">Geometry of the receptor.</param>
        /// <param name="name">Name of the receptor.</param>
        public Receptor(Grid grid, 
            Vector geometry, string name = null)
        {
            Geometry = geometry;
            SetPixel(grid);
            string text = name ?? "R_";

            Name = text + String.Join("_", Pixel.I, 
                Pixel.J, Pixel.K);
        }

        private void SetPixel(Grid grid)
        {
            Pixel = new Pixel
            {
                I = Util.ClosestValue(grid.Xaxis, Geometry.x),
                J = Util.ClosestValue(grid.Yaxis, Geometry.y),
                K = 0
            };
        }
        /// <summary>
        /// String representation of the receptor.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Receptor::{0}", Name);
        }
    }
}
