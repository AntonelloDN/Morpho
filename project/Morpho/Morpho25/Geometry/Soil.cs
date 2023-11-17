using Morpho25.Utility;
using MorphoGeometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Morpho25.Geometry
{
    [DisplayName("Soil")]
    /// <summary>
    /// Soil class.
    /// </summary>
    public class Soil : Entity, IEquatable<Soil>
    {
        [DisplayName("Name")]
        [Description("Name of the soil group")]
        [JsonProperty("name")]
        /// <summary>
        /// Name of the soil.
        /// </summary>
        public override string Name { get; }

        [DisplayName("Geometry")]
        [Description("Flat or solid geometry")]
        [JsonProperty("geometry", Required = Required.Always)]
        /// <summary>
        /// Geometry of the soil.
        /// </summary>
        public FaceGroup Geometry { get; set; }

        [JsonIgnore]
        /// <summary>
        /// Matrix 2D of the soil.
        /// </summary>
        public Matrix2d IDmatrix { get; private set; }
        
        [DisplayName("Material")]
        [Description("Profile of the soil")]
        [JsonProperty("material")]
        /// <summary>
        /// Material of the soil.
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

        [JsonConstructor]
        /// <summary>
        /// Create a new soil.
        /// </summary>
        /// <param name="geometry">Geometry of the soil.</param>
        /// <param name="id">Numerical ID.</param>
        /// <param name="code">Code of the material.</param>
        /// <param name="name">Name of the soil.</param>
        public Soil(FaceGroup geometry, 
            int id, string code = null, 
            string name = null)
        {
            ID = id;
            Geometry = geometry;
            Material = (code != null) 
                ? CreateMaterial(Material.DEFAULT_SOIL, code) 
                : CreateMaterial(Material.DEFAULT_SOIL);
            Name = name ?? "SoilGroup";
        }

        public void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, Material.DEFAULT_SOIL);

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroupBbox(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting2D(rays, Geometry, false, false);
            SetMatrix(intersection, grid, matrix, Material.IDs[0]);

            IDmatrix = matrix;
        }
        /// <summary>
        /// String representation of the soil.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Soil::{0}::{1}::{2}", 
                Name, ID, Material.IDs[0]);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Soil Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Soil>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Soil other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.ID == this.ID
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

            var soilObj = obj as Soil;
            if (soilObj == null)
                return false;
            else
                return Equals(soilObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ID.GetHashCode();
                hash = hash * 23 + Material.GetHashCode();
                hash = hash * 23 + Geometry.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Soil soil1, Soil soil2)
        {
            if (((object)soil1) == null || ((object)soil2) == null)
                return Object.Equals(soil1, soil2);

            return soil1.Equals(soil2);
        }

        public static bool operator !=(Soil soil1, Soil soil2)
        {
            return !(soil1 == soil2);
        }
    }
}
