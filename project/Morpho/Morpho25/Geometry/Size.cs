using System;
using MorphoGeometry;
using Newtonsoft.Json;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Grid size struct.
    /// </summary>
    public class Size : IEquatable<Size>
    {
        [JsonConstructor]
        /// <summary>
        /// Create a new size object.
        /// </summary>
        /// <param name="origin">Origin of the grid.</param>
        /// <param name="cellDimension">Cell dimension.</param>
        /// <param name="numX">Number of X cells.</param>
        /// <param name="numY">Number of Y cells.</param>
        /// <param name="numZ">Number of Z cells.</param>
        public Size(Vector origin, 
            CellDimension cellDimension, 
            int numX, int numY, 
            int numZ)
        {
            Origin = origin;
            NumX = numX;
            NumY = numY;
            NumZ = numZ;

            CellDimension = cellDimension;
            
            MinX = Origin.x;
            MinY = Origin.y;
            MaxX = Origin.x + (NumX * cellDimension.X);
            MaxY = Origin.y + (NumY * cellDimension.Y);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Size() 
        {
            Origin = new Vector(0, 0, 0);
            NumX = 50;
            NumY = 50;
            NumZ = 25;

            CellDimension = new CellDimension(3.0, 3.0, 3.0);

            MinX = Origin.x;
            MinY = Origin.y;
            MaxX = Origin.x + (NumX * CellDimension.X);
            MaxY = Origin.y + (NumY * CellDimension.Y);
        }

        [JsonProperty("numX")]
        /// <summary>
        /// Number of X cells.
        /// </summary>
        public int NumX { get; }

        [JsonProperty("cellDimension")]
        /// <summary>
        /// Number of X cells.
        /// </summary>
        public CellDimension CellDimension { get; }

        [JsonProperty("numY")]
        /// <summary>
        /// Number of Y cells.
        /// </summary>
        public int NumY { get; }

        [JsonProperty("numZ")]
        /// <summary>
        /// Number of Z cells.
        /// </summary>
        public int NumZ { get; }

        [JsonIgnore]
        /// <summary>
        /// X of the lower left corner of the grid.
        /// </summary>
        public double MinX { get; }

        [JsonIgnore]
        /// <summary>
        /// Y of the lower left corner of the grid.
        /// </summary>
        public double MinY { get; }

        [JsonIgnore]
        /// <summary>
        /// X of the upper right corner of the grid.
        /// </summary>
        public double MaxX { get; }

        [JsonIgnore]
        /// <summary>
        /// Y of the upper right corner of the grid.
        /// </summary>
        public double MaxY { get; }

        [JsonIgnore]
        /// <summary>
        /// X dimension of the cell.
        /// </summary>
        public double DimX => CellDimension.X;

        [JsonIgnore]
        /// <summary>
        /// Y dimension of the cell.
        /// </summary>
        public double DimY => CellDimension.Y;

        [JsonIgnore]
        /// <summary>
        /// Z dimension of the cell.
        /// </summary>
        public double DimZ => CellDimension.Z;

        [JsonProperty("origin")]
        /// <summary>
        /// Origin of the grid. Lower left corner.
        /// </summary>
        public Vector Origin { get; }

        /// <summary>
        /// String representation of the Grid Size.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Size::{0},{1},{2}::{3},{4},{5}", 
                NumX, NumY, NumZ, DimX, DimY, DimZ);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Size Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Size>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Size other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.Origin == this.Origin
                && other.CellDimension == this.CellDimension
                && other.NumX == this.NumX
                && other.NumY == this.NumY
                && other.NumZ == this.NumZ)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var sizeObj = obj as Size;
            if (sizeObj == null)
                return false;
            else
                return Equals(sizeObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + NumX.GetHashCode();
                hash = hash * 23 + NumY.GetHashCode();
                hash = hash * 23 + NumZ.GetHashCode();
                hash = hash * 23 + CellDimension.GetHashCode();
                hash = hash * 23 + Origin.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Size size1, Size size2)
        {
            if (((object)size1) == null || ((object)size2) == null)
                return Object.Equals(size1, size2);

            return size1.Equals(size2);
        }

        public static bool operator !=(Size size1, Size size2)
        {
            return !(size1 == size2);
        }
    }
}
