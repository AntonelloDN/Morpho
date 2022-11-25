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
        public int MinFlowsteps { get; set; }

        /// <summary>
        /// Use temperature values of EPW as boundary condition.
        /// </summary>
        public int ForceTemperature { get; set; }

        /// <summary>
        /// Use wind speed and direction values of EPW 
        /// as boundary condition.
        /// </summary>
        public int ForceWind { get; set; }

        /// <summary>
        /// Use relative humidity values 
        /// of EPW as boundary condition.
        /// </summary>
        public int ForceRelativeHumidity { get; set; }

        /// <summary>
        /// Use precipitation values 
        /// of EPW as boundary condition.
        /// </summary>
        public int ForcePrecipitation { get; set; }

        /// <summary>
        /// Use radiation and cloudiness 
        /// values of EPW as boundary condition.
        /// </summary>
        public int ForceRadClouds { get; set; }

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
            MaxWind2500 = 20.0;
            MinFlowsteps = 50;
            ForceTemperature = (int)Active.YES;
            ForceWind = (int)Active.YES;
            ForceRelativeHumidity = (int)Active.YES;
            ForcePrecipitation = (int)Active.NO;
            ForceRadClouds = (int)Active.YES;
        }
    }

}
