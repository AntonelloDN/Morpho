using System.Collections.Generic;
using System.Linq;
using MorphoGeometry;
using Morpho25.Utility;

namespace MorphoReader
{
    /// <summary>
    /// Facade class.
    /// </summary>
    public class Voxel
    {
        const int NULL_VALUE = -999;

        /// <summary>
        /// Face.
        /// </summary>
        public Face Face { get; }
        /// <summary>
        /// Pixel.
        /// </summary>
        public Pixel Pixel { get; }
        /// <summary>
        /// Value of X.
        /// </summary>
        public double ValueX { get; set; }
        /// <summary>
        /// Value of Y.
        /// </summary>
        public double ValueY { get; set; }
        /// <summary>
        /// Value of Z.
        /// </summary>
        public double ValueZ { get; set; }
        /// <summary>
        /// Create a new facade.
        /// </summary>
        /// <param name="face"></param>
        public Voxel(Face face)
        {
            Face = face;
            ValueX = NULL_VALUE;
            ValueY = NULL_VALUE;
            ValueZ = NULL_VALUE;
        }
        /// <summary>
        /// Create a new facade.
        /// </summary>
        /// <param name="pixel">Pixel.</param>
        /// <param name="face">Face.</param>
        public Voxel(Pixel pixel, Face face) 
            : this(face)
        {
            Pixel = pixel;
        }
        /// <summary>
        /// Is direction X?
        /// </summary>
        /// <returns>Yes or no.</returns>
        public bool IsXdirection()
        {
            return ValueX != NULL_VALUE;
        }
        /// <summary>
        /// Is direction Y?
        /// </summary>
        /// <returns>Yes or no.</returns>
        public bool IsYdirection()
        {
            return ValueY != NULL_VALUE;
        }
        /// <summary>
        /// Is direction Z?
        /// </summary>
        /// <returns>Yes or no.</returns>
        public bool IsZdirection()
        {
            return ValueZ != NULL_VALUE;
        }
        /// <summary>
        /// Get values in X.
        /// </summary>
        /// <param name="voxels">Voxels.</param>
        /// <returns>Collection of values.</returns>
        public static List<double> GetValueXFromVoxels(List<Voxel> voxels)
        {
            return voxels.Select(_ => _.ValueX)
                   .ToList();
        }
        /// <summary>
        /// Get values in Y.
        /// </summary>
        /// <param name="voxels">Voxels.</param>
        /// <returns>Collection of values.</returns>
        public static List<double> GetValueYFromVoxels(List<Voxel> voxels)
        {
            return voxels.Select(_ => _.ValueY)
                   .ToList();
        }
        /// <summary>
        /// Get values in Z.
        /// </summary>
        /// <param name="voxels">Voxels.</param>
        /// <returns>Collection of values.</returns>
        public static List<double> GetValueZFromVoxels(List<Voxel> voxels)
        {
            return voxels.Select(_ => _.ValueZ)
                   .ToList();
        }
        /// <summary>
        /// Get faces from facades.
        /// </summary>
        /// <param name="voxels">Voxels.</param>
        /// <returns>Collection of faces.</returns>
        public static List<Face> GetFacesFromVoxels(List<Voxel> voxels)
        {
            return voxels.Select(_ => _.Face)
                          .ToList();
        }


        #region Query
        /// <summary>
        /// Get facades by direction.
        /// </summary>
        /// <param name="voxels">Voxels.</param>
        /// <param name="direction">Direction.</param>
        /// <returns>Collection of facades.</returns>
        public static List<Voxel> GetVoxelsByDirection(List<Voxel> voxels, 
            Direction direction = Direction.Z)
        {

            if (direction == Direction.X)
            {
                return voxels.Where(_ => _.IsXdirection())
                    .ToList();
            }
            else if (direction == Direction.Y)
            {
                return voxels.Where(_ => _.IsYdirection())
                    .ToList();
            }
            else
            {
                return voxels.Where(_ => _.IsZdirection())
                    .ToList();
            }
        }

        /// <summary>
        /// Get Slice of facades by pixel index and direction.
        /// </summary>
        /// <param name="voxels">Voxels.</param>
        /// <param name="index">Index.</param>
        /// <param name="direction">Direction.</param>
        /// <returns>Collection of facades.</returns>
        public static List<Voxel> GetSliceByPixelCoordinate(List<Voxel> voxels, 
            int index, Direction direction = Direction.Z)
        {

            if (direction == Direction.X)
            {
                return voxels.Where(_ => _.Pixel.I == index)
                    .ToList();
            }
            else if (direction == Direction.Y)
            {
                return voxels.Where(_ => _.Pixel.J == index)
                    .ToList();
            }
            else
            {
                return voxels.Where(_ => _.Pixel.K == index)
                    .ToList();
            }
        }

        /// <summary>
        /// Get facades by mask of values.
        /// </summary>
        /// <param name="voxels">Voxels.</param>
        /// <param name="values">Collection of index to use.</param>
        /// <param name="gridHeight">Height of the grid.</param>
        /// <returns>Collection of facades.</returns>
        public static List<Voxel> GetVoxelsFilterByZmask(List<Voxel> voxels, 
            List<int> values, int gridHeight)
        {
            List<Voxel> result = new List<Voxel>();

            values = DuplicateIndex(values, gridHeight);

            for (int i = 0; i < voxels.Count; i++)
            {
                if (voxels[i].Pixel.K == values[i])
                {
                    result.Add(voxels[i]);
                }
            }

            result = result.OrderBy(f => f.Pixel.I)
                .OrderBy(f => f.Pixel.J)
                .ToList();

            return result;
        }

        private static List<int> DuplicateIndex(List<int> values, 
            int gridHeight)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < gridHeight; i++)
            {
                result.AddRange(values);
            }

            return result;
        }

        /// <summary>
        /// Get voxels by threshold.
        /// </summary>
        /// <param name="voxels">Voxels.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <param name="direction">Direction.</param>
        /// <returns>Collection of facades.</returns>
        public static List<Voxel> GetVoxelsByThreshold(List<Voxel> voxels, 
            double min, double max, 
            Direction direction = Direction.Z)
        {

            if (direction == Direction.X)
            {
                return voxels.Where(_ => _.ValueX >= min && _.ValueX <= max)
                    .ToList();
            }
            else if (direction == Direction.Y)
            {
                return voxels.Where(_ => _.ValueY >= min && _.ValueY <= max)
                    .ToList();
            }
            else
            {
                return voxels.Where(_ => _.ValueZ >= min && _.ValueZ <= max)
                    .ToList();
            }
        }

        #endregion
    }
}
