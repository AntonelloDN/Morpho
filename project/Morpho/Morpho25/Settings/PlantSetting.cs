namespace Morpho25.Settings
{
    /// <summary>
    /// Tree settings class.
    /// </summary>
    public class PlantSetting
    {
        /// <summary>
        /// CO2 background level (ppm).
        /// The default value of the CO2 background concentration is set to 400 ppm.
        /// </summary>
        public uint CO2 { get; set; }

        /// <summary>
        /// Use old calculation model.
        /// or use user defined value.
        /// </summary>
        public Active LeafTransmittance { get; set; }

        /// <summary>
        /// Use tree calendar, 
        /// </summary>
        public Active TreeCalendar { get; set; }

        /// <summary>
        /// Create a new PlantSetting object.
        /// </summary>
        public PlantSetting()
        {
            CO2 = 400;
            LeafTransmittance = Active.YES;
            TreeCalendar = Active.YES;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "PlantModel";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            CO2.ToString("n5"),
            ((int)LeafTransmittance).ToString(),
            ((int)TreeCalendar).ToString()
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "CO2BackgroundPPM",
            "LeafTransmittance",
            "TreeCalendar"
        };

        /// <summary>
        /// String representation of PlantSetting object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::PlantSetting";
    }
}
