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
        public int TurbulenceModel { get; }
        /// <summary>
        /// Create new Turbulence object.
        /// </summary>
        /// <param name="turbulenceModel">Turbulence type.</param>
        public Turbulence(TurbolenceType turbulenceModel)
        {
            TurbulenceModel = (int) turbulenceModel;
        }
        /// <summary>
        /// String representation of the Turbulence object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::TurbulenceModel";
    }

}
