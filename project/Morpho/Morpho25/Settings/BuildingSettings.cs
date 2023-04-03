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

        private uint _indoorConst;
        private uint _airCondHeat;

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
        /// 1 to active the setpoint.
        /// </summary>
        public uint IndoorConst
        {
            get
            {
                return _indoorConst;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _indoorConst = value;
            }
        }

        /// <summary>
        /// 1 to active the air conditioning.
        /// </summary>
        public uint AirCondHeat
        {
            get
            {
                return _airCondHeat;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _airCondHeat = value;
            }
        }


        /// <summary>
        /// Create a new BuildingSettings object.
        /// </summary>
        /// <param name="indoorTemp">Indoor temperature [°C].</param>
        /// <param name="indoorConst">Active the setpoint.</param>
        public BuildingSettings(
            double indoorTemp, 
            Active indoorConst,
            double surfaceTemp,
            Active airCondHeat)
        {
            IndoorTemp = indoorTemp;
            IndoorConst = (uint) indoorConst;

            SurfaceTemp = surfaceTemp;
            AirCondHeat = (uint) airCondHeat;
        }
        /// <summary>
        /// String representation of building settings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::BuildingSettings";
    }

}
