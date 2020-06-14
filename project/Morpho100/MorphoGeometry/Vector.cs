using System;

namespace MorphoGeometry
{
    public class Vector
    {

        public float x;
        public float y;
        public float z;

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector Sub(Vector v)
        {
            return new Vector(this.x - v.x,
                            this.y - v.y,
                            this.z - v.z);
        }

        public float Dot(Vector v)
        {
            return this.x * v.x +
                   this.y * v.y +
                   this.z * v.z;
        }

        public Vector Cross(Vector v)
        {
            return new Vector(
                this.y * v.z - this.z * v.y,
                this.z * v.x - this.x * v.z,
                this.x * v.y - this.y * v.x
            );
        }

        public float Length()
        {
            return (float) Math.Sqrt(x * x + y * y + z * z);
        }

        public Vector Normalize()
        {
            float len = Length();
            return new Vector(x / len, y / len, z / len);
        }

        public override String ToString()
        {
            return string.Format("{0}, {1}, {2}", x, y, z);
        }

    };
}
