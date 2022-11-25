using System.Collections.Generic;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Matrix 2D class.
    /// </summary>
    public class Matrix2d
    {
        private readonly string[,] _values;

        /// <summary>
        /// Get a value.
        /// </summary>
        /// <param name="x">X index.</param>
        /// <param name="y">Y index.</param>
        /// <returns>Value.</returns>
        public string this[int x, int y]
        {
            get { return _values[x, y]; }
            set { _values[x, y] = value; }
        }

        /// <summary>
        /// Create a new Matrix 2D.
        /// </summary>
        /// <param name="x">Size in X.</param>
        /// <param name="y">Size in Y.</param>
        public Matrix2d(int x, int y)
        {
            _values = new string[x, y];
        }

        /// <summary>
        /// Create a new Matrix 2D.
        /// </summary>
        /// <param name="x">Size in X.</param>
        /// <param name="y">Size in Y.</param>
        /// <param name="value">Default value to use.</param>
        public Matrix2d(int x, int y, string value)
        {
            _values = new string[x, y];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    _values[i, j] = value;
                }
        }
        /// <summary>
        /// Get X size.
        /// </summary>
        /// <returns>X size.</returns>
        public int GetLengthX()
        {
            return _values.GetLength(0);
        }
        /// <summary>
        /// Get Y size.
        /// </summary>
        /// <returns>Y size.</returns>
        public int GetLengthY()
        {
            return _values.GetLength(1);
        }
        /// <summary>
        /// Merge multiple Matrix 2D matrix.
        /// </summary>
        /// <param name="matrixList">Collection of matrix to merge.</param>
        /// <param name="mask">Value to use for merging.</param>
        /// <returns></returns>
        public static Matrix2d MergeMatrix(List<Matrix2d> matrixList, 
            string mask)
        {
            Matrix2d result = new Matrix2d(matrixList[0].GetLengthX(), 
                matrixList[0].GetLengthY(), mask);
            
            foreach (Matrix2d matrix in matrixList)
            {
                for (int i = 0; i < matrix.GetLengthX(); i++)
                {
                    for (int j = 0; j < matrix.GetLengthY(); j++)
                    {
                        if (matrix[i, j] != mask)
                        {
                            result[i, j] = matrix[i, j];
                        }
                    }
                }
            }
            return result;
        }
    }
}
