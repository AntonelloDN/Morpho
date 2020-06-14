using System;

namespace MorphoGeometry
{
    public class Ray
    {
        public Vector origin;
        public Vector direction;

        public Ray(Vector origin, Vector direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public override String ToString()
        {
            return string.Format("Ray::{0}", direction);
        }

    }
}
