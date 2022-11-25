using System;
using System.Collections.Generic;
using System.Linq;
using Morpho25.Utility;
using MorphoGeometry;

namespace Morpho25.Geometry
{
    public struct CellDimension
    {
        public CellDimension(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }
    }

    public struct Size
    {
        public Size(Vector origin, CellDimension dimension, int numX, int numY, int numZ)
        {
            Origin = origin;
            NumX = numX;
            NumY = numY;
            NumZ = numZ;

            DimX = dimension.X;
            DimY = dimension.Y;
            DimZ = dimension.Z;

            MinX = Origin.x;
            MinY = Origin.y;
            MaxX = Origin.x + (NumX * DimX);
            MaxY = Origin.y + (NumY * DimY);
        }

        public int NumX { get; }
        public int NumY { get; }
        public int NumZ { get; }
        public double MinX { get; }
        public double MinY { get; }
        public double MaxX { get; }
        public double MaxY { get; }
        public double DimX { get; }
        public double DimY { get; }
        public double DimZ { get; }
        public Vector Origin { get; }

        public override string ToString()
        {
            return String.Format("Size::{0},{1},{2}::{3},{4},{5}", NumX, NumY, NumZ, DimX, DimY, DimZ);
        }
    }

    public class Grid
    {
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

        public Size Size { get; }

        public NestingGrids NestingGrids { get; set; }

        public double Telescope {
            get { return _telescope; }
            set
            {
                if (value < 0.0 || value > 18.0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be between 0 and 24.");

                _telescope = value;
            }
        }

        public double StartTelescopeHeight {
            get { return _startTelescopeHeight; }
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be positive.");

                _startTelescopeHeight = value;
            }
        }

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

        public bool CombineGridType { get; }
        public double[] Xaxis { get; private set; }
        public double[] Yaxis { get; private set; }
        public double[] Zaxis { get; private set; }
        public double[] SequenceZ { get; private set; }
        public bool IsSplitted { get; private set; }

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

    public class NestingGrids
    {
        public string FirstMaterial { get; private set; }
        public string SecondMaterial { get; private set; }
        public uint NumberOfCells { get; private set; }

        public NestingGrids()
        {
            NumberOfCells = 0;
            FirstMaterial = Material.DEFAULT_SOIL;
            SecondMaterial = Material.DEFAULT_SOIL;
        }

        public NestingGrids(uint numberOfCells, 
            string firstMaterial,
            string secondMaterial)
        {
            NumberOfCells = numberOfCells;
            FirstMaterial = firstMaterial;
            SecondMaterial = secondMaterial;
        }
    }
}
