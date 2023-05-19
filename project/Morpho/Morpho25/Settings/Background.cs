namespace Morpho25.Settings
{
    /// <summary>
    /// Background class.
    /// </summary>
    public class Background : Configuration
    {
        private double _userSpec;
        private double _no;
        private double _no2;
        private double _o3;
        private double _pm10;
        private double _pm25;

        /// <summary>
        /// User pollutant.
        /// </summary>
        public double UserSpec
        {
            get { return _userSpec; }
            set
            {
                ItIsPositive(value);
                _userSpec = value;
            }
        }
        /// <summary>
        /// Enable NO.
        /// </summary>
        public double No
        {
            get { return _no; }
            set
            {
                ItIsPositive(value);
                _no = value;
            }
        }
        /// <summary>
        /// Enable NO2.
        /// </summary>
        public double No2
        {
            get { return _no2; }
            set
            {
                ItIsPositive(value);
                _no2 = value;
            }
        }
        /// <summary>
        /// Enable O3.
        /// </summary>
        public double O3
        {
            get { return _o3; }
            set
            {
                ItIsPositive(value);
                _o3 = value;
            }
        }
        /// <summary>
        /// Enable PM10.
        /// </summary>
        public double Pm10
        {
            get { return _pm10; }
            set
            {
                ItIsPositive(value);
                _pm10 = value;
            }
        }
        /// <summary>
        /// Enable PM25.
        /// </summary>
        public double Pm25
        {
            get { return _pm25; }
            set
            {
                ItIsPositive(value);
                _pm25 = value;
            }
        }
        /// <summary>
        /// Create new Background object.
        /// </summary>
        public Background()
        {
            UserSpec = 0;
            No = 0;
            No2 = 0;
            O3 = 0;
            Pm10 = 0;
            Pm25 = 0;
        }


        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "Background";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            UserSpec.ToString("n5"),
            No.ToString("n5"),
            No2.ToString("n5"),
            O3.ToString("n5"),
            Pm10.ToString("n5"),
            Pm25.ToString("n5")
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "userSpec",
            "NO",
            "NO2",
            "O3",
            "PM_10",
            "PM_2_5"
        };

        /// <summary>
        /// String representation of BackGround object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::Background";
    }

}
