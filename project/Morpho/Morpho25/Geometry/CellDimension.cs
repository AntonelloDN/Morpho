namespace Morpho25.Geometry
{
    /// <summary>
    /// Cell dimension struct.
    /// </summary>
    public struct CellDimension
    {
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

        /// <summary>
        /// X coordinate.
        /// </summary>
        public double X { get; }
        /// <summary>
        /// Y coordinate.
        /// </summary>
        public double Y { get; }
        /// <summary>
        /// Z coordinate.
        /// </summary>
        public double Z { get; }
    }
}
