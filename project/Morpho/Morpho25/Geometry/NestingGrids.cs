using Newtonsoft.Json;
using System;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Nesting grids class.
    /// </summary>
    public class NestingGrids : IEquatable<NestingGrids>
    {
        [JsonProperty("firstMaterial")]
        /// <summary>
        /// First material.
        /// </summary>
        public string FirstMaterial { get; private set; }

        [JsonProperty("secondMaterial")]
        /// <summary>
        /// Second material.
        /// </summary>
        public string SecondMaterial { get; private set; }

        [JsonProperty("numberOfCells")]
        /// <summary>
        /// Number of cells.
        /// </summary>
        public uint NumberOfCells { get; private set; }

        /// <summary>
        /// Create a new nesting grids object.
        /// </summary>
        public NestingGrids()
        {
            NumberOfCells = 0;
            FirstMaterial = Material.DEFAULT_SOIL;
            SecondMaterial = Material.DEFAULT_SOIL;
        }

        [JsonConstructor]
        /// <summary>
        /// Create a new nesting grids object.
        /// </summary>
        /// <param name="numberOfCells">Number of cells.</param>
        /// <param name="firstMaterial">First material.</param>
        /// <param name="secondMaterial">Second material.</param>
        public NestingGrids(uint numberOfCells,
            string firstMaterial,
            string secondMaterial)
        {
            NumberOfCells = numberOfCells;
            FirstMaterial = firstMaterial;
            SecondMaterial = secondMaterial;
        }
        public bool Equals(NestingGrids other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.NumberOfCells == this.NumberOfCells
                && other.FirstMaterial == this.FirstMaterial
                && other.SecondMaterial == this.SecondMaterial)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var faceObj = obj as NestingGrids;
            if (faceObj == null)
                return false;
            else
                return Equals(faceObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + FirstMaterial.GetHashCode();
                hash = hash * 23 + SecondMaterial.GetHashCode();
                hash = hash * 23 + NumberOfCells.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(NestingGrids nesting1, NestingGrids nesting2)
        {
            if (((object)nesting1) == null || ((object)nesting2) == null)
                return Object.Equals(nesting1, nesting2);

            return nesting1.Equals(nesting2);
        }

        public static bool operator !=(NestingGrids nesting1, NestingGrids nesting2)
        {
            return !(nesting1 == nesting2);
        }
    }
}
