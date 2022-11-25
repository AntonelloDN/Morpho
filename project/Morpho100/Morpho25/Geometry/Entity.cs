using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;

namespace Morpho25.Geometry
{
    public abstract class Entity
    {
        protected Material _material;
        protected string _name;
        protected int _ID;

        public abstract Material Material { get; protected set; }
        public abstract string Name { get; }
        public int ID {
            get { return _ID; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be positive.");

                _ID = value;
            }
        }

        protected void SetMatrix(IEnumerable<Vector> intersection, Grid grid, Matrix2d matrix, String text)
        {
            foreach (Vector vec in intersection)
            {
                int indexX = Util.ClosestValue(grid.Xaxis, vec.x);
                int indexY = Util.ClosestValue(grid.Yaxis, vec.y);
                matrix[indexX, indexY] = (text == String.Empty) ? Math.Round(vec.z, 0).ToString() : text;
            }
        }

        protected void SetMatrix(IEnumerable<Vector> intersection, Grid grid, Matrix3d matrix, String text)
        {
            foreach (Vector vec in intersection)
            {
                int indexX = Util.ClosestValue(grid.Xaxis, vec.x);
                int indexY = Util.ClosestValue(grid.Yaxis, vec.y);
                int indexZ = Util.ClosestValue(grid.Zaxis, vec.z);
                matrix[indexX, indexY, indexZ] = (text == String.Empty) ? Math.Round(vec.z, 0).ToString() : text;
            }
        }

        protected Material CreateMaterial(string defaultCode, string code = null)
        {
            string material = code ?? defaultCode;

            return new Material(new string[] { material });
        }
    }
}
