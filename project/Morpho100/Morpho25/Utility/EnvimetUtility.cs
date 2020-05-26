using Morpho25.Geometry;
using System.Collections.Generic;
using Morpho25.Transformation;
using System;


namespace Morpho25.Utility
{
    public class EnvimetUtility
    {
        public static IEnumerable<g3.Vector3d> Raycasting(g3.DMesh3 gMesh, Grid grid, bool top = true)
        {
            var bounds = G3.GetBoundaryBox(gMesh);

            g3.Vector3d min = bounds.Min;
            g3.Vector3d max = bounds.Max;

            var rayXcomponent = Util.FilterByMinMax(grid.Xaxis, max.x, min.x);
            var rayYcomponent = Util.FilterByMinMax(grid.Yaxis, max.y, min.y);

            double position = top ? grid.Zaxis[grid.Zaxis.Length - 1] : -grid.Zaxis[grid.Zaxis.Length - 1];

            List<g3.Vector3d> intersection = new List<g3.Vector3d>();
            foreach (double y in rayYcomponent)
                foreach (double x in rayXcomponent)
                {
                    var origin = G3.ConvertToOrigin(x, y, position);
                    var vector = G3.RayIntersection(gMesh, origin, top);
                    if (vector.z != G3.INTERSECTION_FAILED)
                        intersection.Add(vector);
                }

            return intersection;
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
    }
}
