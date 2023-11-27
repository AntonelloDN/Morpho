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

        public Model(Grid grid, Location location)
        {
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

        public Model(Grid grid, Location location, Workspace workspace)
            : this(grid, location)
        {
            Workspace = workspace;
        }

        public void Calculation()
        {
            SetDefaultMatrix(Grid);

            if (TerrainObjects.Count > 0)
                SetTerrain();

            if (Plant2dObjects.Count > 0)
                SetPlant2d();

            if (BuildingObjects.Count > 0)
                SetBuilding();

            if (Plant3dObjects.Count > 0)
                SetPlant3d();

            if (ReceptorObjects.Count > 0)
                SetReceptor();

            if (SoilObjects.Count > 0)
                SetSoil();

            if (SourceObjects.Count > 0)
                SetSource();

        }

        public override string ToString()
        {
            var label = IsDetailed ? "3D" : "2.5D";
            return String.Format("EnvimetINX::{0}", label);
        }


        private void SetBuilding()
        {
            BuildingObjects.ForEach(_ => _.SetMatrix(Grid));

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
            SoilObjects.ForEach(_ => _.SetMatrix(Grid));

            List<Soil> soils = SoilObjects.OrderBy(e => e.ID).ToList();
            List<Matrix2d> matrixList = soils.Select(e => e.IDmatrix).ToList();
            Matrix2d soilMatrix = Matrix2d.MergeMatrix(matrixList, Material.DEFAULT_SOIL);
            EnvimetMatrix["soilMatrix"] = soilMatrix;
        }

        private void SetPlant2d()
        {
            Plant2dObjects.ForEach(_ => _.SetMatrix(Grid));

            List<Plant2d> plants = Plant2dObjects.OrderBy(e => e.ID).ToList();
            List<Matrix2d> matrixList = plants.Select(e => e.IDmatrix).ToList();
            Matrix2d plantMatrix = Matrix2d.MergeMatrix(matrixList, "");
            EnvimetMatrix.Add("plantMatrix", plantMatrix);
        }

        private void SetPlant3d()
        {
            Plant3dObjects.ForEach(_ => _.SetPixel(Grid));
        }

        private void SetReceptor()
        {
            ReceptorObjects.ForEach(_ => _.SetPixel(Grid));
        }

        private void SetTerrain()
        {
            TerrainObjects.ForEach(_ => _.SetMatrix(Grid));

            List<Terrain> terrain = TerrainObjects.OrderBy(e => e.ID).ToList();
            List<Matrix2d> matrixList = terrain.Select(e => e.IDmatrix).ToList();
            Matrix2d terrainMatrix = Matrix2d.MergeMatrix(matrixList, "0");
            EnvimetMatrix["terrainMatrix"] = terrainMatrix;
        }

        private void SetSource()
        {
            SourceObjects.ForEach(_ => _.SetMatrix(Grid));

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
