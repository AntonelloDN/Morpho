using System;
using System.Linq;
using System.Collections.Generic;
using MorphoGeometry;
using Morpho25.Geometry;

namespace Morpho25.Utility
{
    public class EnvimetUtility
    {
        public static IEnumerable<Vector> Raycasting2D(List<Ray> rays, 
            FaceGroup facegroup, bool reverse = false, 
            bool project = false)
        {
            return Intersection.RaysFaceGroupIntersect(rays, facegroup,
                Intersection.RayFaceIntersect,
                reverse, project);
        }

        public static IEnumerable<Vector> Raycasting3D(List<Ray> rays,
            FaceGroup facegroup, bool reverse = false,
            bool project = false)
        {
            return Intersection.RaysFaceGroupIntersect(rays, facegroup,
                Intersection.RayFaceIntersectFrontBack,
                reverse, project);
        }

        public static IEnumerable<Vector> GetCentroids(Grid grid, 
            IEnumerable<Vector> intersections)
        {
            var groups = intersections
                .GroupBy(_ => new { x = _.x, y = _.y, } )
                .ToList();

            var centroids = new List<Vector>();
            foreach (var group in groups)
            {
                var chunks = group.ToList().ChunkBy(2);
                foreach (var pts in chunks)
                {
                    var heights = pts.Select(_ => _.z);
                    var min = heights.Min();
                    var max = heights.Max();

                    var zCoords = Util.FilterByMinMax(grid.Zaxis, max, min);
                    var voxels = zCoords.Select(_ => new Vector(pts[0].x, pts[0].y, Convert.ToSingle(_)));

                    centroids.AddRange(voxels);
                }
            }

            return centroids;
        }

        public static List<Ray> GetRayFromFacegroup(Grid grid, FaceGroup facegroup)
        {
            MorphoGeometry.BoundaryBox box = new MorphoGeometry.BoundaryBox(facegroup);

            Vector minPt = box.MinPoint;
            Vector maxPt = box.MaxPoint;

            var rayXcomponent = Util.FilterByMinMax(grid.Xaxis, maxPt.x, minPt.x);
            var rayYcomponent = Util.FilterByMinMax(grid.Yaxis, maxPt.y, minPt.y);

            List<Ray> rays = new List<Ray>();
            foreach (double y in rayYcomponent)
                foreach (double x in rayXcomponent)
                {
                    rays.Add(new Ray(new Vector((float)x, (float)y, 0), new Vector(0, 0, 1)));
                }

            return rays;
        }

        public static string GetASCIImatrix(Matrix2d matrix)
        {
            string text = string.Empty;
            List<string> rows = new List<string>();

            for (int j = matrix.GetLengthY() - 1; j >= 0; j--)
            {
                string[] line = new string[matrix.GetLengthX()];
                for (int i = 0; i < matrix.GetLengthX(); i++)
                {
                    line[i] = matrix[i, j];
                }
                rows.Add(String.Join(",", line));
            }
            text = String.Join("\n", rows) + "\n";

            return text;
        }

        public static double GetAtmosphereSpecificHumidity(List<double> temperature, List<double> relativeHumidity)
        {
            const double AIR_PRESSURE = 1013.25;

            List<double> kelvinTemperature = temperature.Select(_ => _ + Util.TO_KELVIN)
                                             .ToList();

            double meanTemperature = kelvinTemperature.Average();
            double meanRelativeHumidity = relativeHumidity.Average();

            double eSaturation = 0.6112 * Math.Exp(17.67 * (meanTemperature - 273.15) / (meanTemperature - 29.66)) * 10;
            double qSaturation = (0.6112 * (eSaturation / AIR_PRESSURE)) * 1000;
            double specificHumidity = qSaturation * (meanRelativeHumidity / 100);

            return specificHumidity;
        }
    }
}
