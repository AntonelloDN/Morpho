using Newtonsoft.Json;
using System;

namespace MorphoGeometry
{
    /// <summary>
    /// Vector class.
    /// </summary>
    public class Vector: IEquatable<Vector>
    {
        [JsonProperty(Required = Required.Always)]
        /// <summary>
        /// X component.
        /// </summary>
        public float x;

        [JsonProperty(Required = Required.Always)]
        /// <summary>
        /// Y component.
        /// </summary>
        public float y;

        [JsonProperty(Required = Required.Always)]
        /// <summary>
        /// Z component.
        /// </summary>
        public float z;

        [JsonConstructor]
        /// <summary>
        /// Create a new vector.
        /// </summary>
        /// <param name="x">X component.</param>
        /// <param name="y">Y component.</param>
        /// <param name="z">Z component.</param>
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Det 3x3.
        /// </summary>
        /// <param name="matr">Matrix to solve.</param>
        /// <returns>Det.</returns>
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

        /// <summary>
        /// Create a vector from array.
        /// </summary>
        /// <param name="arr">Array with 3 items.</param>
        /// <returns>New vector.</returns>
        public static Vector FromArray(float[] arr)
        {
            return new Vector(arr[0], arr[1], arr[2]);
        }

        /// <summary>
        /// Convert Vector to array.
        /// </summary>
        /// <returns>Array.</returns>
        public float[] ToArray()
        {
            return new[] { x, y, z };
        }

        /// <summary>
        /// Subtract vector.
        /// </summary>
        /// <param name="v">Vector to subtract.</param>
        /// <returns>New vector.</returns>
        public Vector Sub(Vector v)
        {
            return new Vector(this.x - v.x,
                            this.y - v.y,
                            this.z - v.z);
        }

        /// <summary>
        /// Dot product.
        /// </summary>
        /// <param name="v">Vector to multiply.</param>
        /// <returns>Value.</returns>
        public float Dot(Vector v)
        {
            return this.x * v.x +
                   this.y * v.y +
                   this.z * v.z;
        }

        /// <summary>
        /// Cross product.
        /// </summary>
        /// <param name="v">Vector to multiply.</param>
        /// <returns>New vector.</returns>
        public Vector Cross(Vector v)
        {
            return new Vector(
                this.y * v.z - this.z * v.y,
                this.z * v.x - this.x * v.z,
                this.x * v.y - this.y * v.x
            );
        }

        /// <summary>
        /// Vector from 2 points.
        /// </summary>
        /// <param name="vec1">First vector.</param>
        /// <param name="vec2">Second vector.</param>
        /// <returns>New Vector.</returns>
        public static Vector VectorFrom2Points(Vector vec1, 
            Vector vec2)
        {
            return new Vector(vec2.x - vec1.x, 
                vec2.y - vec1.y, vec2.z - vec1.z);
        }

        /// <summary>
        /// Magnitude of the vector.
        /// </summary>
        /// <returns>Value.</returns>
        public float Length()
        {
            return (float) Math.Sqrt(x * x + 
                y * y + z * z);
        }

        /// <summary>
        /// Normalize the vector.
        /// </summary>
        /// <returns>New vector.</returns>
        public Vector Normalize()
        {
            float len = Length();
            return new Vector(x / len, 
                y / len, z / len);
        }

        /// <summary>
        /// String representation of the vector.
        /// </summary>
        /// <returns>String representation.</returns>
        public override String ToString()
        {
            return string.Format("{0}, {1}, {2}", x, y, z);
        }

        public string Serialize() 
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Vector Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Vector>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Vector other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.x == this.x
                && other.y == this.y
                && other.z == this.z)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var vecObj = obj as Vector;
            if (vecObj == null)
                return false;
            else
                return Equals(vecObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                hash = hash * 23 + z.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Vector vec1, Vector vec2)
        {
            if (((object)vec1) == null || ((object)vec2) == null)
                return Object.Equals(vec1, vec2);

            return vec1.Equals(vec2);
        }

        public static bool operator !=(Vector vec1, Vector vec2)
        {
            return !(vec1 == vec2);
        }
    }
}
