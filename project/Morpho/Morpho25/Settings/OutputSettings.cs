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
        /// Create new OutputSettings.
        /// </summary>
        public OutputSettings()
        {
            MainFiles = 60;
            TextFiles = 60;
            NetCDF = (int) Active.NO;
            NetCDFAllDataInOneFile = (int) Active.NO;
        }
        /// <summary>
        /// String representation of OutputSettings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::OutputSettings::{NetCDF}";
    }

}
