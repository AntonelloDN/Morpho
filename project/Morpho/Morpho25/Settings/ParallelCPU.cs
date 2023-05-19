namespace Morpho25.Settings
{
    /// <summary>
    /// Parallel CPU class.
    /// </summary>
    public class ParallelCPU
    {
        /// <summary>
        /// Run parallel calculation.
        /// </summary>
        public string CPUDemand => "ALL";

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "Parallel";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            CPUDemand
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "CPUdemand",
        };

        /// <summary>
        /// String representation of ParallelCPU.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::Parallel";
    }

}
