using System;
using System.Collections.Generic;

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

        public static float Det3x3(float[][] matr)
        {
            return matr[0][0] * matr[1][1] * matr[2][2] +
                matr[0][1] * matr[1][2] * matr[2][0] +
                matr[0][2] * matr[1][0] * matr[2][1] -
                (
                    matr[0][2] * matr[1][1] * matr[2][0] +
                    matr[0][1] * matr[1][0] * matr[2][2] +
                    matr[0][0] * matr[1][2] * matr[2][1]
                );
        }

        public static Vector FromArray(float[] arr)
        {
            return new Vector(arr[0], arr[1], arr[2]);
        }

        public float[] ToArray()
        { 
            return new[] { x, y, z };
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
