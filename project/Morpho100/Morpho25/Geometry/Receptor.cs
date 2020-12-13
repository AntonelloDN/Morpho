using Morpho25.Utility;
using MorphoGeometry;
using System;


namespace Morpho25.Geometry
{
    public class Receptor : Entity
    {
        public Vector Geometry { get; }

        public override string Name { get; }

        public Pixel Pixel { get; private set; }

        public override Material Material { get => throw new NotImplementedException();
            protected set => throw new NotImplementedException(); }

        public Receptor(Grid grid, Vector geometry, string name)
        {
            Geometry = geometry;
            SetPixel(grid);
            string text = name ?? "R_";

            Name = text + String.Join("_", Pixel.I, Pixel.J, Pixel.K);
        }

        private void SetPixel(Grid grid)
        {
            Pixel = new Pixel
            {
                I = Util.ClosestValue(grid.Xaxis, Geometry.x),
                J = Util.ClosestValue(grid.Yaxis, Geometry.y),
                K = 0
            };
        }

        public override string ToString()
        {
            return String.Format("Receptor::{0}", Name);
        }
    }
}
