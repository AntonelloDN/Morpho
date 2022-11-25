namespace Morpho25.Settings
{
    /// <summary>
    /// Sources settings class.
    /// </summary>
    public class Sources : Configuration
    {
        /// <summary>
        /// Isoprene index.
        /// </summary>
        public const string ISOPRENE = "0";
        /// <summary>
        /// User pollutant name.
        /// </summary>
        public string UserPolluName { get; }
        /// <summary>
        /// User pollutant type.
        /// </summary>
        public int UserPolluType { get; }
        /// <summary>
        /// Dispersion and active chemistry
        /// </summary>
        public int ActiveChem { get; }

        private double _userPartDiameter;
        private double _userPartDensity;
        /// <summary>
        /// Multiple source types.
        /// </summary>
        public int MultipleSources { get; }
        /// <summary>
        /// Particle diameter (μm).
        /// </summary>
        public double UserPartDiameter
        {
            get { return _userPartDiameter; }
            set
            {
                ItIsPositive(value);
                _userPartDiameter = value;
            }
        }
        /// <summary>
        /// Particle density (g/cm3).
        /// </summary>
        public double UserPartDensity
        {
            get { return _userPartDensity; }
            set
            {
                ItIsPositive(value);
                _userPartDensity = value;
            }
        }
        /// <summary>
        /// Create new Pollutant.
        /// </summary>
        /// <param name="userPolluName">Name of pollutant source.</param>
        /// <param name="userPolluType">Pollutant type.</param>
        /// <param name="multipleSources">If multiple sources.</param>
        /// <param name="activeChem">Set dispersion and active chemistry.</param>
        public Sources(string userPolluName, Pollutant userPolluType, Active multipleSources, Active activeChem)
        {
            UserPolluName = userPolluName;
            UserPolluType = (int) userPolluType;
            UserPartDiameter = 10.0;
            UserPartDensity = 1.0;
            MultipleSources = (int) multipleSources;
            ActiveChem = (int) activeChem;
        }
        /// <summary>
        /// String representation of Pollutant object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Sources::{UserPolluName}";
    }

}
