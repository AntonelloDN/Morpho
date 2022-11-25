
using System;

namespace Morpho25.Utility
{
    /// <summary>
    /// Pixel struct.
    /// </summary>
    public struct Pixel : IEquatable<Pixel>
    {
        /// <summary>
        /// I component.
        /// </summary>
        public int I { get; set; }
        /// <summary>
        /// J component.
        /// </summary>
        public int J { get; set; }
        /// <summary>
        /// K component.
        /// </summary>
        public int K { get; set; }

        /// <summary>
        /// Create a new pixel.
        /// </summary>
        /// <param name="i">I component.</param>
        /// <param name="j">J component.</param>
        /// <param name="k">K component.</param>
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
