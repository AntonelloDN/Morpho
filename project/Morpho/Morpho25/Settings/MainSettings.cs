using Morpho25.Utility;
using System;


namespace Morpho25.Settings
{
    public enum WindAccuracy
    { 
        standard,
        quick
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
        private double _windLimit;

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

        /// <summary>
        /// Wind limit m/s
        /// </summary>
        public double WindLimit
        {
            get
            { return _windLimit; }
            set
            {
                _windLimit = value;
            }
        }

        /// <summary>
        /// Wind accuracy
        /// </summary>
        public WindAccuracy WindAccuracy { get; set; }

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
        /// Main configuration of the SIMX file.
        /// </summary>
        /// <param name="name">Name of the simulation file</param>
        /// <param name="inx">Model</param>
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
            WindLimit = 5.0;
            WindAccuracy = WindAccuracy.standard;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "mainData";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            Name,
            Inx.Workspace.ModelName + ".inx",
            Name,
            " ",
            StartDate,
            StartTime,
            SimDuration.ToString(),
            WindSpeed.ToString("n5"),
            WindDir.ToString("n5"),
            Roughness.ToString("n5"),
            InitialTemperature.ToString("n5"),
            SpecificHumidity.ToString("n5"),
            RelativeHumidity.ToString("n5"),
            WindLimit.ToString("n5"),
            WindAccuracy.ToString(),
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "simName",
            "INXFile",
            "filebaseName",
            "outDir",
            "startDate",
            "startTime",
            "simDuration",
            "windSpeed",
            "windDir",
            "z0",
            "T_H",
            "Q_H",
            "Q_2m",
            "windLimit",
            "windAccuracy"
        };

        /// <summary>
        /// String representation of main settings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return $"Config::MainSettings::{StartDate}::{StartTime}";
        }

    }

}
