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
        public FacadeMod FacadeMode { get; set; }

        /// <summary>
        /// Create new Facade settings.
        /// </summary>
        /// <param name="facadeMode"> FacadeMode for 
        /// wind resistance model at facede.</param>
        public Facades()
        {
            FacadeMode = FacadeMod.DIN6946;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "Facades";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            ((int) FacadeMode).ToString()
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "FacadeMode"
        };

        /// <summary>
        /// String representation of the Facades settings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::Facades";
    }

}
