using System;
using System.Collections.Generic;
using System.Linq;
using Morpho25.Utility;
using Newtonsoft.Json;

namespace Morpho25.Geometry
{
    /// <summary>
    /// Grid class.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Create a new Grid.
        /// </summary>
        /// <param name="size">Grid size object.</param>
        /// <param name="nestingGrids">Optional nestring grids.</param>
        public Grid(Size size,
            NestingGrids nestingGrids = null)
        {
            Size = size;
            Telescope = 0.0;
            StartTelescopeHeight = 0.0;
            CombineGridType = false;

            SetSequenceAndExtension();
            SetXaxis();
            SetYaxis();

            if (nestingGrids == null)
                NestingGrids = new NestingGrids();
            else
                NestingGrids = nestingGrids;
        }

        [JsonConstructor]
        /// <summary>
        /// Create a new Grid.
        /// </summary>
        /// <param name="size">Grid size object.</param>
        /// <param name="telescope">Vertical increment to use for a telescopic grid.
        /// If 0.0 it will use equidistant grid.</param>
        /// <param name="startTelescopeHeight">Start increment z dimension at.</param>
        /// <param name="combineGridType">True to split the first cell.</param>
        /// <param name="nestingGrids">Optional nesting grids.</param>
        public Grid(Size size,
            double telescope,
            double startTelescopeHeight,
            bool combineGridType,
            NestingGrids nestingGrids = null)
        {
            Size = size;
            Telescope = telescope;
            StartTelescopeHeight = startTelescopeHeight;
            CombineGridType = combineGridType;

            SetSequenceAndExtension();
            SetXaxis();
            SetYaxis();

            if (nestingGrids == null)
                NestingGrids = new NestingGrids();
            else
                NestingGrids = nestingGrids;
        }

        private double _telescope;
        private double _startTelescopeHeight;

        [JsonProperty("size", Required = Required.Always)]
        /// <summary>
        /// Grid size.
        /// </summary>
        public Size Size { get; }

        [JsonProperty("nestingGrids")]
        /// <summary>
        /// Nesting grids.
        /// </summary>
        public NestingGrids NestingGrids { get; set; }

        [JsonProperty("telescope")]
        /// <summary>
        /// Telescope value.
        /// </summary>
        public double Telescope
        {
            get { return _telescope; }
            set
            {
                if (value < 0.0 || value > 18.0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be between 0 and 24.");

                _telescope = value;
            }
        }

        [JsonProperty("startTelescopeHeight")]
        /// <summary>
        /// Start telescopic grid at.
        /// </summary>
        public double StartTelescopeHeight
        {
            get { return _startTelescopeHeight; }
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be positive.");

                _startTelescopeHeight = value;
            }
        }

        /// <summary>
        /// Convert to List of double.
        /// </summary>
        /// <returns>List of double.</returns>
        public List<double[]> ToList()
        {
            var list = new List<double[]>();
            foreach (var x in Xaxis)
                foreach (var y in Yaxis)
                    foreach (var z in Zaxis)
                    {
                        list.Add(new[] { x, y, z });
                    }

            return list;
        }

        [JsonProperty("combineGridType")]
        /// <summary>
        /// Is telescopic grid + first cell splitted?
        /// </summary>
        public bool CombineGridType { get; }

