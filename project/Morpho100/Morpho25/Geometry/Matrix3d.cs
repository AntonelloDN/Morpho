namespace Morpho25.Geometry
{
    /// <summary>
    /// Matrix 3D class.
    /// </summary>
    /// <typeparam name="T">Type to use.</typeparam>
    public class Matrix3d<T>
    {
        private readonly T[,,] _values;

        /// <summary>
        /// Get a value.
        /// </summary>
        /// <param name="x">X index.</param>
        /// <param name="y">Y index.</param>
        /// <param name="z">Z index.</param>
        /// <returns>Value.</returns>
        public T this[int x, int y, int z]
        {
            get { return _values[x, y, z]; }
            set { _values[x, y, z] = value; }
        }

        /// <summary>
        /// Create a new Matrix 3D.
        /// </summary>
        /// <param name="x">Size in X.</param>
        /// <param name="y">Size in Y.</param>
        /// <param name="z">Size in Z.</param>
        public Matrix3d(int x, int y, int z)
        {
            _values = new T[x, y, z];
        }

        /// <summary>
        /// Create a new Matrix 3D.
        /// </summary>
        /// <param name="x">Size in X.</param>
        /// <param name="y">Size in Y.</param>
        /// <param name="z">Size in Z.</param>
        /// <param name="value">Default value to use.</param>
        public Matrix3d(int x, int y, int z, T value)
        {
            _values = new T[x, y, z];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for(int k = 0; k < z; k++)
                        {
                            _values[i, j, k] = value;
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
        /// Get Z size.
        /// </summary>
        /// <returns>Z size.</returns>
        public int GetLengthZ()
        {
            return _values.GetLength(2);
        }
    }
}
