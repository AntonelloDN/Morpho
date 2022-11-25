using Morpho25.Geometry;
using Morpho25.Management;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Morpho25.Settings
{
    public class Model
    {
        public Dictionary<string, Matrix2d> EnvimetMatrix { get; private set; }

        public Workspace Workspace { get; }
        public Grid Grid { get; }
        public Location Location { get; }
        public List<Building> BuildingObjects { get; set; }
        public List<Plant2d> Plant2dObjects { get; set; }
        public List<Plant3d> Plant3dObjects { get; set; }
        public List<Soil> SoilObjects { get; set; }
        public List<Terrain> TerrainObjects { get; set; }
        public List<Source> SourceObjects { get; set; }
        public List<Receptor> ReceptorObjects { get; set; }

        public bool IsDetailed { get; set; }
        public bool ShiftEachVoxel { get; set; }

        public Model(Grid grid, Location location, Workspace workspace)
        {
            Workspace = workspace;
            EnvimetMatrix = new Dictionary<string, Matrix2d>();
            Grid = grid;
            Location = location;
            BuildingObjects = new List<Building>();
            Plant2dObjects = new List<Plant2d>();
            Plant3dObjects = new List<Plant3d>();
            SoilObjects = new List<Soil>();
            TerrainObjects = new List<Terrain>();
            SourceObjects = new List<Source>();
            ReceptorObjects = new List<Receptor>();
        }

        public Model(Grid grid, Location location, Workspace workspace, 
            List<Building> buildingObjects)
        {
            Workspace = workspace;
            EnvimetMatrix = new Dictionary<string, Matrix2d>();
            Grid = grid;
            Location = location;
            BuildingObjects = buildingObjects;
            Plant2dObjects = new List<Plant2d>();
            Plant3dObjects = new List<Plant3d>();
            SoilObjects = new List<Soil>();
            TerrainObjects = new List<Terrain>();
            SourceObjects = new List<Source>();
            ReceptorObjects = new List<Receptor>();

            Calculation();
        }

        public Model(Grid grid, Location location, Workspace workspace,
            List<Building> buildingObjects, List<Plant2d> plant2dObjects)
        {
            Workspace = workspace;
            EnvimetMatrix = new Dictionary<string, Matrix2d>();
            Grid = grid;
            Location = location;
            BuildingObjects = buildingObjects;
            Plant2dObjects = plant2dObjects;
            Plant3dObjects = new List<Plant3d>();
            SoilObjects = new List<Soil>();
            TerrainObjects = new List<Terrain>();
            SourceObjects = new List<Source>();
            ReceptorObjects = new List<Receptor>();

            Calculation();
        }

        public void Calculation()
        {
            SetDefaultMatrix(Grid);

            if (BuildingObjects.Count > 0)
                SetBuilding();

            if (Plant2dObjects.Count > 0)
                SetPlant2d();

            if (SoilObjects.Count > 0)
                SetSoil();

            if (TerrainObjects.Count > 0)
                SetTerrain();

            if (SourceObjects.Count > 0)
                SetSource();

        }

        public override string ToString()
        {
            return String.Format("EnvimetINX::2.5D::{0}", Workspace.ModelName);
        }

        private void SetBuilding()
        {
            List<Building> buildings = BuildingObjects.OrderBy(b => b.ID).ToList();
            List<Matrix2d> topMatrixList = buildings.Select(b => b.TopMatrix).ToList();
            List<Matrix2d> bottomMatrixList = buildings.Select(b => b.BottomMatrix).ToList();
            List<Matrix2d> idMatrixList = buildings.Select(b => b.IDmatrix).ToList();

            Matrix2d topMatrix = Matrix2d.MergeMatrix(topMatrixList, "0");
            Matrix2d bottomMatrix = Matrix2d.MergeMatrix(bottomMatrixList, "0");
            Matrix2d idMatrix = Matrix2d.MergeMatrix(idMatrixList, "0");

            EnvimetMatrix["topMatrix"] = topMatrix;
            EnvimetMatrix["bottomMatrix"] = bottomMatrix;
            EnvimetMatrix["idMatrix"] = idMatrix;
        }

        private void SetSoil()
        {
            List<Soil> soils = SoilObjects.OrderBy(e => e.ID).ToList();
            List<Matrix2d> matrixList = soils.Select(e => e.IDmatrix).ToList();
            Matrix2d soilMatrix = Matrix2d.MergeMatrix(matrixList, Material.DEFAULT_SOIL);
            EnvimetMatrix["soilMatrix"] = soilMatrix;
        }

        private void SetPlant2d()
        {
            List<Plant2d> plants = Plant2dObjects.OrderBy(e => e.ID).ToList();
            List<Matrix2d> matrixList = plants.Select(e => e.IDmatrix).ToList();
            Matrix2d plantMatrix = Matrix2d.MergeMatrix(matrixList, "");
            EnvimetMatrix.Add("plantMatrix", plantMatrix);
        }

        private void SetTerrain()
        {
            List<Terrain> terrain = TerrainObjects.OrderBy(e => e.ID).ToList();
            List<Matrix2d> matrixList = terrain.Select(e => e.IDmatrix).ToList();
            Matrix2d terrainMatrix = Matrix2d.MergeMatrix(matrixList, "0");
            EnvimetMatrix["terrainMatrix"] = terrainMatrix;
        }

        private void SetSource()
        {
            List<Source> sources = SourceObjects.OrderBy(e => e.ID).ToList();
            List<Matrix2d> matrixList = sources.Select(e => e.IDmatrix).ToList();
            Matrix2d sourceMatrix = Matrix2d.MergeMatrix(matrixList, "");
            EnvimetMatrix.Add("sourceMatrix", sourceMatrix);
        }

        private void SetDefaultMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");
            Matrix2d matrixSoil = new Matrix2d(grid.Size.NumX, grid.Size.NumY, Material.DEFAULT_SOIL);
            EnvimetMatrix.Add("topMatrix", matrix);
            EnvimetMatrix.Add("bottomMatrix", matrix);
            EnvimetMatrix.Add("idMatrix", matrix);
            EnvimetMatrix.Add("terrainMatrix", matrix);
            EnvimetMatrix.Add("soilMatrix", matrixSoil);
        }
    }
}