        [JsonIgnore]
        /// <summary>
        /// X axis of the grid.
        /// </summary>
        public double[] Xaxis { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Y axis of the grid.
        /// </summary>
        public double[] Yaxis { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Z axis of the grid.
        /// </summary>
        public double[] Zaxis { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Height of cells.
        /// </summary>
        public double[] SequenceZ { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Is the grid splitted?
        /// </summary>
        public bool IsSplitted { get; private set; }

        /// <summary>
        /// String representation of the grid.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Grid::Size {0},{1},{2}", Size.NumX, Size.NumY, Size.NumZ);
        }

        private void SetXaxis()
        {
            double[] sequence = new double[Size.NumX];
            for (int i = 0; i < Size.NumX; i++)
                sequence[i] = (Size.DimX * i) + Size.MinX;
            Xaxis = sequence;
        }

        private void SetYaxis()
        {
            double[] sequence = new double[Size.NumY];
            for (int i = 0; i < Size.NumY; i++)
                sequence[i] = (Size.DimY * i) + Size.MinY;
            Yaxis = sequence;
        }

        private void SetSequenceAndExtension()
        {
            if (CombineGridType && Telescope > 0.0)
            {
                SequenceZ = GetCombinedSequence(Telescope, StartTelescopeHeight);
                IsSplitted = true;
            }
            else if (CombineGridType == false && Telescope > 0.0)
            {
                SequenceZ = GetTelescopeSequence(Telescope, StartTelescopeHeight);
                IsSplitted = false;
            }
            else
            {
                SequenceZ = GetEquidistantSequence();
                IsSplitted = true;
            }

            var accumulated = Util.Accumulate(SequenceZ)
                .ToArray();
            Zaxis = accumulated.Zip(SequenceZ, (a, b) => a - (b / 2))
                .ToArray();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Grid Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Grid>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Grid other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.Size == this.Size
                && other.Telescope == other.Telescope
                && other.StartTelescopeHeight == other.StartTelescopeHeight
                && other.CombineGridType == other.CombineGridType
                && other.NestingGrids == other.NestingGrids)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var gridObj = obj as Grid;
            if (gridObj == null)
                return false;
            else
                return Equals(gridObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Size.GetHashCode();
                hash = hash * 23 + Telescope.GetHashCode();
                hash = hash * 23 + StartTelescopeHeight.GetHashCode();
                hash = hash * 23 + CombineGridType.GetHashCode();
                hash = hash * 23 + NestingGrids.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Grid grid1, Grid grid2)
        {
            if (((object)grid1) == null || ((object)grid2) == null)
                return Object.Equals(grid1, grid2);

            return grid1.Equals(grid2);
        }

        public static bool operator !=(Grid grid1, Grid grid2)
        {
            return !(grid1 == grid2);
        }

        #region Sequence
        private double[] GetEquidistantSequence()
        {
            var baseCell = Size.DimZ / 5;
            var cell = Size.DimZ;

            double[] sequence = new double[Size.NumZ];

            for (int k = 0; k < sequence.Length; k++)
            {
                if (k < 5)
                    sequence[k] = baseCell;
                else
                    sequence[k] = cell;
            }

            return sequence;
        }

        private double[] GetTelescopeSequence(double telescope, double start)
        {
            var cell = Size.DimZ;

            double[] sequence = new double[Size.NumZ];

            double val = cell;

            for (int k = 0; k < sequence.Length; k++)
            {
                if (val * k < start)
                {
                    sequence[k] = cell;
                }
                else
                {
                    sequence[k] = val + (val * telescope / 100);
                    val = sequence[k];
                }
            }

            return sequence;
        }

        private double[] GetCombinedSequence(double telescope, double start)
        {
            var cell = Size.DimZ;
            var baseCell = Size.DimZ / 5;
            double val = cell;

            double[] firstSequence = new double[5];
            double[] sequence = new double[Size.NumZ - 1];

            for (int k = 0; k < 5; k++)
                firstSequence[k] = baseCell;

            for (int k = 0; k < sequence.Length; k++)
            {
                if (val * (k + 1) < start)
                {
                    sequence[k] = cell;
                }
                else
                {
                    sequence[k] = val + (val * telescope / 100);
                    val = sequence[k];
                }
            }

            double[] completeSequence = new double[sequence.Length + firstSequence.Length];

            firstSequence.CopyTo(completeSequence, 0);
            sequence.CopyTo(completeSequence, firstSequence.Length);
            Array.Resize(ref completeSequence, sequence.Length + 1);

            return completeSequence;
        }
        #endregion
    }
}
