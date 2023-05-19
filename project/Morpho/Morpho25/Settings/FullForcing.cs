using Morpho25.IO;
using Morpho25.Management;


namespace Morpho25.Settings
{
    /// <summary>
    /// FullForcing class.
    /// </summary>
    public class FullForcing
    {
        /// <summary>
        /// File name of simulation file.
        /// </summary>
        public string FileName { get; }

        public const string INTERPOLATION_METHOD = "linear";
        public const string NUDGING = "1";
        public const string NUNDGING_FACTOR = "1.00000";
        public const string Z_0 = "0.10000";

        /// <summary>
        /// Limit of wind speed at 2500 meters.
        /// </summary>
        public double LimitWind2500 { get; set; }
        /// <summary>
        /// Max wind speed at 2500 meter.
        /// </summary>
        public double MaxWind2500 { get; set; }

        /// <summary>
        /// Adjust the minimum interal for updating 
        /// the Full Forcing inflow.
        /// </summary>
        public uint MinFlowsteps { get; set; }

        /// <summary>
        /// Use temperature values of EPW as boundary condition.
        /// </summary>
        public Active ForceTemperature { get; set; }

        /// <summary>
        /// Use wind speed and direction values of EPW 
        /// as boundary condition.
        /// </summary>
        public Active ForceWind { get; set; }

        /// <summary>
        /// Use relative humidity values 
        /// of EPW as boundary condition.
        /// </summary>
        public Active ForceRelativeHumidity { get; set; }

        /// <summary>
        /// Use precipitation values 
        /// of EPW as boundary condition.
        /// </summary>
        public Active ForcePrecipitation { get; set; }

        /// <summary>
        /// Use radiation and cloudiness 
        /// values of EPW as boundary condition.
        /// </summary>
        public Active ForceRadClouds { get; set; }

        /// <summary>
        /// Create a new FullForcing settings.
        /// Force boundary condition using EPW file.
        /// </summary>
        /// <param name="epw">EPW file to use.</param>
        /// <param name="workspace">Inx Workspace object of your current project.</param>
        public FullForcing(string epw, Workspace workspace)
        {
            FileName = FoxBatch.GetFoxFile(epw, workspace);
            LimitWind2500 = 0;
            MaxWind2500 = 999.00000;
            MinFlowsteps = 30;
            ForceTemperature = Active.YES;
            ForceWind = Active.YES;
            ForceRelativeHumidity = Active.YES;
            ForcePrecipitation = Active.NO;
            ForceRadClouds = Active.YES;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "FullForcing";

        /// <summary>
        /// Values of the XML section
        /// TODO: It will be updated soon
        /// </summary>
        public string[] Values => new[] {
            FileName,
            ((int)ForceTemperature).ToString(),
            ((int)ForceRelativeHumidity).ToString(),
            ((int)ForceWind).ToString(),
            ((int)ForcePrecipitation).ToString(),
            ((int)ForceRadClouds).ToString(),
            INTERPOLATION_METHOD,
            NUDGING, 
            FullForcing.NUNDGING_FACTOR,
            MinFlowsteps.ToString(),
            LimitWind2500.ToString(),
            MaxWind2500.ToString("n5"),
            Z_0
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "fileName",
            "forceT",
            "forceQ",
            "forceWind",
            "forcePrecip",
            "forceRadClouds",
            "interpolationMethod",
            "nudging",
            "nudgingFactor",
            "minFlowsteps",
            "limitWind2500",
            "maxWind2500",
            "z_0" 
        };

        /// <summary>
        /// String representation of tthread object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::FullForcing::{FileName}";
    }

}
