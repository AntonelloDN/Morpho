using Morpho25.Utility;
using MorphoGeometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Morpho25.Geometry
{
    [DisplayName("Terrain")]
    /// <summary>
    /// Terrain class.
    /// </summary>
    public class Terrain : Entity, IEquatable<Terrain>
    {
        [DisplayName("Name")]
        [Description("Name of the terrain group")]
        [JsonProperty("name")]
        /// <summary>
        /// Name of the terrain.
        /// </summary>
        public override string Name { get; }

        [DisplayName("Geometry")]
        [Description("Flat or solid geometry")]
        [JsonProperty("geometry", Required = Required.Always)]
        /// <summary>
        /// Geometry of the terrain.
        /// </summary>
        public FaceGroup Geometry { get; }

        [JsonIgnore]
        /// <summary>
        /// Matrix 2D of the terrain.
        /// </summary>
        public Matrix2d IDmatrix { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Collection of string based on Pixel and ID.
        /// </summary>
        public List<string> TerrainIDrows { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Location of the terrain in the grid.
        /// </summary>
        public List<Pixel> Pixels { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Material of the terrain.
        /// </summary>
        public override Material Material
        {
            get => throw new NotImplementedException();
            protected set => throw new NotImplementedException();
        }
        /// <summary>
        /// Create a new terrain.
        /// </summary>
        /// <param name="geometry">Geometry of the terrain.</param>
        /// <param name="id">Numerical ID.</param>
        /// <param name="name">Name of the terrain.</param>
        public Terrain(FaceGroup geometry,
            int id, string name = null)
        {
            ID = id;
            Geometry = geometry;
            Name = name ?? "TerrainGroup";
            TerrainIDrows = new List<string>();
            Pixels = new List<Pixel>();
        }

        public void SetMatrix(Grid grid)
        {
            Matrix2d matrix = new Matrix2d(grid.Size.NumX, grid.Size.NumY, "0");

            List<Ray> rays = EnvimetUtility.GetRayFromFacegroupBbox(grid, Geometry);

            var intersection = EnvimetUtility.Raycasting2D(rays, Geometry, true, false);
            // 2D Part
            SetMatrix(intersection, grid, matrix, "");
            // 3D Part
            CalculatePixels(grid, intersection);

            IDmatrix = matrix;
        }

        private void CalculatePixels(Grid grid, IEnumerable<Vector> intersection)
        {
            var voxels = new List<Vector>();
            foreach (var pt in intersection)
            {
                var h = (pt.z < 0) ? 0 : pt.z;
                var zList = Util.FilterByMinMax(grid.Zaxis, h, 0);

                foreach(var v in zList)
                {
                    voxels.Add(new Vector(pt.x, pt.y, Convert.ToSingle(v)));
                }
            }
            Pixels = voxels
                .Select(_ => _.ToPixel(grid))
                .ToList();

            TerrainIDrows = GetTerrainRows()
                .ToList();
        }

        private IEnumerable<string> GetTerrainRows()
        {
            foreach (var px in Pixels)
            {
                yield return String.Format("{0},{1},{2},{3}", 
                    px.I, px.J, px.K, "1.00000");
            }
        }
        /// <summary>
        /// String representation of the terrain.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Terrain::{0}::{1}", Name, ID);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Terrain Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Terrain>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Terrain other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.ID == this.ID
                && other.Name == this.Name
                && other.Geometry == this.Geometry)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var terrainObj = obj as Terrain;
            if (terrainObj == null)
                return false;
            else
                return Equals(terrainObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ID.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Geometry.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Terrain terrain1, Terrain terrain2)
        {
            if (((object)terrain1) == null || ((object)terrain2) == null)
                return Object.Equals(terrain1, terrain2);

            return terrain1.Equals(terrain2);
        }

        public static bool operator !=(Terrain terrain1, Terrain terrain2)
        {
            return !(terrain1 == terrain2);
        }
    }
}
