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
                Util.CreateXmlSection(xWriter, SimpleForcing.Title, 
                    SimpleForcing.Tags, SimpleForcing.Values, 0, empty);
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
                Util.CreateXmlSection(xWriter, TimeSteps.Title,
                    TimeSteps.Tags, TimeSteps.Values, 0, empty);
            }

            if (OutputSettings != null)
            {
                Util.CreateXmlSection(xWriter, OutputSettings.Title,
                    OutputSettings.Tags, OutputSettings.Values, 0, empty);
            }

            if (Cloud != null && FullForcing == null)
            {
                Util.CreateXmlSection(xWriter, Cloud.Title,
                    Cloud.Tags, Cloud.Values, 0, empty);
            }

            if (Background != null)
            {
                Util.CreateXmlSection(xWriter, Background.Title,
                    Background.Tags, Background.Values, 0, empty);
            }

            if (SolarAdjust != null && FullForcing == null)
            {
                Util.CreateXmlSection(xWriter, SolarAdjust.Title,
                    SolarAdjust.Tags, SolarAdjust.Values, 0, empty);
            }

            if (BuildingSettings != null)
            {
                Util.CreateXmlSection(xWriter, BuildingSettings.Title,
                    BuildingSettings.Tags, BuildingSettings.Values, 0, empty);
            }

            if (RadScheme != null)
            {
                Util.CreateXmlSection(xWriter, RadScheme.Title,
                    RadScheme.Tags, RadScheme.Values, 0, empty);
            }

            if (ParallelCPU != null)
            {
                Util.CreateXmlSection(xWriter, ParallelCPU.Title,
                    ParallelCPU.Tags, ParallelCPU.Values, 0, empty);
            }

            if (SOR != null)
            {
                Util.CreateXmlSection(xWriter, SOR.Title,
                    SOR.Tags, SOR.Values, 0, empty);
            }

            if (InflowAvg != null)
            {
                Util.CreateXmlSection(xWriter, InflowAvg.Title,
                    InflowAvg.Tags, InflowAvg.Values, 0, empty);
            }

            if (PlantSetting != null)
            {
                Util.CreateXmlSection(xWriter, PlantSetting.Title,
                    PlantSetting.Tags, PlantSetting.Values, 0, empty);
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
