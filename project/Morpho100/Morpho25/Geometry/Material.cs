using System;

namespace Morpho25.Geometry
{
    public class Material
    {
        public const string DEFAULT_WALL = "000000";
        public const string DEFAULT_ROOF = "000000";
        public const string DEFAULT_GREEN_ROOF = " ";
        public const string DEFAULT_GREEN_WALL = " ";
        public const string DEFAULT_SOIL = "000000";
        public const string DEFAULT_PLANT_2D = "0000XX";
        public const string DEFAULT_SOURCE = "0000FT";
        public const string DEFAULT_PLANT_3D = "0000C2";

        public string[] IDs { get; }

        public Material(string[] ids)
        {
            IDs = ids;
        }

        public override string ToString()
        {
            return String.Format("Material::{0}", String.Join(",", IDs));
        }
    }
}
