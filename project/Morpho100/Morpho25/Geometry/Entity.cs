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
                int indexX = Array.FindIndex(grid.Xaxis, val => Math.Round(val, 4) == Math.Round(vec.x, 4));
                int indexY = Array.FindIndex(grid.Yaxis, val => Math.Round(val, 4) == Math.Round(vec.y, 4));
                matrix[indexX, indexY] = (text == String.Empty) ? Math.Round(vec.z, 0).ToString() : text;
            }
        }

        protected Material CreateMaterial(string defaultCode, string code = null)
        {
            string material = code ?? defaultCode;

            return new Material(new string[] { material });
        }
    }
}
