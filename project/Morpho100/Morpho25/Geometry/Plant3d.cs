using Morpho25.Utility;
using MorphoGeometry;
using System;

namespace Morpho25.Geometry
{
    public class Plant3d : Entity
    {
        private const int SHIFT = 1;

        public Vector Geometry { get; }

        public override Material Material
        {
            get { return _material; }
            protected set
            {
                if (value.IDs.Length != 1)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)}  must contains 1 material code.");

                _material = value;
            }

        }

        public override string Name { get; }

        public Pixel Pixel { get; private set; }

        public Plant3d(Grid grid, Vector geometry, string code, string name)
        {
            Geometry = geometry;
            Material = (code != null) ? CreateMaterial(Material.DEFAULT_PLANT_3D, code) : CreateMaterial(Material.DEFAULT_PLANT_3D);
            Name = name ?? "PlantGroup";

            SetPixel(grid);
        }

        private void SetPixel(Grid grid)
        {
            Pixel = new Pixel
            {
                I = Util.ClosestValue(grid.Xaxis, Geometry.x) + SHIFT,
                J = Util.ClosestValue(grid.Yaxis, Geometry.y) + SHIFT,
                K = 0
            };
        }

        public override string ToString()
        {
            return String.Format("Plant3D::{0}::{1}::{2}", Name, Material.IDs[0], String.Join(",", Pixel.I, Pixel.J, Pixel.K));
        }

    }
}
