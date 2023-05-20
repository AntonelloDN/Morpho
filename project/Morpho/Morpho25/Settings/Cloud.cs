using System;


namespace Morpho25.Settings
{
    /// <summary>
    /// Cloud class.
    /// </summary>
    public class Cloud
    {
        private uint _lowClouds;
        private uint _middleClouds;
        private uint _highClouds;

        private void IsMoreThanEight(double value)
        {
            if (value < 0 || value > 8)
                throw new ArgumentException("Value must be in range (0, 8).");
        }
        /// <summary>
        /// Fraction of LOW clouds (x/8).
        /// </summary>
        public uint LowClouds
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
        public uint MiddleClouds
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
        public uint HighClouds
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
        /// Title of the XML section
        /// </summary>
        public string Title => "Clouds";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            LowClouds.ToString("n5"),
            MiddleClouds.ToString("n5"),
            HighClouds.ToString("n5")
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "lowClouds",
            "middleClouds",
            "highClouds"
        };

        /// <summary>
        /// String representation of Cloud object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Cloud::{LowClouds},{MiddleClouds},{HighClouds}";
    }

}
