namespace Morpho25.Settings
{
    /// <summary>
    /// Facades class.
    /// </summary>
    public class Facades
    {
        /// <summary>
        /// FacadeMode for wind resistance model at facede.
        /// </summary>
        public int FacadeMode { get; }

        /// <summary>
        /// Create new Facade settings.
        /// </summary>
        /// <param name="facadeMode"> FacadeMode for 
        /// wind resistance model at facede.</param>
        public Facades(FacadeMod facadeMode)
        {
            FacadeMode = (int) facadeMode;
        }

        /// <summary>
        /// String representation of the Facades settings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::Facades";
    }

}
