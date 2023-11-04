using Morpho25.Utility;
using MorphoGeometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Morpho25.Geometry
{
    [DisplayName("Source")]
    /// <summary>
    /// Source class.
    /// </summary>
    public class Source : Entity, IEquatable<Source>
    {
        [DisplayName("Name")]
        [Description("Name of the source group")]
        [JsonProperty("name")]
        /// <summary>
        /// Name of the source.
        /// </summary>
        public override string Name { get; }

        [DisplayName("Geometry")]
        [Description("Flat or solid geometry")]
        [JsonProperty("geometry", Required = Required.Always)]
        /// <summary>
        /// Geometry of the source.
        /// </summary>
        public FaceGroup Geometry { get; }

        [JsonIgnore]
        /// <summary>
        /// Matrix of the source.
        /// </summary>
        public Matrix2d IDmatrix { get; private set; }

        [DisplayName("Material")]
        [Description("Source type")]
        [JsonProperty("material")]
        /// <summary>
        /// Material of the source.
        /// </summary>
        public override Material Material
        {
            get { return _material; }
            protected set
            {
                if (value.IDs.Length != 1)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must contains 1 material code.");

                _material = value;
            }

        }

        [JsonConstructor]
        /// <summary>
        /// Create a new source.
        /// </summary>
        /// <param name="geometry">Geometry of the source.</param>
        /// <param name="id">Numerical ID.</param>
        /// <param name="code">Code of the material.</param>
        /// <param name="name">Name of the source.</param>
        public Source(FaceGroup geometry, 
            int id, string code = null, 
            string name = null)
        {
            ID = id;
            Geometry = geometry;
            Material = (code != null) 
                ? CreateMaterial(Material.DEFAULT_SOURCE, code) 
                : CreateMaterial(Material.DEFAULT_SOURCE);
            Name = name ?? "SourceGroup";
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
        /// String representation of the source.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Source::{0}::{1}::{2}", 
                Name, ID, Material.IDs[0]);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Source Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Source>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Source other)
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

            var sourceObj = obj as Source;
            if (sourceObj == null)
                return false;
            else
                return Equals(sourceObj);
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

        public static bool operator ==(Source source1, Source source2)
        {
            if (((object)source1) == null || ((object)source2) == null)
                return Object.Equals(source1, source2);

            return source1.Equals(source2);
        }

        public static bool operator !=(Source source1, Source source2)
        {
            return !(source1 == source2);
        }

    }
}
