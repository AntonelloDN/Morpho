namespace Morpho25.Settings
{
    /// <summary>
    /// Lateral Boundary Condition (LBC) class.
    /// </summary>
    public class LBC
    {
        /// <summary>
        /// Force temperature and humidity
        /// </summary>
        public int TemperatureHumidity { get; }

        /// <summary>
        /// Force turbolence.
        /// </summary>
        public int Turbolence { get; }

        /// <summary>
        /// Create Lateral Boundary Condition.
        /// 'Forced' is used when simpleforcing is activate.
        /// 'Open' copy the values of the next grid point close 
        /// to the border back to the border each timestep which 
        /// mean overstimate the influence of environment near border.
        /// 'Cyclic' describes the process of copying values 
        /// of the downstream boarder to the upstream boarder.
        /// </summary>
        /// <param name="temperatureHumidity">Force temperature and humidity.</param>
        /// <param name="turbolence">Force turbolence.</param>
        public LBC(BoundaryCondition temperatureHumidity, 
            BoundaryCondition turbolence)
        {
            TemperatureHumidity = (int) temperatureHumidity;
            Turbolence = (int)turbolence;
        }

        /// <summary>
        /// String representation of LBC object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::LBC";
    }

}
