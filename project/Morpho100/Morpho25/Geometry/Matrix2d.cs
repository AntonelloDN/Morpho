using System.Collections.Generic;

namespace Morpho25.Geometry
{
    public class Matrix2d
    {
        private readonly string[,] _values;

        public string this[int x, int y]
        {
            get { return _values[x, y]; }
            set { _values[x, y] = value; }
        }

        public Matrix2d(int x, int y)
        {
            _values = new string[x, y];
        }

        public Matrix2d(int x, int y, string value)
        {
            _values = new string[x, y];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    _values[i, j] = value;
                }
        }

        public int GetLengthX()
        {
            return _values.GetLength(0);
        }

        public int GetLengthY()
        {
            return _values.GetLength(1);
        }

        public static Matrix2d MergeMatrix(List<Matrix2d> matrixList, string defaultValue)
        {
            Matrix2d result = new Matrix2d(matrixList[0].GetLengthX(), matrixList[0].GetLengthY(), defaultValue);

            foreach (Matrix2d matrix in matrixList)
            {
                for (int i = 0; i < matrix.GetLengthX(); i++)
                {
                    for (int j = 0; j < matrix.GetLengthY(); j++)
                    {
                        if (matrix[i, j] != defaultValue)
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
