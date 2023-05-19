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

        /// </summary>
        /// Enable NetCDF output.
        /// </summary>
        public Active NetCDF { get; set; }

        /// <summary>
        /// Merge NetCDF files.
        /// </summary>
        public Active NetCDFAllDataInOneFile { get; set; }

        /// <summary>
        /// NetCDF small size.
        /// </summary>
        public Active NetCDFWriteOnlySmallFiles { get; set; }

        /// <summary>
        /// Include nesting grid.
        /// </summary>
        public Active IncludeNestingGrid { get; set; }

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
        /// Enable Agent output.
        /// </summary>
        public Active WriteAgents { get; set; }


        /// </summary>
        /// Enable Atmosphere output.
        /// </summary>
        public Active WriteAtmosphere { get; set; }

        /// </summary>
        /// Enable Buildings output.
        /// </summary>
        public Active WriteBuildings { get; set; }

        /// </summary>
        /// Enable Objects output.
        /// </summary>
        public Active WriteObjects { get; set; }

        /// </summary>
        /// Enable Greenpass output.
        /// </summary>
        public Active WriteGreenpass { get; set; }

        /// </summary>
        /// Enable Nesting output.
        /// </summary>
        public Active WriteNesting { get; set; }

        /// </summary>
        /// Enable Radiation output.
        /// </summary>
        public Active WriteRadiation { get; set; }

        /// </summary>
        /// Enable Soil output.
        /// </summary>
        public Active WriteSoil { get; set; }

        /// </summary>
        /// Enable SolarAccess output.
        /// </summary>
        public Active WriteSolarAccess { get; set; }

        /// </summary>
        /// Enable Surface output.
        /// </summary>
        public Active WriteSurface { get; set; }

        /// </summary>
        /// Enable Buildings output.
        /// </summary>
        public Active WriteVegetation { get; set; }

        /// <summary>
        /// Create new OutputSettings.
        /// </summary>
        public OutputSettings()
        {
            MainFiles = 60;
            TextFiles = 60;
            NetCDF = Active.NO;
            NetCDFAllDataInOneFile = Active.NO;
            NetCDFWriteOnlySmallFiles = Active.NO;
            WriteAgents = Active.NO;
            WriteAtmosphere = Active.YES;
            WriteBuildings = Active.YES;
            WriteObjects = Active.NO;
            WriteGreenpass = Active.NO;
            WriteNesting = Active.NO;
            WriteRadiation = Active.YES;
            WriteSoil = Active.YES;
            WriteSolarAccess = Active.YES;
            WriteSurface = Active.YES;
            WriteVegetation = Active.YES;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "OutputSettings";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            MainFiles.ToString(),
            TextFiles.ToString(),
            ((int)NetCDF).ToString(),
            ((int)NetCDFAllDataInOneFile).ToString(),
            ((int)NetCDFWriteOnlySmallFiles).ToString(),
            ((int)IncludeNestingGrid).ToString(),
            ((int)WriteAgents).ToString(),
            ((int)WriteAtmosphere).ToString(),
            ((int)WriteBuildings).ToString(),
            ((int)WriteObjects).ToString(),
            ((int)WriteGreenpass).ToString(),
            ((int)WriteNesting).ToString(),
            ((int)WriteRadiation).ToString(),
            ((int)WriteSoil).ToString(),
            ((int)WriteSolarAccess).ToString(),
            ((int)WriteSurface).ToString(),
            ((int)WriteVegetation).ToString(),
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "mainFiles",
            "textFiles",
            "netCDF",
            "netCDFAllDataInOneFile",
            "netCDFWriteOnlySmallFile",
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

        /// <summary>
        /// String representation of OutputSettings.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::OutputSettings::{NetCDF}";
    }

}
