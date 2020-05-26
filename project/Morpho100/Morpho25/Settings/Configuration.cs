using Morpho25.Geometry;
using Morpho25.Utility;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Morpho25.Settings
{
    public class Configuration
    {
        protected void ItIsPositive(double value)
        {
            if (value < 0)
                throw new ArgumentException("You cannot insert negative numbers");
        }

        protected void IsHumidityOk(double value)
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Relative humidity go from 0 to 100.");
        }
    }

    public enum Active
    {
        NO,
        YES
    }

    public class MainSettings : Configuration
    {
        private int _simulationDuration;
        private double _specificHumidity;
        private double _relativeHumidity;
        private double _initialTemperature;
        private string _startDate;
        private string _startTime;
        private double _windDir;

        public string Name { get; }
        public Model Inx { get; }

        public string StartDate
        {
            get { return _startDate; }
            set
            {
                DateValidation(value);
                _startDate = value;
            }
        }

        public string StartTime
        {
            get { return _startTime; }
            set
            {
                TimeValidation(value);
                _startTime = value;
            }
        }

        public double WindSpeed { get; set; }

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

        public double Roughness { get; set; }

        public double InitialTemperature
        {
            get { return _initialTemperature; }
            set
            {
                _initialTemperature = value + Util.TO_KELVIN;
            }
        }

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

        public override string ToString()
        {
            return $"Config::MainSettings::{StartDate}::{StartTime}";
        }

    }

    public class SimpleForcing
    {
        public string Temperature { get; }
        public string RelativeHumidity { get; }
        public int Count { get; }

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

        public override string ToString() => $"Config::SimpleForcing::count {Count}";
    }

    public class TThread
    {
        public int UseTreading { get; }

        public int TThreadpriority { get; }

        public TThread(Active useTreading)
        {
            UseTreading = (int) useTreading;
            TThreadpriority = 5;
        }

        public override string ToString() => $"Config::Treading {UseTreading}";
    }

    public class TimeSteps : Configuration
    {
        private double _sunheightStep01;
        private double _sunheightStep02;
        private double _dtStep00;
        private double _dtStep01;
        private double _dtStep02;

        public double SunheightStep01 {
            get { return _sunheightStep01; }
            set
            {
                ItIsPositive(value);
                _sunheightStep01 = value;
            }
        }
        public double SunheightStep02
        {
            get { return _sunheightStep02; }
            set
            {
                ItIsPositive(value);
                _sunheightStep02 = value;
            }
        }
        public double DtStep00
        {
            get { return _dtStep00; }
            set
            {
                ItIsPositive(value);
                _dtStep00 = value;
            }
        }
        public double DtStep01
        {
            get { return _dtStep01; }
            set
            {
                ItIsPositive(value);
                _dtStep01 = value;
            }
        }
        public double DtStep02
        {
            get { return _dtStep02; }
            set
            {
                ItIsPositive(value);
                _dtStep02 = value;
            }
        }

        public TimeSteps()
        {
            SunheightStep01 = 40.00000;
            SunheightStep02 = 50.00000;
            DtStep00 = 2.00000;
            DtStep01 = 2.00000;
            DtStep02 = 1.00000;
        }

        public override string ToString() => "Config::TimeSteps";
    }

    public class ModelTiming : Configuration
    {
        private int _surfaceSteps;
        private int _flowSteps;
        private int _radiationSteps;
        private int _plantSteps;
        private int _sourcesSteps;

        public int SurfaceSteps
        {
            get { return _surfaceSteps; }
            set
            {
                ItIsPositive(value);
                _surfaceSteps = value;
            }
        }

        public int FlowSteps
        {
            get { return _flowSteps; }
            set
            {
                ItIsPositive(value);
                _flowSteps = value;
            }
        }

        public int RadiationSteps
        {
            get { return _radiationSteps; }
            set
            {
                ItIsPositive(value);
                _radiationSteps = value;
            }
        }

        public int PlantSteps
        {
            get { return _plantSteps; }
            set
            {
                ItIsPositive(value);
                _plantSteps = value;
            }
        }

        public int SourcesSteps
        {
            get { return _sourcesSteps; }
            set
            {
                ItIsPositive(value);
                _sourcesSteps = value;
            }
        }

        public ModelTiming()
        {
            SurfaceSteps = 30;
            FlowSteps = 900;
            RadiationSteps = 600;
            PlantSteps = 600;
            SourcesSteps = 600;
        }

        public override string ToString() => "Config::ModelTiming";
    }

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

        public double TempUpperlayer
        {
            get { return _tempUpperlayer; }
            set
            {
                _tempUpperlayer = ToKelvin(value);
            }
        }

        public double TempMiddlelayer
        {
            get { return _tempMiddlelayer; }
            set
            {
                _tempMiddlelayer = ToKelvin(value);
            }
        }

        public double TempDeeplayer
        {
            get { return _tempDeeplayer; }
            set
            {
                _tempDeeplayer = ToKelvin(value);
            }
        }

        public double TempBedrockLayer
        {
            get { return _tempBedrockLayer; }
            set
            {
                _tempBedrockLayer = ToKelvin(value);
            }
        }

        public double WaterUpperlayer
        {
            get { return _waterUpperlayer; }
            set
            {
                IsHumidityOk(value);
                _waterUpperlayer = value;
            }
        }

        public double WaterMiddlelayer
        {
            get { return _waterMiddlelayer; }
            set
            {
                IsHumidityOk(value);
                _waterMiddlelayer = value;
            }
        }

        public double WaterDeeplayer
        {
            get { return _waterDeeplayer; }
            set
            {
                IsHumidityOk(value);
                _waterDeeplayer = value;
            }
        }

        public double WaterBedrockLayer
        {
            get { return _waterBedrockLayer; }
            set
            {
                IsHumidityOk(value);
                _waterBedrockLayer = value;
            }
        }

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

        public override string ToString() => "Config::SoilSettings";
    }

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

    public class Sources : Configuration
    {
        public const string ISOPRENE = "0";

        public string UserPolluName { get; }
        public int UserPolluType { get; }
        public int ActiveChem { get; }

        private double _userPartDiameter;
        private double _userPartDensity;

        public int MultipleSources { get; }

        public double UserPartDiameter
        {
            get { return _userPartDiameter; }
            set
            {
                ItIsPositive(value);
                _userPartDiameter = value;
            }
        }

        public double UserPartDensity
        {
            get { return _userPartDensity; }
            set
            {
                ItIsPositive(value);
                _userPartDensity = value;
            }
        }

        public Sources(string userPolluName, Pollutant userPolluType, Active multipleSources, Active activeChem)
        {
            UserPolluName = userPolluName;
            UserPolluType = (int) userPolluType;
            UserPartDiameter = 10.0;
            UserPartDensity = 1.0;
            MultipleSources = (int) multipleSources;
            ActiveChem = (int) activeChem;
        }

        public override string ToString() => $"Config::Sources::{UserPolluName}";
    }

    public enum TurbolenceType
    {
        MellorAndYamada,
        KatoAndLaunder,
        Lopez,
        Bruse
    }

    public class Turbulence
    {
        public int TurbulenceModel { get; }

        public Turbulence(TurbolenceType turbulenceModel)
        {
            TurbulenceModel = (int) turbulenceModel;
        }

        public override string ToString() => "Config::TurbulenceModel";
    }

    public class OutputSettings : Configuration
    {
        public const int NESTING_GRID = 0;

        private int _mainFiles;
        private int _textFiles;

        public string BuildingNumbers { get; private set; }
        public int BuildingCnt { get; private set; }

        public int NetCDF { get; set; }

        public int NetCDFAllDataInOneFile { get; set; }

        public int MainFiles
        {
            get { return _mainFiles; }
            set
            {
                ItIsPositive(value);
                _mainFiles = value;
            }
        }

        public int TextFiles
        {
            get { return _textFiles; }
            set
            {
                ItIsPositive(value);
                _textFiles = value;
            }
        }

        public int WriteBPS { get; set; }

        public void SetBuildingNumber(IEnumerable<Building> buildings)
        {
            if (WriteBPS == (int) Active.YES)
            {
                BuildingNumbers = String.Join(",", buildings.Select(b => b.ID).ToList());
                BuildingCnt = buildings.Count();
            }
        }

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

        public override string ToString() => $"Config::OutputSettings::{BuildingCnt}";
    }

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

        public double LowClouds
        {
            get { return _lowClouds; }
            set
            {
                IsMoreThanEight(value);
                _lowClouds = value;
            }
        }

        public double MiddleClouds
        {
            get { return _middleClouds; }
            set
            {
                IsMoreThanEight(value);
                _middleClouds = value;
            }
        }

        public double HighClouds
        {
            get { return _highClouds; }
            set
            {
                IsMoreThanEight(value);
                _highClouds = value;
            }
        }

        public Cloud()
        {
            LowClouds = 0;
            MiddleClouds = 0;
            HighClouds = 0;

        }

        public override string ToString() => $"Config::Cloud::{LowClouds},{MiddleClouds},{HighClouds}";
    }

    public class Background : Configuration
    {
        private double _userSpec;
        private double _no;
        private double _no2;
        private double _o3;
        private double _pm10;
        private double _pm25;

        public double UserSpec
        {
            get { return _userSpec; }
            set
            {
                ItIsPositive(value);
                _userSpec = value;
            }
        }

        public double No
        {
            get { return _no; }
            set
            {
                ItIsPositive(value);
                _no = value;
            }
        }

        public double No2
        {
            get { return _no2; }
            set
            {
                ItIsPositive(value);
                _no2 = value;
            }
        }

        public double O3
        {
            get { return _o3; }
            set
            {
                ItIsPositive(value);
                _o3 = value;
            }
        }

        public double Pm10
        {
            get { return _pm10; }
            set
            {
                ItIsPositive(value);
                _pm10 = value;
            }
        }

        public double Pm25
        {
            get { return _pm25; }
            set
            {
                ItIsPositive(value);
                _pm25 = value;
            }
        }

        public Background()
        {
            UserSpec = 0;
            No = 0;
            No2 = 0;
            O3 = 0;
            Pm10 = 0;
            Pm25 = 0;
        }

        public override string ToString() => "Config::Background";
    }

    public class SolarAdjust
    {
        private double _sWfactor;

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

        public SolarAdjust(double sWfactor)
        {
            SWfactor = sWfactor;
        }

        public override string ToString() => "Config::SolarAdjust";
    }

    public class BuildingSettings
    {
        private double _indoorTemp;

        public double IndoorTemp
        {
            get { return _indoorTemp; }
            set
            {
                _indoorTemp = value + Util.TO_KELVIN;
            }
        }

        public int IndoorConst { get; set; }

        public BuildingSettings(double indoorTemp, Active indoorConst)
        {
            IndoorTemp = indoorTemp;
            IndoorConst = (int) indoorConst;
        }

        public override string ToString() => "Config::BuildingSettings";
    }

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

}
