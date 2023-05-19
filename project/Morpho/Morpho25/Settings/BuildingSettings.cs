using Morpho25.Utility;


namespace Morpho25.Settings
{
    /// <summary>
    /// BuildingSettings class.
    /// </summary>
    public class BuildingSettings
    {
        private double _indoorTemp;
        private double _surfaceTemp;

        /// <summary>
        /// Indoor temperature [°C].
        /// </summary>
        public double IndoorTemp
        {
            get { return _indoorTemp; }
            set
            {
                _indoorTemp = value + Util.TO_KELVIN;
            }
        }

        /// <summary>
        /// Surface temperature [°C] of the building.
        /// </summary>
        public double SurfaceTemp
        {
            get { return _surfaceTemp; }
            set
            {
                _surfaceTemp = value + Util.TO_KELVIN;
            }
        }

        /// <summary>
        /// Active the setpoint.
        /// </summary>
        public Active IndoorConst { get; set; }

        /// <summary>
        /// Active the air conditioning.
        /// </summary>
        public Active AirCondHeat { get; set; }

        /// <summary>
        /// Create a new BuildingSettings object.
        /// </summary>
        /// <param name="indoorTemp">Indoor temperature [°C].</param>
        /// <param name="indoorConst">Active the setpoint.</param>
        public BuildingSettings()
        {
            IndoorTemp = 19.85;
            IndoorConst = Active.YES;

            SurfaceTemp = 19.85;
            AirCondHeat = Active.NO;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "Building";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            SurfaceTemp.ToString("n5"),
            IndoorTemp.ToString("n5"),
            ((int)IndoorConst).ToString(),
            ((int)AirCondHeat).ToString(),
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "surfaceTemp",
            "indoorTemp",
            "indoorConst",
            "airConHeat",
        };

        /// <summary>
        /// String representation of building settings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::BuildingSettings";
    }

}
