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
        public BoundaryCondition TemperatureHumidity { get; set; }

        /// <summary>
        /// Force turbolence.
        /// </summary>
        public BoundaryCondition Turbolence { get; set; }

        /// <summary>
        /// Create Lateral Boundary Condition.
        /// 'Forced' is used when simpleforcing is activate.
        /// 'Open' copy the values of the next grid point close 
        /// to the border back to the border each timestep which 
        /// mean overstimate the influence of environment near border.
        /// 'Cyclic' describes the process of copying values 
        /// of the downstream boarder to the upstream boarder.
        /// </summary>
        public LBC()
        {
            TemperatureHumidity = BoundaryCondition.Open;
            Turbolence = BoundaryCondition.Open;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "LBC";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            ((int)TemperatureHumidity).ToString(),
            ((int)Turbolence).ToString()
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "LBC_TQ",
            "LBC_TKE"
        };

        /// <summary>
        /// String representation of LBC object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::LBC";
    }

}
