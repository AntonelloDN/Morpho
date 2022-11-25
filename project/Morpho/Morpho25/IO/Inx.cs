using System;
using System.Linq;
using System.Xml;
using System.Text;
using Morpho25.Utility;
using Morpho25.Settings;
using Morpho25.Geometry;
using System.Collections.Generic;
using System.Data;

namespace Morpho25.IO
{
    /// <summary>
    /// INX File class.
    /// </summary>
    public class Inx
    {
        const char NEWLINE = '\n';
        const string VERSION = "404";
        const string CHECK_SUM = "6104088";
        /// <summary>
        /// Model.
        /// </summary>
        public Model Model { get; }
        /// <summary>
        /// Matrix 2D of the terrain.
        /// </summary>
        public string TerrainMatrix { get; private set; }
        /// <summary>
        /// Matrix 2D of the soils.
        /// </summary>
        public string SoilMatrix { get; set; }

        /// <summary>
        /// Create a new INX File.
        /// </summary>
        /// <param name="model">Model.</param>
        public Inx(Model model)
        {
            Model = model;
            TerrainMatrix = EnvimetUtility.GetASCIImatrix(
                Model.EnvimetMatrix["terrainMatrix"]);
            SoilMatrix = EnvimetUtility.GetASCIImatrix(
                Model.EnvimetMatrix["soilMatrix"]);
        }
        /// <summary>
        /// Create a new INX File. 2.5D only.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="ASCIIterrain">ASCII matrix for DEM.</param>
        public Inx(Model model, string ASCIIterrain)
        {
            Model = model;
            if (IsASCIIcorrect(ASCIIterrain))
                TerrainMatrix = ASCIIterrain;
            SoilMatrix = EnvimetUtility.GetASCIImatrix(
                Model.EnvimetMatrix["soilMatrix"]);
            // 2D only
            Model.IsDetailed = false;
        }

