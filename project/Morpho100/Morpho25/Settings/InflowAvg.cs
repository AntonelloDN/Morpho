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
        public int Avg { get; }

        /// <summary>
        /// Create a new Active averaged inflow object.
        /// </summary>
        /// <param name="mode"></param>
        public InflowAvg(Active mode)
        {
            Avg = (int) mode;
        }

        /// <summary>
        /// String representation of the Avg inflow.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::InflowAvg";
    }
}
