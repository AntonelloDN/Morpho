namespace Morpho25.Settings
{
    /// <summary>
    /// Active averaged inflow class.
    /// </summary>
    public class InflowAvg
    {
        /// <summary>
        /// Enable Avg inflow.
        /// </summary>
        public Active Avg { get; set; }

        /// <summary>
        /// Create a new Active averaged inflow object.
        /// </summary>
        public InflowAvg()
        {
            Avg = Active.YES;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "InflowAvg";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            ((int)Avg).ToString()
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "inflowAvg"
        };

        /// <summary>
        /// String representation of the Avg inflow.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::InflowAvg";
    }
}
