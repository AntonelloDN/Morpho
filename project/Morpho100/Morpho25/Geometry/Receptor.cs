using Morpho25.Utility;
using System;


namespace Morpho25.Geometry
{
    public class Receptor : Entity
    {
        public g3.Vector3d Geometry { get; }

        public override string Name { get; }

        public Pixel Pixel { get; private set; }

        public override Material Material { get => throw new NotImplementedException();
            protected set => throw new NotImplementedException(); }

        public Receptor(g3.Vector3d geometry, Grid grid, string name)
        {
            Geometry = geometry;
            SetPixel(grid);
            string text = name ?? "R";

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
