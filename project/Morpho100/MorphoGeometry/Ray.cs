using System;

namespace MorphoGeometry
{
    /// <summary>
    /// Ray class.
    /// </summary>
    public class Ray
    {
        /// <summary>
        /// Ray origin.
        /// </summary>
        public Vector origin;

        /// <summary>
        /// Direction of the ray.
        /// </summary>
        public Vector direction;

        /// <summary>
        /// Create a new Ray.
        /// </summary>
        /// <param name="origin">Origin of the ray.</param>
        /// <param name="direction">Direction of the ray.</param>
        public Ray(Vector origin, 
            Vector direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        /// <summary>
        /// String representation of the ray.
        /// </summary>
        /// <returns>String representation.</returns>
        public override String ToString()
        {
            return string.Format("Ray::{0}", direction);
        }

    }
}
