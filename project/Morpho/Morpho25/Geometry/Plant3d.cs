using Morpho25.Utility;
using MorphoGeometry;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Morpho25.Geometry
{
    [DisplayName("Plant3D")]
    /// <summary>
    /// Plant 3D class.
    /// </summary>
    public class Plant3d : Entity, IEquatable<Plant3d>
    {
        private const int SHIFT = 1;

        [DisplayName("Name")]
        [Description("Name of the plant3D group")]
        [JsonProperty("name")]
        /// <summary>
        /// Name of the plant 3D.
        /// </summary>
        public override string Name { get; }

        [DisplayName("Geometry")]
        [Description("Point geometry")]
        [JsonProperty("geometry", Required = Required.Always)]
        /// <summary>
        /// Geometry of the plant 3D.
        /// </summary>
        public Vector Geometry { get; set; }

        [DisplayName("Material")]
        [Description("Plant3D type")]
        [JsonProperty("material")]
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
        
        [JsonIgnore]
        /// <summary>
        /// Location of the plant 3D in the grid.
        /// </summary>
        public Pixel Pixel { get; private set; }
        /// <summary>
        /// Create a new plant 3D.
        /// </summary>
        /// <param name="geometry">Geometry of the plant 3D.</param>
        /// <param name="code">Code of the material.</param>
        /// <param name="name">Name of the plant 3D.</param>
        public Plant3d(Vector geometry, 
            string code = null, string name = null)
        {
            Geometry = geometry;
            Material = (code != null) 
                ? CreateMaterial(Material.DEFAULT_PLANT_3D, code) 
                : CreateMaterial(Material.DEFAULT_PLANT_3D);
            Name = name ?? "PlantGroup";
        }

        public void SetPixel(Grid grid)
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

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Plant3d Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Plant3d>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Plant3d other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.Name == this.Name
                && other.Material == this.Material
                && other.Geometry == this.Geometry)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var plantObj = obj as Plant3d;
            if (plantObj == null)
                return false;
            else
                return Equals(plantObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Material.GetHashCode();
                hash = hash * 23 + Geometry.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Plant3d plant1, Plant3d plant2)
        {
            if (((object)plant1) == null || ((object)plant2) == null)
                return Object.Equals(plant1, plant2);

            return plant1.Equals(plant2);
        }

        public static bool operator !=(Plant3d plant1, Plant3d plant2)
        {
            return !(plant1 == plant2);
        }
    }
}
