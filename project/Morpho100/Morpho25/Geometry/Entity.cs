using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Entity class.
    /// </summary>
    public abstract class Entity
    {
        protected string _name;
        protected Material _material; 
        protected int _ID;

        /// <summary>
        /// Material of the entity.
        /// </summary>
        public abstract Material Material { get; protected set; }

        /// <summary>
        /// Name of the entity.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// ID of the entity.
        /// </summary>
        public int ID {
            get { return _ID; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be positive.");

                _ID = value;
            }
        }

        /// <summary>
        /// Set 2D Matrix.
        /// </summary>
        /// <param name="intersection">Intersection points.</param>
        /// <param name="grid">Grid object.</param>
        /// <param name="matrix">2D Matrix to map.</param>
        /// <param name="text">Optional text to use.</param>
        protected void SetMatrix(IEnumerable<Vector> intersection, 
            Grid grid, Matrix2d matrix, String text = "")
        {
            foreach (Vector vec in intersection)
            {
                var pixel = vec.ToPixel(grid);

                matrix[pixel.I, pixel.J] = (text == String.Empty) 
                    ? Math.Round(vec.z, 0).ToString() 
                    : text;
            }
        }

        /// <summary>
        /// Create a new material.
        /// </summary>
        /// <param name="defaultCode">Default material ID.</param>
        /// <param name="code">Custom material ID.</param>
        /// <returns>New material object.</returns>
        protected Material CreateMaterial(string defaultCode, string code = null)
        {
            string material = code ?? defaultCode;

            return new Material(new string[] { material });
        }
    }
}
