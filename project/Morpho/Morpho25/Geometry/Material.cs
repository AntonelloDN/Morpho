using Newtonsoft.Json;
using System;
using System.Linq;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Material class.
    /// </summary>
    public class Material : IEquatable<Material>
    {
        [JsonIgnore]
        /// <summary>
        /// Default wall material.
        /// </summary>
        public const string DEFAULT_WALL = "000000";
        [JsonIgnore]
        /// <summary>
        /// Default roof material.
        /// </summary>
        public const string DEFAULT_ROOF = "000000";
        [JsonIgnore]
        /// <summary>
        /// Default green roof material.
        /// </summary>
        public const string DEFAULT_GREEN_ROOF = " ";
        [JsonIgnore]
        /// <summary>
        /// Default green wall material.
        /// </summary>
        public const string DEFAULT_GREEN_WALL = " ";
        [JsonIgnore]
        /// <summary>
        /// Default soil material.
        /// </summary>
        public const string DEFAULT_SOIL = "000000";
        [JsonIgnore]
        /// <summary>
        /// Default plant 2D material.
        /// </summary>
        public const string DEFAULT_PLANT_2D = "0000XX";
        [JsonIgnore]
        /// <summary>
        /// Default source material.
        /// </summary>
        public const string DEFAULT_SOURCE = "0000FT";
        [JsonIgnore]
        /// <summary>
        /// Default plant 3D material.
        /// </summary>
        public const string DEFAULT_PLANT_3D = "0000C2";

        [JsonProperty("ids", Required = Required.Always)]
        /// <summary>
        /// Material array of ID.
        /// </summary>
        public string[] IDs { get; private set; }

        [JsonConstructor]
        /// <summary>
        /// Create a new material.
        /// </summary>
        /// <param name="ids">Array of material code.</param>
        public Material(string[] ids)
        {
            IDs = ids;
        }

        /// <summary>
        /// String representation of the material.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Material::{0}", String.Join(",", IDs));
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Material Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Material>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Material other)
        {
            if (other == null)
                return false;

            if (other != null
                && Enumerable.SequenceEqual(other.IDs, this.IDs))
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var materialObj = obj as Material;
            if (materialObj == null)
                return false;
            else
                return Equals(materialObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + IDs.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Material mat1, Material mat2)
        {
            if (((object)mat1) == null || ((object)mat2) == null)
                return Object.Equals(mat1, mat2);

            return mat1.Equals(mat2);
        }

        public static bool operator !=(Material mat1, Material mat2)
        {
            return !(mat1 == mat2);
        }
    }
}
