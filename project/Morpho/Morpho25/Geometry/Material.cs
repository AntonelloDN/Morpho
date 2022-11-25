using System;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Material class.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Default wall material.
        /// </summary>
        public const string DEFAULT_WALL = "000000";
        /// <summary>
        /// Default roof material.
        /// </summary>
        public const string DEFAULT_ROOF = "000000";
        /// <summary>
        /// Default green roof material.
        /// </summary>
        public const string DEFAULT_GREEN_ROOF = " ";
        /// <summary>
        /// Default green wall material.
        /// </summary>
        public const string DEFAULT_GREEN_WALL = " ";
        /// <summary>
        /// Default soil material.
        /// </summary>
        public const string DEFAULT_SOIL = "000000";
        /// <summary>
        /// Default plant 2D material.
        /// </summary>
        public const string DEFAULT_PLANT_2D = "0000XX";
        /// <summary>
        /// Default source material.
        /// </summary>
        public const string DEFAULT_SOURCE = "0000FT";
        /// <summary>
        /// Default plant 3D material.
        /// </summary>
        public const string DEFAULT_PLANT_3D = "0000C2";

        /// <summary>
        /// Material array of ID.
        /// </summary>
        public string[] IDs { get; }

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
    }
}
