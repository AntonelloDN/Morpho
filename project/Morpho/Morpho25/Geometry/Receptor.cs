using Morpho25.Utility;
using MorphoGeometry;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Morpho25.Geometry
{
    [DisplayName("Receptor")]
    /// <summary>
    /// Receptor class.
    /// </summary>
    public class Receptor : Entity, IEquatable<Receptor>
    {
        [DisplayName("Name")]
        [Description("Name of the receptor")]
        [JsonProperty("name", Required = Required.Always)]
        /// <summary>
        /// Name of the receptor.
        /// </summary>
        public override string Name { get; }

        [DisplayName("Geometry")]
        [Description("Point geometry")]
        [JsonProperty("geometry", Required = Required.Always)]
        /// <summary>
        /// Geometry of the receptor.
        /// </summary>
        public Vector Geometry { get; set; }

        [JsonIgnore]
        /// <summary>
        /// Location of the receptor in the grid.
        /// </summary>
        public Pixel Pixel { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Material of the receptor.
        /// </summary>
        public override Material Material
        {
            get => throw new NotImplementedException();
            protected set => throw new NotImplementedException();
        }
        /// <summary>
        /// Create a new receptor.
        /// </summary>
        /// <param name="geometry">Geometry of the receptor.</param>
        /// <param name="name">Name of the receptor.</param>
        public Receptor(Vector geometry, string name)
        {
            Geometry = geometry;
            Name = name;
        }

        public void SetPixel(Grid grid)
        {
            Pixel = new Pixel
            {
                I = Util.ClosestValue(grid.Xaxis, Geometry.x),
                J = Util.ClosestValue(grid.Yaxis, Geometry.y),
                K = 0
            };
        }
        /// <summary>
        /// String representation of the receptor.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Receptor::{0}", Name);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Receptor Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Receptor>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Receptor other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.Name == this.Name
                && other.Geometry == this.Geometry)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var receptorObj = obj as Receptor;
            if (receptorObj == null)
                return false;
            else
                return Equals(receptorObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Geometry.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Receptor receptor1, Receptor receptor2)
        {
            if (((object)receptor1) == null || ((object)receptor2) == null)
                return Object.Equals(receptor1, receptor2);

            return receptor1.Equals(receptor2);
        }

        public static bool operator !=(Receptor receptor1, Receptor receptor2)
        {
            return !(receptor1 == receptor2);
        }
    }
}
