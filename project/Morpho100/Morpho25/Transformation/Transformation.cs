using g3;
using System.Collections.Generic;


namespace Morpho25.Transformation
{
    public class G3
    {
        public const int INTERSECTION_FAILED = 999999;

        public static g3.Vector3d RayIntersection(DMesh3 gMesh, g3.Vector3d origin, bool top = true)
        {
            g3.DMeshAABBTree3 spatial = new g3.DMeshAABBTree3(gMesh);
            spatial.Build();

            var vector = top ? -g3.Vector3f.AxisZ : g3.Vector3f.AxisZ;

            g3.Ray3d ray = new g3.Ray3d(origin, vector);
            int hit_tid = spatial.FindNearestHitTriangle(ray);

            g3.Vector3d hit_dist = new g3.Vector3d(0, 0, INTERSECTION_FAILED);
            if (hit_tid != g3.DMesh3.InvalidID)
            {
                g3.IntrRay3Triangle3 intr = g3.MeshQueries.TriangleIntersection(gMesh, hit_tid, ray);
                hit_dist = ray.PointAt(intr.RayParameter);
            }
            return hit_dist;
        }

        public static DMesh3 CreateMesh(List<g3.Vector3f> vertices, int[] triangles, List<g3.Vector3f> normals)
        {
            return DMesh3Builder.Build(vertices, triangles, normals);
        }

        public static g3.Vector3d ConvertToOrigin(double x, double y, double z)
        {
            return new g3.Vector3d(x, y, z);
        }

        public static g3.AxisAlignedBox3d GetBoundaryBox(DMesh3 gMesh)
        {
            return gMesh.GetBounds();
        }
    }

}
