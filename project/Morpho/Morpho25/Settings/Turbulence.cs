namespace Morpho25.Settings
{
    /// <summary>
    /// Turbulence class.
    /// </summary>
    public class Turbulence
    {
        /// <summary>
        /// Turbulence model index.
        /// </summary>
        public TurbolenceType TurbulenceModel { get; set; }

        /// <summary>
        /// Create new Turbulence object.
        /// </summary>
        public Turbulence()
        {
            TurbulenceModel = TurbolenceType.Bruse;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "Turbulence";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            ((int)TurbulenceModel).ToString()
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "turbulenceModel"
        };

        /// <summary>
        /// String representation of the Turbulence object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::TurbulenceModel";
    }

}
