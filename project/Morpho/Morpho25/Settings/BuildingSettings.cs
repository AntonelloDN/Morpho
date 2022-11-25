using Morpho25.Utility;


namespace Morpho25.Settings
{
    /// <summary>
    /// BuildingSettings class.
    /// </summary>
    public class BuildingSettings
    {
        private double _indoorTemp;
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
        /// 1 to active the setpoint.
        /// </summary>
        public int IndoorConst { get; set; }
        /// <summary>
        /// Create a new BuildingSettings object.
        /// </summary>
        /// <param name="indoorTemp">Indoor temperature [°C].</param>
        /// <param name="indoorConst">Active the setpoint.</param>
        public BuildingSettings(double indoorTemp, Active indoorConst)
        {
            IndoorTemp = indoorTemp;
            IndoorConst = (int) indoorConst;
        }
        /// <summary>
        /// String representation of building settings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::BuildingSettings";
    }

}