        private bool IsASCIIcorrect(string ASCIImatrix)
        {
            // ASCII envimet matrix are made by integers. I
            // do not add validation for now.

            string[] yDirection = ASCIImatrix.Split('\n');
            if (yDirection.Length != Model.Grid.Size.NumY)
                throw new ArgumentOutOfRangeException(
                    $"{nameof(ASCIImatrix)} must contain {Model.Grid.Size.NumY} elements in Y.");
            foreach (string y in yDirection)
            {
                string[] xDirection = y.Split(',');
                if (xDirection.Length != Model.Grid.Size.NumX)
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(ASCIImatrix)} must contain {Model.Grid.Size.NumX} elements in X.");
            }
            return true;
        }
        /// <summary>
        /// Write INX File on machine.
        /// </summary>
        public void WriteInx()
        {
            var now = DateTime.Now;
            string revisionDate = now.ToString("yyyy-MM-dd HH:mm:ss");

            // get objects
            Grid grid = Model.Grid;
            Location location = Model.Location;
            List<Terrain> terrains = Model.TerrainObjects;
            List<Building> buildings = Model.BuildingObjects;
            List<Plant3d> plant3d = Model.Plant3dObjects;
            List<Receptor> receptors = Model.ReceptorObjects;

            // Is 3D?
            var is3D = Model.IsDetailed;
            var shiftVoxels = Model.ShiftEachVoxel;

            // get ascii matrix
            string topMatrix = EnvimetUtility.GetASCIImatrix(Model.EnvimetMatrix["topMatrix"]);
            string bottomMatrix = EnvimetUtility.GetASCIImatrix(Model.EnvimetMatrix["bottomMatrix"]);
            string IDmatrix = EnvimetUtility.GetASCIImatrix(Model.EnvimetMatrix["idMatrix"]);
            string zeroMatrix = EnvimetUtility.GetASCIImatrix(new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0"));

            // start with xml
            XmlTextWriter xWriter = new XmlTextWriter(Model.Workspace.ModelPath, Encoding.UTF8);
            xWriter.WriteStartElement("ENVI-MET_Datafile");
            xWriter.WriteString(NEWLINE + " ");

            string[] empty = { };

            string headerTitle = "Header";
            string[] headerTag = new string[] { "filetype", "version", "revisiondate", "remark", "checksum", "encryptionlevel" };
            string[] headerValue = new string[] { "INPX ENVI-met Area Input File", VERSION, revisionDate, "Created with Envimet INX for Grasshopper", CHECK_SUM, "0" };
            Util.CreateXmlSection(xWriter, headerTitle, headerTag, headerValue, 0, empty);

            string baseDataTitle = "baseData";
            string[] baseDataTag = new string[] { "modelDescription", "modelAuthor", "modelcopyright" };
            string[] baseDataValue = new string[] { " A brave new area ", " Grasshopper envimet ", "The creator or distributor is responsible for following Copyright Laws" };
            Util.CreateXmlSection(xWriter, baseDataTitle, baseDataTag, baseDataValue, 0, empty);

            string useSplitting = null;
            string verticalStretch = null;
            string startStretch = null;
            string gridsZ = (grid.IsSplitted)
                ? (grid.Size.NumZ - 4).ToString()
                : grid.Size.NumZ.ToString();
            string useTelescoping = null;
            string gridsI = (grid.Size.NumX).ToString();
            string gridsJ = (grid.Size.NumY).ToString();
            string[] attribute2dElements = { "matrix-data", gridsI, gridsJ };
            string dx = grid.Size.DimX.ToString("n5");
            string dy = grid.Size.DimY.ToString("n5");
            string dz = grid.Size.DimZ.ToString("n5");

            if (grid.Telescope > 0)
            {
                useTelescoping = "1";
                useSplitting = "0";
                verticalStretch = grid.Telescope.ToString("n5");
                startStretch = grid.StartTelescopeHeight.ToString("n5");
                if (grid.CombineGridType)
                    useSplitting = "1";
            }
            else
            {
                useTelescoping = "0";
                useSplitting = "1";
                verticalStretch = "0";
                startStretch = "0";
            }

            string modelGeometryTitle = "modelGeometry";
            string[] modelGeometryTag = new string[] { "grids-I", "grids-J", "grids-Z", "dx", "dy", "dz-base", "useTelescoping_grid", "useSplitting", "verticalStretch", "startStretch", "has3DModel", "isFull3DDesign" };
            string[] modelGeometryValue = new string[] { gridsI, gridsJ, gridsZ, dx, dy, dz, useTelescoping, useSplitting, verticalStretch, startStretch, is3D ? "1" : "0", is3D ? "1" : "0" };
            Util.CreateXmlSection(xWriter, modelGeometryTitle, modelGeometryTag, modelGeometryValue, 0, empty);

            string nestingAreaTitle = "nestingArea";
            string[] nestingAreaTag = new string[] { "numberNestinggrids", "soilProfileA", "soilProfileB" };
            string[] nestingAreaValue = new string[] { grid.NestingGrids.NumberOfCells.ToString(), grid.NestingGrids.FirstMaterial, grid.NestingGrids.SecondMaterial };
            Util.CreateXmlSection(xWriter, nestingAreaTitle, nestingAreaTag, nestingAreaValue, 0, empty);

            string locationDataTitle = "locationData";

            string utmZone = " ", realworldLowerLeftX = Location.REALWORLD_POINT, realworldLowerLeftY = Location.REALWORLD_POINT;

            if (location.UTM != null)
            {
                utmZone = location.UTM.UTMzone;
                realworldLowerLeftX = location.UTM.UTMesting.ToString();
                realworldLowerLeftY = location.UTM.UTMnorthing.ToString();
            }

            string[] locationDataTag = new string[] { "modelRotation", "projectionSystem", "UTMZone", "realworldLowerLeft_X", "realworldLowerLeft_Y", "locationName", "location_Longitude", "location_Latitude", "locationTimeZone_Name", "locationTimeZone_Longitude" };
            string[] locationDataValue = new string[] { location.ModelRotation.ToString("n5"), Location.PROJECTION_SYSTEM, utmZone, realworldLowerLeftX, realworldLowerLeftY, location.LocationName, location.Longitude.ToString("n5"), location.Latitude.ToString("n5"), location.TimeZone, location.TimezoneReference.ToString("n5") };
            Util.CreateXmlSection(xWriter, locationDataTitle, locationDataTag, locationDataValue, 0, empty);

            string defaultSettingsTitle = "defaultSettings";
            string[] defaultSettingsTag = new string[] { "commonWallMaterial", "commonRoofMaterial" };
            string[] defaultSettingsValue = new string[] { Material.DEFAULT_WALL, Material.DEFAULT_ROOF };
            Util.CreateXmlSection(xWriter, defaultSettingsTitle, defaultSettingsTag, defaultSettingsValue, 0, empty);

            string buildings2DTitle = "buildings2D";
            string[] buildings2DTag = new string[] { "zTop", "zBottom", "buildingNr", "fixedheight" };
            string[] buildings2DValue = new string[] { NEWLINE + topMatrix, NEWLINE + bottomMatrix, NEWLINE + IDmatrix, NEWLINE + zeroMatrix };
            Util.CreateXmlSection(xWriter, buildings2DTitle, buildings2DTag, buildings2DValue, 1, attribute2dElements);

            if (Model.Plant2dObjects.Count > 0)
            {
                string plantMatrix = EnvimetUtility.GetASCIImatrix(Model.EnvimetMatrix["plantMatrix"]);
                string simpleplants2DTitle = "simpleplants2D";
                string[] simpleplants2DTag = new string[] { "ID_plants1D" };
                string[] simpleplants2DValue = new string[] { NEWLINE + plantMatrix };
                Util.CreateXmlSection(xWriter, simpleplants2DTitle, simpleplants2DTag, simpleplants2DValue, 1, attribute2dElements);
            }

            if (plant3d.Count > 0)
            {
                foreach (Plant3d plant in plant3d)
                {
                    string plants3DTitle = "3Dplants";
                    string[] plants3DTag = new string[] { "rootcell_i", "rootcell_j", "rootcell_k", "plantID", "name", "observe" };
                    string[] plants3DValue = new string[] { plant.Pixel.I.ToString(), plant.Pixel.J.ToString(), plant.Pixel.K.ToString(), plant.Material.IDs.Last(), plant.Name, "0" };
                    Util.CreateXmlSection(xWriter, plants3DTitle, plants3DTag, plants3DValue, 0, empty);
                }
            }

            if (receptors.Count > 0)
            {
                foreach (Receptor receptor in receptors)
                {
                    string receptorsTitle = "Receptors";
                    string[] receptorsTag = new string[] { "cell_i", "cell_j", "name" };
                    string[] receptorsValue = new string[] { receptor.Pixel.I.ToString(), receptor.Pixel.J.ToString(), receptor.Name };
                    Util.CreateXmlSection(xWriter, receptorsTitle, receptorsTag, receptorsValue, 0, empty);
                }
            }

            string soils2DTitle = "soils2D";
            string[] soils2DTag = new string[] { "ID_soilprofile" };
            string[] soils2DValue = new string[] { NEWLINE + SoilMatrix };
            Util.CreateXmlSection(xWriter, soils2DTitle, soils2DTag, soils2DValue, 1, attribute2dElements);

            string demTitle = "dem";
            string[] demDTag = new string[] { "terrainheight" };
            string[] demValue = new string[] { NEWLINE + TerrainMatrix };
            Util.CreateXmlSection(xWriter, demTitle, demDTag, demValue, 1, attribute2dElements);

            if (Model.SourceObjects.Count > 0)
            {
                string sourceMatrix = EnvimetUtility.GetASCIImatrix(Model.EnvimetMatrix["sourceMatrix"]);
                string sources2DTitle = "sources2D";
                string[] sources2DTag = new string[] { "ID_sources" };
                string[] sources2DValue = new string[] { NEWLINE + sourceMatrix };
                Util.CreateXmlSection(xWriter, sources2DTitle, sources2DTag, sources2DValue, 1, attribute2dElements);
            }

            if (buildings.Count > 0)
            {
                foreach (Building building in buildings)
                {
                    string buildinginfoTitle = "Buildinginfo";
                    string[] buildinginfoTag = new string[] { "BuildingInternalNr", "BuildingName", "BuildingWallMaterial", "BuildingRoofMaterial", "BuildingFacadeGreening", "BuildingRoofGreening" };
                    string[] buildinginfoValue = new string[] { building.ID.ToString(), building.Name, building.Material.IDs[0], building.Material.IDs[1], building.Material.IDs[2], building.Material.IDs[3] };
                    Util.CreateXmlSection(xWriter, buildinginfoTitle, buildinginfoTag, buildinginfoValue, 0, empty);
                }
            }

            if (!is3D)
            {
                xWriter.WriteEndElement();
                xWriter.Close();
                return;
            }

            // 3D part
            var gridsK = grid.Size.NumZ.ToString();
            string[] attribute3dElements = { "sparematrix-3D", gridsI, gridsJ, gridsK, "" };
            string[] attribute3dBuildings3D = { "sparematrix-3D", gridsI, gridsJ, gridsK, "0" };
            string[] attribute3dDem3D = { "sparematrix-3D", gridsI, gridsJ, gridsK, "0.00000" };

            string modelGeometry3DTitle = "modelGeometry3D";
            string[] modelGeometry3DTag = new string[] { "grids3D-I", "grids3D-J", "grids3D-K" };
            string[] modelGeometry3DValue = new string[] { gridsI, gridsJ, gridsK };
            Util.CreateXmlSection(xWriter, modelGeometry3DTitle, modelGeometry3DTag, modelGeometry3DValue, 0, empty);

            // 3D ID
            var terrainPixels = terrains.SelectMany(_ => _.Pixels)
                .ToList();
            if (!terrainPixels.Any()) terrainPixels = null;

            var idMatrix = new List<string>() { string.Empty };
            var wallMatrix = new List<string>() { string.Empty };
            var greenMatrix = new List<string>() { string.Empty };

            foreach (var building in buildings)
            {
                building.SetMatrix3d(grid, terrainPixels, shiftVoxels);
                idMatrix.AddRange(building.BuildingIDrows);
                wallMatrix.AddRange(building.BuildingWallRows);
                greenMatrix.AddRange(building.BuildingGreenWallRows);
            }
            idMatrix.Add(string.Empty);
            wallMatrix.Add(string.Empty);
            greenMatrix.Add(string.Empty);

            string buildings3DTitle = "buildings3D";
            string[] buildings3DTag = new string[] { "buildingFlagAndNr" };
            string[] buildings3DValue = new string[] { String.Join("\n", idMatrix) };
            Util.CreateXmlSection(xWriter, buildings3DTitle, buildings3DTag, buildings3DValue, 2, attribute3dBuildings3D);

            var demMatrix = new List<string>() { string.Empty };
            foreach (var terrain in terrains)
            {
                demMatrix.AddRange(terrain.TerrainIDrows);
            }
            demMatrix.Add(string.Empty);

            string dem3DTitle = "dem3D";
            string[] dem3DTag = new string[] { "terrainflag" };
            string[] dem3DValue = new string[] { String.Join("\n", demMatrix) };
            Util.CreateXmlSection(xWriter, dem3DTitle, dem3DTag, dem3DValue, 2, attribute3dDem3D);

            string wallDBTitle = "WallDB";
            string[] wallDBTag = new string[] { "ID_wallDB" };
            string[] wallDBValue = new string[] { String.Join("\n", wallMatrix) };
            Util.CreateXmlSection(xWriter, wallDBTitle, wallDBTag, wallDBValue, 2, attribute3dElements);

            string singleWallDBTitle = "SingleWallDB";
            string[] singleWallDBTag = new string[] { "ID_singlewallDB" };
            string[] singleWallDBValue = new string[] { "\n" };
            Util.CreateXmlSection(xWriter, singleWallDBTitle, singleWallDBTag, singleWallDBValue, 2, attribute3dElements);

            string greeningDBTitle = "GreeningDB";
            string[] greeningDBTag = new string[] { "ID_GreeningDB" };
            string[] greeningDBValue = new string[] { String.Join("\n", greenMatrix) };
            Util.CreateXmlSection(xWriter, greeningDBTitle, greeningDBTag, greeningDBValue, 2, attribute3dElements);

            xWriter.WriteEndElement();
            xWriter.Close();
        }
    }
}
