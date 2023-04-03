using System;
using System.Linq;

namespace Morpho25.Settings
{
    /// <summary>
    /// IVS class.
    /// </summary>
    public class RadScheme
    {
        public static readonly int[] AngleCategory = new[] { 2, 5, 10, 15, 30, 45 };

        private int _ivsHeightAngleHighRes;
        private int _ivsAzimutAngleHighRes;

        /// <summary>
        /// Height angle for IVS calculation
        /// </summary>
        public int IVSHeightAngleHighRes 
        {
            get 
            {
                return _ivsHeightAngleHighRes;
            }
            private set
            {
                if (!AngleCategory.Contains(value))
                    throw new Exception("Value must be 2, 5, 10, 15, 30, 45");
                _ivsHeightAngleHighRes = value;
            }
        }

        /// <summary>
        /// Azimut angle for IVS calculation
        /// </summary>
        public int IVSAzimutAngleHighRes
        {
            get
            {
                return _ivsAzimutAngleHighRes;
            }
            private set
            {
                if (!AngleCategory.Contains(value))
                    throw new Exception("Value must be 2, 5, 10, 15, 30, 45");
                _ivsAzimutAngleHighRes = value;
            }
        }

        public int IVSHeightAngleLowRes { get; }
        public int IVSAzimutAngleLowRes { get; }

        /// <summary>
        /// Create new radiation settings
        /// </summary>
        public RadScheme()
        {
            IVSHeightAngleHighRes = 15;
            IVSAzimutAngleHighRes = 15;

            var hIndex = AngleCategory.ToList().IndexOf(IVSHeightAngleHighRes);
            if (hIndex < 0)
            {
                IVSHeightAngleLowRes = IVSHeightAngleHighRes;
            }
            else
            {
                IVSHeightAngleLowRes = AngleCategory.ElementAt(hIndex + 1);
            }

            var aIndex = AngleCategory.ToList().IndexOf(IVSAzimutAngleHighRes);
            if (aIndex < 0)
            {
                IVSAzimutAngleLowRes = IVSAzimutAngleHighRes;
            }
            else
            {
                IVSAzimutAngleLowRes = AngleCategory.ElementAt(aIndex + 1);
            }

        }

        /// <summary>
        /// String representation of IVS.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::IVS";
    }

}
