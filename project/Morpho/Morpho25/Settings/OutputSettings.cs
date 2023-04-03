using Morpho25.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Morpho25.Settings
{
    /// <summary>
    /// OutputSettings class.
    /// </summary>
    public class OutputSettings : Configuration
    {
        public const int NESTING_GRID = 0;

        private int _mainFiles;
        private int _textFiles;

        private uint _writeAgents;
        private uint _writeAtmosphere;
        private uint _writeBuildings;
        private uint _writeObjects;
        private uint _writeGreenpass;
        private uint _writeNesting;
        private uint _writeRadiation;
        private uint _writeSoil;
        private uint _writeSolarAccess;
        private uint _writeSurface;
        private uint _writeVegetation;

        /// </summary>
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

        /// </summary>
        /// 1 to enable Agent output.
        /// </summary>
        public uint WriteAgents
        {
            get
            {
                return _writeAgents;
            }
            set 
            {
                if (value > 1)
                    value = 1;
                _writeAgents = value;
            }
        }

        /// </summary>
        /// 1 to enable Atmosphere output.
        /// </summary>
        public uint WriteAtmosphere
        {
            get
            {
                return _writeAtmosphere;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeAtmosphere = value;
            }
        }

        /// </summary>
        /// 1 to enable Buildings output.
        /// </summary>
        public uint WriteBuildings
        {
            get
            {
                return _writeBuildings;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeBuildings = value;
            }
        }

        /// </summary>
        /// 1 to enable Objects output.
        /// </summary>
        public uint WriteObjects
        {
            get
            {
                return _writeObjects;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeObjects = value;
            }
        }

        /// </summary>
        /// 1 to enable Greenpass output.
        /// </summary>
        public uint WriteGreenpass
        {
            get
            {
                return _writeGreenpass;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeGreenpass = value;
            }
        }

        /// </summary>
        /// 1 to enable Nesting output.
        /// </summary>
        public uint WriteNesting
        {
            get
            {
                return _writeNesting;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeNesting = value;
            }
        }

        /// </summary>
        /// 1 to enable Radiation output.
        /// </summary>
        public uint WriteRadiation
        {
            get
            {
                return _writeRadiation;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeRadiation = value;
            }
        }

        /// </summary>
        /// 1 to enable Soil output.
        /// </summary>
        public uint WriteSoil
        {
            get
            {
                return _writeSoil;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeSoil = value;
            }
        }

        /// </summary>
        /// 1 to enable SolarAccess output.
        /// </summary>
        public uint WriteSolarAccess
        {
            get
            {
                return _writeSolarAccess;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeSolarAccess = value;
            }
        }

        /// </summary>
        /// 1 to enable Surface output.
        /// </summary>
        public uint WriteSurface
        {
            get
            {
                return _writeSurface;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeSurface = value;
            }
        }

        /// </summary>
        /// 1 to enable Buildings output.
        /// </summary>
        public uint WriteVegetation
        {
            get
            {
                return _writeVegetation;
            }
            set
            {
                if (value > 1)
                    value = 1;
                _writeVegetation = value;
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
            WriteAgents = 0;
            WriteAtmosphere = 1;
            WriteBuildings = 1;
            WriteObjects = 0;
            WriteGreenpass = 0;
            WriteNesting = 0;
            WriteRadiation = 1;
            WriteSoil = 1;
            WriteSolarAccess = 1;
            WriteSurface = 1;
            WriteVegetation = 1;
        }

        /// <summary>
        /// String representation of OutputSettings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::OutputSettings::{NetCDF}";
    }

}
