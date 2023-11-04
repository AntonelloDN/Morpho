using Newtonsoft.Json;
using System;

namespace MorphoGeometry
{
    /// <summary>
    /// Ray class.
    /// </summary>
    public class Ray : IEquatable<Ray>
    {
        [JsonProperty(Required = Required.Always)]
        /// <summary>
        /// Ray origin.
        /// </summary>
        public Vector origin;

        [JsonProperty(Required = Required.Always)]
        /// <summary>
        /// Direction of the ray.
        /// </summary>
        public Vector direction;

        [JsonConstructor]
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

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Ray Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Ray>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Ray other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.origin == this.origin
                && other.direction == this.direction)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var vecObj = obj as Ray;
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
                hash = hash * 23 + origin.GetHashCode();
                hash = hash * 23 + direction.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Ray ray1, Ray ray2)
        {
            if (((object)ray1) == null || ((object)ray2) == null)
                return Object.Equals(ray1, ray2);

            return ray1.Equals(ray2);
        }

        public static bool operator !=(Ray ray1, Ray ray2)
        {
            return !(ray1 == ray2);
        }
    }
}
