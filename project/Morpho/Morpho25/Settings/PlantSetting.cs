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
        public int CO2 { get; }

        /// <summary>
        /// 1 to use old calculation model.
        /// 0 to use user defined value.
        /// </summary>
        public int LeafTransmittance { get; }

        /// <summary>
        /// 1 to use tree calendar, 
        /// 0 to disable tree calendar.
        /// </summary>
        public int TreeCalendar { get; }

        /// <summary>
        /// Create a new PlantSetting object.
        /// </summary>
        /// <param name="leafTransmittance">Use new calculation method.</param>
        /// <param name="treeCalendar">Enable tree calendar.</param>
        /// <param name="co2">CO2 background level (ppm).</param>
        public PlantSetting(Active leafTransmittance, Active treeCalendar, int co2)
        {
            CO2 = (co2 >= 0) ? co2 : 0;
            LeafTransmittance = (int)leafTransmittance;
            TreeCalendar = (int)treeCalendar;
        }

        /// <summary>
        /// String representation of PlantSetting object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::PlantSetting";
    }
}
