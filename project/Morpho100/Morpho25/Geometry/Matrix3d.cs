using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho25.Geometry
{
    public class Matrix3d
    {
        private readonly string[,,] _values;

        public string this[int x, int y, int z]
        {
            get { return _values[x, y, z]; }
            set { _values[x, y, z] = value; }
        }

        public Matrix3d(int x, int y, int z)
        {
            _values = new string[x, y, z];
        }

        public Matrix3d(int x, int y, int z, string value)
        {
            _values = new string[x, y, z];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for(int k = 0; k < z; k++)
                        {
                            _values[i, j, k] = value;
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

        public int GetLengthZ()
        {
            return _values.GetLength(2);
        }
    }
}
