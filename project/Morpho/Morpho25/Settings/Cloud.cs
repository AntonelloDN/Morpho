using System;


namespace Morpho25.Settings
{
    /// <summary>
    /// Cloud class.
    /// </summary>
    public class Cloud
    {
        private double _lowClouds;
        private double _middleClouds;
        private double _highClouds;

        private void IsMoreThanEight(double value)
        {
            if (value < 0 || value > 8)
                throw new ArgumentException("Value must be in range (0, 8).");
        }
        /// <summary>
        /// Fraction of LOW clouds (x/8).
        /// </summary>
        public double LowClouds
        {
            get { return _lowClouds; }
            set
            {
                IsMoreThanEight(value);
                _lowClouds = value;
            }
        }
        /// <summary>
        /// Fraction of MIDDLE clouds (x/8).
        /// </summary>
        public double MiddleClouds
        {
            get { return _middleClouds; }
            set
            {
                IsMoreThanEight(value);
                _middleClouds = value;
            }
        }
        /// <summary>
        /// Fraction of HIGH clouds (x/8).
        /// </summary>
        public double HighClouds
        {
            get { return _highClouds; }
            set
            {
                IsMoreThanEight(value);
                _highClouds = value;
            }
        }
        /// <summary>
        /// Create new Cloud object.
        /// </summary>
        public Cloud()
        {
            LowClouds = 0;
            MiddleClouds = 0;
            HighClouds = 0;

        }
        /// <summary>
        /// String representation of Cloud object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Cloud::{LowClouds},{MiddleClouds},{HighClouds}";
    }

}
