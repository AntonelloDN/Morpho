using Newtonsoft.Json;
using System;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Cell dimension struct.
    /// </summary>
    public class CellDimension : IEquatable<CellDimension>
    {
        [JsonConstructor]
        /// <summary>
        /// Create a new cell.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public CellDimension(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [JsonProperty("x")]
        /// <summary>
        /// X coordinate.
        /// </summary>
        public double X { get; }

        [JsonProperty("y")]
        /// <summary>
        /// Y coordinate.
        /// </summary>
        public double Y { get; }
        
        [JsonProperty("z")]
        /// <summary>
        /// Z coordinate.
        /// </summary>
        public double Z { get; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static CellDimension Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<CellDimension>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Equals(CellDimension other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.X == this.X
                && other.Y == this.Y
                && other.Z == this.Z)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var dimObj = obj as CellDimension;
            if (dimObj == null)
                return false;
            else
                return Equals(dimObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(CellDimension dim1, CellDimension dim2)
        {
            if (((object)dim1) == null || ((object)dim2) == null)
                return Object.Equals(dim1, dim2);

            return dim1.Equals(dim2);
        }

        public static bool operator !=(CellDimension dim1, CellDimension dim2)
        {
            return !(dim1 == dim2);
        }
    }
}
