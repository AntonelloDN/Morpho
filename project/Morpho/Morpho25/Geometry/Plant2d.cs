using Morpho25.Utility;
using MorphoGeometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Morpho25.Geometry
{
    [DisplayName("Simple Plant")]
    /// <summary>
    /// Plant 2D class.
    /// </summary>
    public class Plant2d : Entity, IEquatable<Plant2d>
    {
        [DisplayName("Name")]
        [Description("Name of the simple plant group")]
        [JsonProperty("name")]
        /// <summary>
        /// Name of the plant 2D.
        /// </summary>
        public override string Name { get; }

        [DisplayName("Geometry")]
        [Description("Flat or solid geometry")]
        [JsonProperty("geometry", Required = Required.Always)]
        /// <summary>
        /// Geometry of the plant 2D.
        /// </summary>
        public FaceGroup Geometry { get; set; }

        [JsonIgnore]
        /// <summary>
        /// Matrix 2D of the plant 2D.
        /// </summary>
        public Matrix2d IDmatrix { get; private set; }

        [DisplayName("Material")]
        [Description("Simple plant type")]
        [JsonProperty("material")]
        /// <summary>
        /// Material of the plant 2D.
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
        /// Create a new plant 2D.
        /// </summary>
        /// <param name="geometry">Geometry of the plant 2D.</param>
        /// <param name="id">Numerical ID.</param>
        /// <param name="code">Code of the material.</param>
        /// <param name="name">Name of the plant 2D.</param>
        public Plant2d(FaceGroup geometry, 
            int id, string code = null, string name = null)
        {
            ID = id;
            Geometry = geometry;
            Material = (code != null) 
                ? CreateMaterial(Material.DEFAULT_PLANT_2D, code) 
                : CreateMaterial(Material.DEFAULT_PLANT_2D);
            Name = name ?? "PlantGroup";
        }

        public void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroupBbox(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting2D(rays, Geometry, false, false);
            SetMatrix(intersection, grid, matrix, Material.IDs[0]);

            IDmatrix = matrix;
        }

        /// <summary>
        /// String representation of the plant 2D.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Plant2D::{0}::{1}::{2}", 
                Name, ID, Material.IDs[0]);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Plant2d Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Plant2d>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Plant2d other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.ID == this.ID
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

            var plantObj = obj as Plant2d;
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
                hash = hash * 23 + ID.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Material.GetHashCode();
                hash = hash * 23 + Geometry.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Plant2d plant1, Plant2d plant2)
        {
            if (((object)plant1) == null || ((object)plant2) == null)
                return Object.Equals(plant1, plant2);

            return plant1.Equals(plant2);
        }

        public static bool operator !=(Plant2d plant1, Plant2d plant2)
        {
            return !(plant1 == plant2);
        }
    }
}
