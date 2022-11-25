using System;


namespace Morpho25.Settings
{
    /// <summary>
    /// SolarAdjust class.
    /// </summary>
    public class SolarAdjust
    {
        private double _sWfactor;
        /// <summary>
        /// Solar adjustment factor.
        /// </summary>
        public double SWfactor
        {
            get { return _sWfactor; }
            set
            {
                if (value > 1.50 || value < 0.50)
                    throw new ArgumentException("Sw factor must be in range (0.5, 1.50).");
                _sWfactor = value;
            }
        }
        /// <summary>
        /// Create a new SolarAdjust object.
        /// </summary>
        /// <param name="sWfactor">Solar adjustment factor to apply. double in range (0.5, 1.50).</param>
        public SolarAdjust(double sWfactor)
        {
            SWfactor = sWfactor;
        }
        /// <summary>
        /// String representation of solar adjust object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::SolarAdjust";
    }

}
