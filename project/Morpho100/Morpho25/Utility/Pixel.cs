
using System;

namespace Morpho25.Utility
{
    public struct Pixel : IEquatable<Pixel>
    {
        public int I { get; set; }
        public int J { get; set; }
        public int K { get; set; }

        public Pixel(int i, int j, int k)
        {
            I = i;
            J = j;
            K = k;
        }

        public static bool operator ==(Pixel first, Pixel second) => first.Equals(second);
        public static bool operator !=(Pixel first, Pixel second) => !(first == second);
        public override bool Equals(object obj) => obj is Pixel other && this.Equals(other);
        public override int GetHashCode() => (I, J, K).GetHashCode();
        public bool Equals(Pixel other)
        {
            return I == other.I && J == other.J && K == other.K;
        }
    }
}
