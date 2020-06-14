using Morpho25.Geometry;
using System.Collections.Generic;
using System;
using MorphoGeometry;

namespace Morpho25.Utility
{
    public class EnvimetUtility
    {
        public static IEnumerable<Vector> Raycasting(List<Ray> rays, FaceGroup facegroup, bool reverse = false, bool project = false)
        {
            return Intersection.RaysFaceGroupIntersect(rays, facegroup, reverse, project);
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
    }
}
