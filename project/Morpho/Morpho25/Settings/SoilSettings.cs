using Morpho25.Utility;


namespace Morpho25.Settings
{
    /// <summary>
    /// Soil settings class.
    /// </summary>
    public class SoilSettings : Configuration
    {
        private double _tempUpperlayer;
        private double _tempMiddlelayer;
        private double _tempDeeplayer;
        private double _tempBedrockLayer;
        private double _waterUpperlayer;
        private double _waterMiddlelayer;
        private double _waterDeeplayer;
        private double _waterBedrockLayer;

        private double ToKelvin(double value)
        {
            return value + Util.TO_KELVIN;
        }
        /// <summary>
        /// Initial Temperature Upper Layer (0-20 cm) [°C].
        /// </summary>
        public double TempUpperlayer
        {
            get { return _tempUpperlayer; }
            set
            {
                _tempUpperlayer = ToKelvin(value);
            }
        }
        /// <summary>
        /// Initial Temperature Middle Layer (20-50 cm) [°C].
        /// </summary>
        public double TempMiddlelayer
        {
            get { return _tempMiddlelayer; }
            set
            {
                _tempMiddlelayer = ToKelvin(value);
            }
        }
        /// <summary>
        /// Initial Temperature Deep Layer (below 50-200 cm) [°C].
        /// </summary>
        public double TempDeeplayer
        {
            get { return _tempDeeplayer; }
            set
            {
                _tempDeeplayer = ToKelvin(value);
            }
        }
        /// <summary>
        /// Initial Temperature Bedrock Layer (200 cm) [°C].
        /// </summary>
        public double TempBedrockLayer
        {
            get { return _tempBedrockLayer; }
            set
            {
                _tempBedrockLayer = ToKelvin(value);
            }
        }
        /// <summary>
        /// Relative Humidity Upper Layer (0-20 cm).
        /// </summary>
        public double WaterUpperlayer
        {
            get { return _waterUpperlayer; }
            set
            {
                IsHumidityOk(value);
                _waterUpperlayer = value;
            }
        }
        /// <summary>
        /// Relative Humidity Middle Layer (20-50 cm).
        /// </summary>
        public double WaterMiddlelayer
        {
            get { return _waterMiddlelayer; }
            set
            {
                IsHumidityOk(value);
                _waterMiddlelayer = value;
            }
        }
        /// <summary>
        /// Relative Humidity Deep Layer (50-200 cm).
        /// </summary>
        public double WaterDeeplayer
        {
            get { return _waterDeeplayer; }
            set
            {
                IsHumidityOk(value);
                _waterDeeplayer = value;
            }
        }
        /// <summary>
        /// Relative Humidity Bedrock (below 200 cm).
        /// </summary>
        public double WaterBedrockLayer
        {
            get { return _waterBedrockLayer; }
            set
            {
                IsHumidityOk(value);
                _waterBedrockLayer = value;
            }
        }
        /// <summary>
        /// Create soil settings object.
        /// </summary>
        public SoilSettings()
        {
            TempUpperlayer = 19.85;
            TempMiddlelayer = 19.85;
            TempDeeplayer = 19.85;
            TempBedrockLayer = 19.85;
            WaterUpperlayer = 70.0;
            WaterMiddlelayer = 75.0;
            WaterDeeplayer = 75.0;
            WaterBedrockLayer = 75.0;
        }
        /// <summary>
        /// String representation of SoilSettings object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::SoilSettings";
    }

}
