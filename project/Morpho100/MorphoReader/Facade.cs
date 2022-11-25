using System.Collections.Generic;
using System.Linq;
using MorphoGeometry;
using Morpho25.Utility;

namespace MorphoReader
{
    public class Facade
    {
        const int NULL_VALUE = -999;

        public Face Face { get; }
        public Pixel Pixel { get; }
        public double ValueX { get; set; }
        public double ValueY { get; set; }
        public double ValueZ { get; set; }

        public Facade(Face face)
        {
            Face = face;
            ValueX = NULL_VALUE;
            ValueY = NULL_VALUE;
            ValueZ = NULL_VALUE;
        }

        public Facade(Pixel pixel, Face face) 
            : this(face)
        {
            Pixel = pixel;
        }

        public bool IsXdirection()
        {
            return ValueX != NULL_VALUE;
        }

        public bool IsYdirection()
        {
            return ValueY != NULL_VALUE;
        }

        public bool IsZdirection()
        {
            return ValueZ != NULL_VALUE;
        }

        public static List<double> GetValueXFromFacades(List<Facade> facades)
        {
            return facades.Select(_ => _.ValueX)
                   .ToList();
        }

        public static List<double> GetValueYFromFacades(List<Facade> facades)
        {
            return facades.Select(_ => _.ValueY)
                   .ToList();
        }

        public static List<double> GetValueZFromFacades(List<Facade> facades)
        {
            return facades.Select(_ => _.ValueZ)
                   .ToList();
        }

        public static List<Face> GetFacesFromFacades(List<Facade> facades)
        {
            return facades.Select(_ => _.Face)
                          .ToList();
        }


        #region Query
        public static List<Facade> GetFacadesByDirection(List<Facade> facades, Direction direction = Direction.Z)
        {

            if (direction == Direction.X)
            {
                return facades.Where(_ => _.IsXdirection())
                    .ToList();
            }
            else if (direction == Direction.Y)
            {
                return facades.Where(_ => _.IsYdirection())
                    .ToList();
            }
            else
            {
                return facades.Where(_ => _.IsZdirection())
                    .ToList();
            }
        }

        public static List<Facade> GetSliceByPixelCoordinate(List<Facade> facades, int index, Direction direction = Direction.Z)
        {

            if (direction == Direction.X)
            {
                return facades.Where(_ => _.Pixel.I == index)
                    .ToList();
            }
            else if (direction == Direction.Y)
            {
                return facades.Where(_ => _.Pixel.J == index)
                    .ToList();
            }
            else
            {
                return facades.Where(_ => _.Pixel.K == index)
                    .ToList();
            }
        }

        public static List<Facade> GetFacadesFilterByZmask(List<Facade> facades, List<int> values, int gridHeight)
        {
            List<Facade> result = new List<Facade>();

            values = DuplicateIndex(values, gridHeight);

            for (int i = 0; i < facades.Count; i++)
            {
                if (facades[i].Pixel.K == values[i])
                {
                    result.Add(facades[i]);
                }
            }

            result = result.OrderBy(f => f.Pixel.I)
                .OrderBy(f => f.Pixel.J)
                .ToList();

            return result;
        }

        private static List<int> DuplicateIndex(List<int> values, int gridHeight)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < gridHeight; i++)
            {
                result.AddRange(values);
            }

            return result;
        }

        public static List<Facade> GetFacadesByThreshold(List<Facade> facades, double min, double max, Direction direction = Direction.Z)
        {

            if (direction == Direction.X)
            {
                return facades.Where(_ => _.ValueX >= min && _.ValueX <= max)
                    .ToList();
            }
            else if (direction == Direction.Y)
            {
                return facades.Where(_ => _.ValueY >= min && _.ValueY <= max)
                    .ToList();
            }
            else
            {
                return facades.Where(_ => _.ValueZ >= min && _.ValueZ <= max)
                    .ToList();
            }
        }

        #endregion
    }
}
