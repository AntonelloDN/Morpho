using Morpho25.Geometry;
using Morpho25.IO;
using Morpho25.Management;
using Morpho25.Utility;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Morpho25.Settings
{
    /// <summary>
    /// Configuration class.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Check if value is positive.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <exception cref="ArgumentException">Negative.</exception>
        protected void ItIsPositive(double value)
        {
            if (value < 0)
                throw new ArgumentException("You cannot insert negative numbers");
        }
        /// <summary>
        /// Check if relative humidity value is between 0% and 100%.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <exception cref="ArgumentException">Wrong value.</exception>
        protected void IsHumidityOk(double value)
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Relative humidity go from 0 to 100.");
        }
    }

    /// <summary>
    /// Active enum.
    /// </summary>
    public enum Active
    {
        NO,
        YES
    }
    /// <summary>
    /// Main settings class.
    /// </summary>
    public class MainSettings : Configuration
    {
        private int _simulationDuration;
        private double _specificHumidity;
        private double _relativeHumidity;
        private double _initialTemperature;
        private string _startDate;
        private string _startTime;
        private double _windDir;
        /// <summary>
        /// Name of simulation file (*.simx).
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Model.
        /// </summary>
        public Model Inx { get; }
        /// <summary>
        /// It sets when your simulation starts. Format DD.MM.YYYY.
        /// </summary>
        public string StartDate
        {
            get { return _startDate; }
            set
            {
                DateValidation(value);
                _startDate = value;
            }
        }
        /// <summary>
        /// It sets at what time your simulation starts. Format HH:MM:SS.
        /// </summary>
        public string StartTime
        {
            get { return _startTime; }
            set
            {
                TimeValidation(value);
                _startTime = value;
            }
        }
        /// <summary>
        /// Initial wind speed (m/s).
        /// </summary>
        public double WindSpeed { get; set; }
        /// <summary>
        /// Initial wind direction (°dec).
        /// </summary>
        public double WindDir
        {
            get
            { return _windDir; }
            set
            {

                if (value < 0 || value > 360)
                    throw new ArgumentException("Angle must be in range (0, 360).");
                _windDir = value;
            }
        }
        /// <summary>
        /// Roughness.
        /// </summary>
        public double Roughness { get; set; }
        /// <summary>
        /// Initial temperature of the air (°C).
        /// </summary>
        public double InitialTemperature
        {
            get { return _initialTemperature; }
            set
            {
                _initialTemperature = value + Util.TO_KELVIN;
            }
        }
        /// <summary>
        /// Duration of simulation in hours.
        /// </summary>
        public int SimDuration
        {
            get { return _simulationDuration; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("You cannot insert negative numbers");
                _simulationDuration = value;
            }
        }
        /// <summary>
        /// Initial specific humidity of the air in 2500 m.
        /// </summary>
        public double SpecificHumidity
        {
            get
            { return _specificHumidity; }
            set
            {
                ItIsPositive(value);
                _specificHumidity = value;
            }
        }
        /// <summary>
        /// Initial relative humidity of the air in 2m (%).
        /// </summary>
        public double RelativeHumidity
        {
            get
            { return _relativeHumidity; }
            set
            {

                IsHumidityOk(value);
                _relativeHumidity = value;
            }
        }

        private void DateValidation(string value)
        {
            var pattern = @"^[0-9]{2}.[0-9]{2}.[0-9]{4}";
            var regexp = new System.Text.RegularExpressions.Regex(pattern);
            if (!regexp.IsMatch(value))
                throw new ArgumentException("Format must to be DD.MM.YYYY");
        }

        private void TimeValidation(string value)
        {
            var pattern = @"^[0-9]{2}:[0-9]{2}:[0-9]{2}";
            var regexp = new System.Text.RegularExpressions.Regex(pattern);
            if (!regexp.IsMatch(value))
                throw new ArgumentException("Format must to be HH:MM:SS");
        }
        /// <summary>
        /// Create a new main settings.
        /// </summary>
        /// <param name="name">Name of the simulation file.</param>
        /// <param name="inx">Model.</param>
        public MainSettings(string name, Model inx)
        {
            Name = name;
            Inx = inx;
            StartDate = "23.06.2018";
            StartTime = "06:00:00";
            WindSpeed = 2.5;
            WindDir = 0.0;
            Roughness = 0.01000;
            InitialTemperature = 19;
            SimDuration = 24;
            SpecificHumidity = 7.00;
            RelativeHumidity = 50;
        }
        /// <summary>
        /// String representation of main settings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return $"Config::MainSettings::{StartDate}::{StartTime}";
        }

    }
    /// <summary>
    /// Simple forcing class.
    /// </summary>
    public class SimpleForcing
    {
        /// <summary>
        /// List of temperature values to use as boundary condition (°C).
        /// </summary>
        public string Temperature { get; }
        /// <summary>
        /// List of relative humidity values to use as boundary condition (%).
        /// </summary>
        public string RelativeHumidity { get; }
        /// <summary>
        /// Number of values.
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// Create a simple forcing object.
        /// </summary>
        /// <param name="temperature">List of temperature values to use as boundary condition (°C).</param>
        /// <param name="relativeHumidity">List of relative humidity values to use as boundary condition (%)</param>
        /// <exception cref="ArgumentException"></exception>
        public SimpleForcing(List<double> temperature, List<double> relativeHumidity)
        {
            Count = temperature.Count;

            if (Count != relativeHumidity.Count)
                throw new ArgumentException("Temperature List size = Relative Humidity List size.");

            List<double> temperatureKelvin = new List<double>();
            foreach (double num in temperature)
                temperatureKelvin.Add(num + Util.TO_KELVIN);
            Temperature = String.Join(",", temperatureKelvin);
            RelativeHumidity = String.Join(",", relativeHumidity);
            Count = temperature.Count;
        }
        /// <summary>
        /// String representation of simple forcing settigns.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::SimpleForcing::count {Count}";
    }
    /// <summary>
    /// TThread class.
    /// </summary>
    public class TThread
    {
        /// <summary>
        /// Is active?
        /// </summary>
        public int UseTreading { get; }
        /// <summary>
        /// Thread priority.
        /// </summary>
        public int TThreadpriority { get; }
        /// <summary>
        /// Create a new tthread object.
        /// </summary>
        /// <param name="useTreading">Active.</param>
        public TThread(Active useTreading)
        {
            UseTreading = (int) useTreading;
            TThreadpriority = 5;
        }
        /// <summary>
        /// String representation of tthread object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Treading {UseTreading}";
    }
    /// <summary>
    /// Timesteps class.
    /// </summary>
    public class TimeSteps : Configuration
    {
        private double _sunheightStep01;
        private double _sunheightStep02;
        private double _dtStep00;
        private double _dtStep01;
        private double _dtStep02;
        /// <summary>
        /// Sun height for switching dt(0). From 0 deg to 40.00.
        /// </summary>
        public double SunheightStep01 {
            get { return _sunheightStep01; }
            set
            {
                ItIsPositive(value);
                _sunheightStep01 = value;
            }
        }
        /// <summary>
        /// Sun height for switching dt(1). From 40.00 deg to 50.00 deg.
        /// </summary>
        public double SunheightStep02
        {
            get { return _sunheightStep02; }
            set
            {
                ItIsPositive(value);
                _sunheightStep02 = value;
            }
        }
        /// <summary>
        /// Time step (s) for interval 1 dt(0).
        /// </summary>
        public double DtStep00
        {
            get { return _dtStep00; }
            set
            {
                ItIsPositive(value);
                _dtStep00 = value;
            }
        }
        /// <summary>
        /// Time step (s) for interval 1 dt(1).
        /// </summary>
        public double DtStep01
        {
            get { return _dtStep01; }
            set
            {
                ItIsPositive(value);
                _dtStep01 = value;
            }
        }
        /// <summary>
        /// Time step (s) for interval 1 dt(2).
        /// </summary>
        public double DtStep02
        {
            get { return _dtStep02; }
            set
            {
                ItIsPositive(value);
                _dtStep02 = value;
            }
        }
        /// <summary>
        /// Create time step settings.
        /// </summary>
        public TimeSteps()
        {
            SunheightStep01 = 40.00000;
            SunheightStep02 = 50.00000;
            DtStep00 = 2.00000;
            DtStep01 = 2.00000;
            DtStep02 = 1.00000;
        }
        /// <summary>
        /// String representation of TimeSteps object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::TimeSteps";
    }
    /// <summary>
    /// Model timing settings.
    /// </summary>
    public class ModelTiming : Configuration
    {
        private int _surfaceSteps;
        private int _flowSteps;
        private int _radiationSteps;
        private int _plantSteps;
        private int _sourcesSteps;
        /// <summary>
        /// Update Surface Data each ? sec.
        /// </summary>
        public int SurfaceSteps
        {
            get { return _surfaceSteps; }
            set
            {
                ItIsPositive(value);
                _surfaceSteps = value;
            }
        }
        /// <summary>
        /// Update Wind field each ? sec.
        /// </summary>
        public int FlowSteps
        {
            get { return _flowSteps; }
            set
            {
                ItIsPositive(value);
                _flowSteps = value;
            }
        }
        /// <summary>
        /// Update Radiation and Shadows each ? sec.
        /// </summary>
        public int RadiationSteps
        {
            get { return _radiationSteps; }
            set
            {
                ItIsPositive(value);
                _radiationSteps = value;
            }
        }
        /// <summary>
        /// Update Plant Data each ? sec.
        /// </summary>
        public int PlantSteps
        {
            get { return _plantSteps; }
            set
            {
                ItIsPositive(value);
                _plantSteps = value;
            }
        }
        /// <summary>
        /// Update Emmission Data each ? sec.
        /// </summary>
        public int SourcesSteps
        {
            get { return _sourcesSteps; }
            set
            {
                ItIsPositive(value);
                _sourcesSteps = value;
            }
        }
        /// <summary>
        /// Create model timing object.
        /// </summary>
        public ModelTiming()
        {
            SurfaceSteps = 30;
            FlowSteps = 900;
            RadiationSteps = 600;
            PlantSteps = 600;
            SourcesSteps = 600;
        }
        /// <summary>
        /// String representation of ModelTiming object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::ModelTiming";
    }
    /// <summary>
    /// Soil settings class.
    /// </summary>
    public class SoilSettings : Configuration
    {
        private double _tempUpperlayer;
        private double _tempMiddlelayer;
        private double _tempDeeplayer;
        private double _tempBedrockLayer;
        private double _waterUpperlayer;
        private double _waterMiddlelayer;
        private double _waterDeeplayer;
        private double _waterBedrockLayer;

        private double ToKelvin(double value)
        {
            return value + Util.TO_KELVIN;
        }
        /// <summary>
        /// Initial Temperature Upper Layer (0-20 cm) [°C].
        /// </summary>
        public double TempUpperlayer
        {
            get { return _tempUpperlayer; }
            set
            {
                _tempUpperlayer = ToKelvin(value);
            }
        }
        /// <summary>
        /// Initial Temperature Middle Layer (20-50 cm) [°C].
        /// </summary>
        public double TempMiddlelayer
        {
            get { return _tempMiddlelayer; }
            set
            {
                _tempMiddlelayer = ToKelvin(value);
            }
        }
        /// <summary>
        /// Initial Temperature Deep Layer (below 50-200 cm) [°C].
        /// </summary>
        public double TempDeeplayer
        {
            get { return _tempDeeplayer; }
            set
            {
                _tempDeeplayer = ToKelvin(value);
            }
        }
        /// <summary>
        /// Initial Temperature Bedrock Layer (200 cm) [°C].
        /// </summary>
        public double TempBedrockLayer
        {
            get { return _tempBedrockLayer; }
            set
            {
                _tempBedrockLayer = ToKelvin(value);
            }
        }
        /// <summary>
        /// Relative Humidity Upper Layer (0-20 cm).
        /// </summary>
        public double WaterUpperlayer
        {
            get { return _waterUpperlayer; }
            set
            {
                IsHumidityOk(value);
                _waterUpperlayer = value;
            }
        }
        /// <summary>
        /// Relative Humidity Middle Layer (20-50 cm).
        /// </summary>
        public double WaterMiddlelayer
        {
            get { return _waterMiddlelayer; }
            set
            {
                IsHumidityOk(value);
                _waterMiddlelayer = value;
            }
        }
        /// <summary>
        /// Relative Humidity Deep Layer (50-200 cm).
        /// </summary>
        public double WaterDeeplayer
        {
            get { return _waterDeeplayer; }
            set
            {
                IsHumidityOk(value);
                _waterDeeplayer = value;
            }
        }
        /// <summary>
        /// Relative Humidity Bedrock (below 200 cm).
        /// </summary>
        public double WaterBedrockLayer
        {
            get { return _waterBedrockLayer; }
            set
            {
                IsHumidityOk(value);
                _waterBedrockLayer = value;
            }
        }
        /// <summary>
        /// Create soil settings object.
        /// </summary>
        public SoilSettings()
        {
            TempUpperlayer = 19.85;
            TempMiddlelayer = 19.85;
            TempDeeplayer = 19.85;
            TempBedrockLayer = 19.85;
            WaterUpperlayer = 70.0;
            WaterMiddlelayer = 75.0;
            WaterDeeplayer = 75.0;
            WaterBedrockLayer = 75.0;
        }
        /// <summary>
        /// String representation of SoilSettings object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::SoilSettings";
    }
    /// <summary>
    /// Pollutant enum.
    /// </summary>
    public enum Pollutant
    {
        PM,
        CO,
        CO2,
        NO,
        NO2,
        SO2,
        NH3,
        H2O2,
        SPRAY
    }
    /// <summary>
    /// Sources settings class.
    /// </summary>
    public class Sources : Configuration
    {
        /// <summary>
        /// Isoprene index.
        /// </summary>
        public const string ISOPRENE = "0";
        /// <summary>
        /// User pollutant name.
        /// </summary>
        public string UserPolluName { get; }
        /// <summary>
        /// User pollutant type.
        /// </summary>
        public int UserPolluType { get; }
        /// <summary>
        /// Dispersion and active chemistry
        /// </summary>
        public int ActiveChem { get; }

        private double _userPartDiameter;
        private double _userPartDensity;
        /// <summary>
        /// Multiple source types.
        /// </summary>
        public int MultipleSources { get; }
        /// <summary>
        /// Particle diameter (μm).
        /// </summary>
        public double UserPartDiameter
        {
            get { return _userPartDiameter; }
            set
            {
                ItIsPositive(value);
                _userPartDiameter = value;
            }
        }
        /// <summary>
        /// Particle density (g/cm3).
        /// </summary>
        public double UserPartDensity
        {
            get { return _userPartDensity; }
            set
            {
                ItIsPositive(value);
                _userPartDensity = value;
            }
        }
        /// <summary>
        /// Create new Pollutant.
        /// </summary>
        /// <param name="userPolluName">Name of pollutant source.</param>
        /// <param name="userPolluType">Pollutant type.</param>
        /// <param name="multipleSources">If multiple sources.</param>
        /// <param name="activeChem">Set dispersion and active chemistry.</param>
        public Sources(string userPolluName, Pollutant userPolluType, Active multipleSources, Active activeChem)
        {
            UserPolluName = userPolluName;
            UserPolluType = (int) userPolluType;
            UserPartDiameter = 10.0;
            UserPartDensity = 1.0;
            MultipleSources = (int) multipleSources;
            ActiveChem = (int) activeChem;
        }
        /// <summary>
        /// String representation of Pollutant object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Sources::{UserPolluName}";
    }
    /// <summary>
    /// Turbulence enum.
    /// </summary>
    public enum TurbolenceType
    {
        MellorAndYamada,
        KatoAndLaunder,
        Lopez,
        Bruse
    }
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
    /// <summary>
    /// OutputSettings class.
    /// </summary>
    public class OutputSettings : Configuration
    {
        public const int NESTING_GRID = 0;

        private int _mainFiles;
        private int _textFiles;
        /// <summary>
        /// Number of buildings.
        /// </summary>
        public string BuildingNumbers { get; private set; }
        /// <summary>
        /// Building count.
        /// </summary>
        public int BuildingCnt { get; private set; }
        /// <summary>
        /// 1 to enable NetCDF output.
        /// </summary>
        public int NetCDF { get; set; }
        /// <summary>
        /// Merge NetCDF files.
        /// </summary>
        public int NetCDFAllDataInOneFile { get; set; }
        /// <summary>
        /// Decide in which output interval save output files.
        /// </summary>
        public int MainFiles
        {
            get { return _mainFiles; }
            set
            {
                ItIsPositive(value);
                _mainFiles = value;
            }
        }
        /// <summary>
        /// Decide in which output interval save receptor and building files.
        /// </summary>
        public int TextFiles
        {
            get { return _textFiles; }
            set
            {
                ItIsPositive(value);
                _textFiles = value;
            }
        }
        /// <summary>
        /// Write BPS.
        /// </summary>
        public int WriteBPS { get; set; }
        /// <summary>
        /// Set building numbers.
        /// </summary>
        /// <param name="buildings">Buildings.</param>
        public void SetBuildingNumber(IEnumerable<Building> buildings)
        {
            if (WriteBPS == (int) Active.YES)
            {
                BuildingNumbers = String.Join(",", buildings.Select(b => b.ID).ToList());
                BuildingCnt = buildings.Count();
            }
        }
        /// <summary>
        /// Create new OutputSettings.
        /// </summary>
        public OutputSettings()
        {
            MainFiles = 60;
            TextFiles = 60;
            NetCDF = (int) Active.NO;
            NetCDFAllDataInOneFile = (int) Active.NO;
            WriteBPS = (int) Active.NO;
            BuildingCnt = 0;
            BuildingNumbers = " ";
        }
        /// <summary>
        /// String representation of OutputSettings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::OutputSettings::{BuildingCnt}";
    }
    /// <summary>
    /// Cloud class.
    /// </summary>
    public class Cloud
    {
        private double _lowClouds;
        private double _middleClouds;
        private double _highClouds;

        private void IsMoreThanEight(double value)
        {
            if (value < 0 || value > 8)
                throw new ArgumentException("Value must be in range (0, 8).");
        }
        /// <summary>
        /// Fraction of LOW clouds (x/8).
        /// </summary>
        public double LowClouds
        {
            get { return _lowClouds; }
            set
            {
                IsMoreThanEight(value);
                _lowClouds = value;
            }
        }
        /// <summary>
        /// Fraction of MIDDLE clouds (x/8).
        /// </summary>
        public double MiddleClouds
        {
            get { return _middleClouds; }
            set
            {
                IsMoreThanEight(value);
                _middleClouds = value;
            }
        }
        /// <summary>
        /// Fraction of HIGH clouds (x/8).
        /// </summary>
        public double HighClouds
        {
            get { return _highClouds; }
            set
            {
                IsMoreThanEight(value);
                _highClouds = value;
            }
        }
        /// <summary>
        /// Create new Cloud object.
        /// </summary>
        public Cloud()
        {
            LowClouds = 0;
            MiddleClouds = 0;
            HighClouds = 0;

        }
        /// <summary>
        /// String representation of Cloud object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Cloud::{LowClouds},{MiddleClouds},{HighClouds}";
    }
    /// <summary>
    /// Background class.
    /// </summary>
    public class Background : Configuration
    {
        private double _userSpec;
        private double _no;
        private double _no2;
        private double _o3;
        private double _pm10;
        private double _pm25;
        /// <summary>
        /// User pollutant.
        /// </summary>
        public double UserSpec
        {
            get { return _userSpec; }
            set
            {
                ItIsPositive(value);
                _userSpec = value;
            }
        }
        /// <summary>
        /// Enable NO.
        /// </summary>
        public double No
        {
            get { return _no; }
            set
            {
                ItIsPositive(value);
                _no = value;
            }
        }
        /// <summary>
        /// Enable NO2.
        /// </summary>
        public double No2
        {
            get { return _no2; }
            set
            {
                ItIsPositive(value);
                _no2 = value;
            }
        }
        /// <summary>
        /// Enable O3.
        /// </summary>
        public double O3
        {
            get { return _o3; }
            set
            {
                ItIsPositive(value);
                _o3 = value;
            }
        }
        /// <summary>
        /// Enable PM10.
        /// </summary>
        public double Pm10
        {
            get { return _pm10; }
            set
            {
                ItIsPositive(value);
                _pm10 = value;
            }
        }
        /// <summary>
        /// Enable PM25.
        /// </summary>
        public double Pm25
        {
            get { return _pm25; }
            set
            {
                ItIsPositive(value);
                _pm25 = value;
            }
        }
        /// <summary>
        /// Create new Background object.
        /// </summary>
        public Background()
        {
            UserSpec = 0;
            No = 0;
            No2 = 0;
            O3 = 0;
            Pm10 = 0;
            Pm25 = 0;
        }
        /// <summary>
        /// String representation of BackGround object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::Background";
    }
    /// <summary>
    /// SolarAdjust class.
    /// </summary>
    public class SolarAdjust
    {
        private double _sWfactor;
        /// <summary>
        /// Solar adjustment factor.
        /// </summary>
        public double SWfactor
        {
            get { return _sWfactor; }
            set
            {
                if (value > 1.50 || value < 0.50)
                    throw new ArgumentException("Sw factor must be in range (0.5, 1.50).");
                _sWfactor = value;
            }
        }
        /// <summary>
        /// Create a new SolarAdjust object.
        /// </summary>
        /// <param name="sWfactor">Solar adjustment factor to apply. double in range (0.5, 1.50).</param>
        public SolarAdjust(double sWfactor)
        {
            SWfactor = sWfactor;
        }
        /// <summary>
        /// String representation of solar adjust object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::SolarAdjust";
    }
    /// <summary>
    /// BuildingSettings class.
    /// </summary>
    public class BuildingSettings
    {
        private double _indoorTemp;
        /// <summary>
        /// Indoor temperature [°C].
        /// </summary>
        public double IndoorTemp
        {
            get { return _indoorTemp; }
            set
            {
                _indoorTemp = value + Util.TO_KELVIN;
            }
        }
        /// <summary>
        /// 1 to active the setpoint.
        /// </summary>
        public int IndoorConst { get; set; }
        /// <summary>
        /// Create a new BuildingSettings object.
        /// </summary>
        /// <param name="indoorTemp">Indoor temperature [°C].</param>
        /// <param name="indoorConst">Active the setpoint.</param>
        public BuildingSettings(double indoorTemp, Active indoorConst)
        {
            IndoorTemp = indoorTemp;
            IndoorConst = (int) indoorConst;
        }
        /// <summary>
        /// String representation of building settings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::BuildingSettings";
    }
    /// <summary>
    /// IVS class.
    /// </summary>
    public class IVS
    {
        public int IVSOn { get; }

        public int IVSMem { get; }

        public IVS(Active ivsOn, Active ivsMem)
        {
            IVSOn = (int)ivsOn;
            IVSMem = (int)ivsMem;
        }

        public override string ToString() => "Config::IVS";
    }

    public class ParallelCPU
    {
        public const string CPU = "ALL";
        public override string ToString() => "Config::Parallel";
    }

    public class SOR
    {
        public int SORMode { get; }

        public SOR (Active mode)
        {
            SORMode = (int) mode;
        }

        public override string ToString() => "Config::SOR";
    }

    public class InflowAvg
    {
        public int Avg { get; }

        public InflowAvg(Active mode)
        {
            Avg = (int) mode;
        }

        public override string ToString() => "Config::InflowAvg";
    }

    public enum FacadeMod
    {
        MO,
        DIN6946
    }

    public class Facades
    {
        public int FacadeMode { get; }

        public Facades(FacadeMod facadeMode)
        {
            FacadeMode = (int) facadeMode;
        }

        public override string ToString() => "Config::Facades";
    }

    public class PlantSetting
    {
        public int CO2 { get; }
        public int LeafTransmittance { get; }
        public int TreeCalendar { get; }

        public PlantSetting(Active leafTransmittance, Active treeCalendar, int co2)
        {
            CO2 = (co2 >= 0) ? co2 : 0;
            LeafTransmittance = (int)leafTransmittance;
            TreeCalendar = (int)treeCalendar;
        }

        public override string ToString() => "Config::PlantSetting";
    }

    public enum BoundaryCondition
    {
        Open = 1,
        Forced,
        Cyclic
    }

    public class LBC
    {
        public int TemperatureHumidity { get; }
        public int Turbolence { get; }

        public LBC(BoundaryCondition temperatureHumidity, BoundaryCondition turbolence)
        {
            TemperatureHumidity = (int) temperatureHumidity;
            Turbolence = (int)turbolence;
        }

        public override string ToString() => "Config::LBC";
    }

    public class FullForcing
    {
        public string FileName { get; }

        public const string INTERPOLATION_METHOD = "linear";
        public const string NUDGING = "1";
        public const string NUNDGING_FACTOR = "1.00000";
        public const string Z_0 = "0.10000";

        public double LimitWind2500 { get; set; }
        public double MaxWind2500 { get; set; }
        public int MinFlowsteps { get; set; }

        public int ForceTemperature { get; set; }
        public int ForceWind { get; set; }
        public int ForceRelativeHumidity { get; set; }
        public int ForcePrecipitation { get; set; }
        public int ForceRadClouds { get; set; }

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
