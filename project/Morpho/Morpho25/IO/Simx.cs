using Morpho25.Settings;
using Morpho25.Utility;
using System;
using System.IO;
using System.Text;
using System.Xml;


namespace Morpho25.IO
{
    /// <summary>
    /// Simulation definition class.
    /// </summary>
    public class Simx
    {
        /// <summary>
        /// New line.
        /// </summary>
        public const string NEWLINE = "\n";
        /// <summary>
        /// Main settings.
        /// </summary>
        public MainSettings MainSettings { get; }
        /// <summary>
        /// Simple forcing settings.
        /// </summary>
        public SimpleForcing SimpleForcing { get; set; }
        /// <summary>
        /// Thread settings.
        /// </summary>
        public TThread TThread { get; set; }
        /// <summary>
        /// Time step definition.
        /// </summary>
        public TimeSteps TimeSteps { get; set; }
        /// <summary>
        /// Timing settings.
        /// </summary>
        public ModelTiming ModelTiming { get; set; }
        /// <summary>
        /// Soil settings.
        /// </summary>
        public SoilSettings SoilSettings { get; set; }
        /// <summary>
        /// Source settings.
        /// </summary>
        public Sources Sources { get; set; }
        /// <summary>
        /// Turbolence settings.
        /// </summary>
        public Turbulence Turbulence { get; set; }
        /// <summary>
        /// Output settings.
        /// </summary>
        public OutputSettings OutputSettings { get; set; }
        /// <summary>
        /// Cloudiness settings.
        /// </summary>
        public Cloud Cloud { get; set; }
        /// <summary>
        /// Background settings.
        /// </summary>
        public Background Background { get; set; }
        /// <summary>
        /// Sun settings.
        /// </summary>
        public SolarAdjust SolarAdjust { get; set; }
        /// <summary>
        /// Building settings.
        /// </summary>
        public BuildingSettings BuildingSettings { get; set; }
        /// <summary>
        /// Radiation settings.
        /// </summary>
        public RadScheme RadScheme { get; set; }
        /// <summary>
        /// Parallel CPU settings.
        /// </summary>
        public ParallelCPU ParallelCPU { get; set; }
        /// <summary>
        /// SOR settings.
        /// </summary>
        public SOR SOR { get; set; }
        /// <summary>
        /// Flow settings.
        /// </summary>
        public InflowAvg InflowAvg { get; set; }
        /// <summary>
        /// Facades.
        /// </summary>
        public Facades Facades { get; set; }
        /// <summary>
        /// Plant settings.
        /// </summary>
        public PlantSetting PlantSetting { get; set; }
        /// <summary>
        /// Lateral boundary condition.
        /// </summary>
        public LBC LBC { get; set; }
        /// <summary>
        /// Full forcing settings.
        /// </summary>
        public FullForcing FullForcing { get; set; }
        /// <summary>
        /// Create a simulation definition.
        /// </summary>
        /// <param name="mainSettings">Main settings.</param>
        public Simx(MainSettings mainSettings)
        {
            MainSettings = mainSettings;
            SimpleForcing = null;
            TThread = null;
            TimeSteps = null;
            ModelTiming = null;
            SoilSettings = null;
            Sources = null;
            Turbulence = null;
            OutputSettings = null;
            Cloud = null;
            Background = null;
            SolarAdjust = null;
            BuildingSettings = null;
            RadScheme = null;
            SOR = null;
            InflowAvg = null;
            Facades = null;
            PlantSetting = null;
            LBC = null;
            FullForcing = null;
        }
        /// <summary>
        /// Write simulation file.
        /// </summary>
        public void WriteSimx()
        {
            var now = DateTime.Now;
            string revisionDate = now.ToString("yyyy-MM-dd HH:mm:ss");
            string filePath = Path.Combine(MainSettings
                .Inx.Workspace.ProjectFolder, MainSettings.Name + ".simx");
            string[] empty = { };

            XmlTextWriter xWriter = new XmlTextWriter(filePath, Encoding.UTF8);
            xWriter.WriteStartElement("ENVI-MET_Datafile");
            xWriter.WriteString(NEWLINE);

            // Header section
            string headerTitle = "Header";
            string[] headerTag = new string[] { "filetype", "version",
                "revisiondate", "remark", "encryptionlevel" };
            string[] headerValue = new string[] { "SIMX", "2",
                revisionDate, "Created with lb_envimet", "0" };

            Util.CreateXmlSection(xWriter, headerTitle,
                headerTag, headerValue, 0, empty);

            // Main section
            Util.CreateXmlSection(xWriter, MainSettings.Title,
                MainSettings.Tags, MainSettings.Values, 0, empty);

            if (SimpleForcing != null && FullForcing == null)
            {
                string title = "SimpleForcing";
                string[] tags = new string[] { 
                    "TAir", 
                    "Qrel" 
                };
                string[] values = new string[] { SimpleForcing
                    .Temperature, SimpleForcing.RelativeHumidity };

                Util.CreateXmlSection(xWriter, title, tags,
                    values, 0, empty);
            }

            if (TThread != null)
            {
                Util.CreateXmlSection(xWriter, TThread.Title,
                    TThread.Tags, TThread.Values, 0, empty);
            }

            if (ModelTiming != null)
            {
                Util.CreateXmlSection(xWriter, ModelTiming.Title,
                    ModelTiming.Tags, ModelTiming.Values, 0, empty);
            }

            if (SoilSettings != null)
            {
                Util.CreateXmlSection(xWriter, SoilSettings.Title,
                    SoilSettings.Tags, SoilSettings.Values, 0, empty);
            }

            if (Sources != null)
            {
                Util.CreateXmlSection(xWriter, Sources.Title,
                    Sources.Tags, Sources.Values, 0, empty);
            }

            if (Turbulence != null)
            {
                Util.CreateXmlSection(xWriter, Turbulence.Title,
                    Turbulence.Tags, Turbulence.Values, 0, empty);
            }

            if (TimeSteps != null)
            {
                string title = "TimeSteps";
                string[] tags = new string[] { 
                    "sunheight_step01",
                    "sunheight_step02", 
                    "dt_step00",
                    "dt_step01", 
                    "dt_step02"
                };
                string[] values = new string[] {
                    TimeSteps.SunheightStep01.ToString("n6"),
                    TimeSteps.SunheightStep02.ToString("n6"),
                    TimeSteps.DtStep00.ToString("n6"),
                    TimeSteps.DtStep01.ToString("n6"),
                    TimeSteps.DtStep02.ToString("n6") };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (OutputSettings != null)
            {
                string title = "OutputSettings";
                string[] tags = new string[] {
                    "mainFiles",
                    "textFiles",
                    "netCDF",
                    "netCDFAllDataInOneFile",
                    "inclNestingGrids",
                    "writeAgents",
                    "writeAtmosphere",
                    "writeBuildings",
                    "writeObjects",
                    "writeGreenpass",
                    "writeNesting",
                    "writeRadiation",
                    "writeSoil",
                    "writeSolarAccess",
                    "writeSurface",
                    "writeVegetation"
                };
                string[] values = new string[] {
                    OutputSettings.MainFiles.ToString(),
                    OutputSettings.TextFiles.ToString(),
                    OutputSettings.NetCDF.ToString(),
                    OutputSettings.NetCDFAllDataInOneFile.ToString(),
                    "0",
                    OutputSettings.WriteAgents.ToString(),
                    OutputSettings.WriteAtmosphere.ToString(),
                    OutputSettings.WriteBuildings.ToString(),
                    OutputSettings.WriteObjects.ToString(),
                    OutputSettings.WriteGreenpass.ToString(),
                    OutputSettings.WriteNesting.ToString(),
                    OutputSettings.WriteRadiation.ToString(),
                    OutputSettings.WriteSoil.ToString(),
                    OutputSettings.WriteSolarAccess.ToString(),
                    OutputSettings.WriteSurface.ToString(),
                    OutputSettings.WriteVegetation.ToString(),
                };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (Cloud != null && FullForcing == null)
            {
                string title = "Clouds";
                string[] tags = new string[] { "lowClouds",
                    "middleClouds", "highClouds" };
                string[] values = new string[] {
                    Cloud.LowClouds.ToString("n6"),
                    Cloud.MiddleClouds.ToString("n6"),
                    Cloud.HighClouds.ToString("n6") };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (Background != null)
            {
                string title = "Background";
                string[] tags = new string[] { 
                    "userSpec",
                    "NO",
                    "NO2", 
                    "O3", 
                    "PM_10", 
                    "PM_2_5"
                };
                string[] values = new string[] {
                    Background.UserSpec.ToString("n6"),
                    Background.No.ToString("n6"),
                    Background.No2.ToString("n6"),
                    Background.O3.ToString("n6"),
                    Background.Pm10.ToString("n6"),
                    Background.Pm25.ToString("n6") };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (SolarAdjust != null && FullForcing == null)
            {
                string title = "SolarAdjust";
                string[] tags = new string[] { 
                    "SWFactor"
                };
                string[] values = new string[] {
                    SolarAdjust.SWfactor.ToString("n6") };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (BuildingSettings != null)
            {
                string title = "Building";
                string[] tags = new string[] {
                    "indoorTemp",
                    "indoorConst",
                    "surfaceTemp",
                    "airConHeat",
                };
                string[] values = new string[]
                {
                    BuildingSettings.IndoorTemp.ToString("n6"),
                    BuildingSettings.IndoorConst.ToString(),
                    BuildingSettings.SurfaceTemp.ToString("n6"),
                    BuildingSettings.AirCondHeat.ToString(),
                };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (RadScheme != null)
            {
                Util.CreateXmlSection(xWriter, RadScheme.Title,
                    RadScheme.Tags, RadScheme.Values, 0, empty);
            }

            if (ParallelCPU != null)
            {
                string title = "Parallel";
                string[] tags = new string[] { "CPUdemand" };
                string[] values = new string[] { ParallelCPU.CPU };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (SOR != null)
            {
                string title = "SOR";
                string[] tags = new string[] { "SORMode" };
                string[] values = new string[] { SOR.SORMode.ToString() };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (InflowAvg != null)
            {
                string title = "InflowAvg";
                string[] tags = new string[] { "inflowAvg" };
                string[] values = new string[] {
                    InflowAvg.Avg.ToString() };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (PlantSetting != null)
            {
                string title = "PlantModel";
                string[] tags = new string[] { "CO2BackgroundPPM",
                    "LeafTransmittance", "TreeCalendar" };
                string[] values = new string[] {
                    PlantSetting.CO2.ToString(),
                    PlantSetting.LeafTransmittance.ToString(),
                    PlantSetting.TreeCalendar.ToString() };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (Facades != null)
            {
                string title = "Facades";
                string[] tags = new string[] { "FacadeMode" };
                string[] values = new string[] {
                    Facades.FacadeMode.ToString() };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (LBC != null && (SimpleForcing == null || FullForcing == null))
            {
                string title = "LBC";
                string[] tags = new string[] { "LBC_TQ", "LBC_TKE" };
                string[] values = new string[] {
                    LBC.TemperatureHumidity.ToString(),
                    LBC.Turbolence.ToString() };

                Util.CreateXmlSection(xWriter, title,
                    tags, values, 0, empty);
            }

            if (FullForcing != null)
            {
                Util.CreateXmlSection(xWriter, FullForcing.Title,
                    FullForcing.Tags, FullForcing.Values, 0, empty);
            }

            xWriter.WriteEndElement();
            xWriter.Close();
        }
    }
}
