using System;
using MorphoGeometry;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Grid size struct.
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// Create a new size object.
        /// </summary>
        /// <param name="origin">Origin of the grid.</param>
        /// <param name="dimension">Cell dimension.</param>
        /// <param name="numX">Number of X cells.</param>
        /// <param name="numY">Number of Y cells.</param>
        /// <param name="numZ">Number of Z cells.</param>
        public Size(Vector origin, 
            CellDimension dimension, 
            int numX, int numY, 
            int numZ)
        {
            Origin = origin;
            NumX = numX;
            NumY = numY;
            NumZ = numZ;

            DimX = dimension.X;
            DimY = dimension.Y;
            DimZ = dimension.Z;

            MinX = Origin.x;
            MinY = Origin.y;
            MaxX = Origin.x + (NumX * DimX);
            MaxY = Origin.y + (NumY * DimY);
        }

        /// <summary>
        /// Number of X cells.
        /// </summary>
        public int NumX { get; }
        /// <summary>
        /// Number of Y cells.
        /// </summary>
        public int NumY { get; }
        /// <summary>
        /// Number of Z cells.
        /// </summary>
        public int NumZ { get; }
        /// <summary>
        /// X of the lower left corner of the grid.
        /// </summary>
        public double MinX { get; }
        /// <summary>
        /// Y of the lower left corner of the grid.
        /// </summary>
        public double MinY { get; }
        /// <summary>
        /// X of the upper right corner of the grid.
        /// </summary>
        public double MaxX { get; }
        /// <summary>
        /// Y of the upper right corner of the grid.
        /// </summary>
        public double MaxY { get; }
        /// <summary>
        /// X dimension of the cell.
        /// </summary>
        public double DimX { get; }
        /// <summary>
        /// Y dimension of the cell.
        /// </summary>
        public double DimY { get; }
        /// <summary>
        /// Z dimension of the cell.
        /// </summary>
        public double DimZ { get; }
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
    }
}
