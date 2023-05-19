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
        public string UserPolluName { get; set; }

        /// <summary>
        /// User pollutant type.
        /// </summary>
        public Pollutant UserPolluType { get; set; }

        /// <summary>
        /// Dispersion and active chemistry
        /// </summary>
        public Active ActiveChem { get; set; }

        private double _userPartDiameter;
        private double _userPartDensity;
        /// <summary>
        /// Multiple source types.
        /// </summary>
        public Active MultipleSources { get; set; }
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
        public Sources()
        {
            UserPolluName = "My Pollutant";
            UserPolluType = Pollutant.CO2;
            UserPartDiameter = 10.0;
            UserPartDensity = 1.0;
            MultipleSources = Active.YES;
            ActiveChem = Active.YES;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "Sources";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            UserPolluName,
            ((int)UserPolluType).ToString(),
            UserPartDiameter.ToString("n5"),
            UserPartDensity.ToString("n5"),
            ((int)MultipleSources).ToString(),
            ((int)ActiveChem).ToString(),
            ISOPRENE
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "userPolluName",
            "userPolluType",
            "userPartDiameter",
            "userPartDensity",
            "multipleSources",
            "activeChem",
            "isoprene"
        };

        /// <summary>
        /// String representation of Pollutant object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Sources::{UserPolluName}";
    }

}
