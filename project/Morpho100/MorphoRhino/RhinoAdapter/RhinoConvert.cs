using System.Collections.Generic;
using System.Linq;
using Morpho25.Transformation;
using g3;

namespace MorphoRhino.RhinoAdapter
{
    public class RhinoConvert
    {
        public static g3.Vector3d ConvertToOrigin(Rhino.Geometry.Point3d point)
        {
            return new g3.Vector3d(point.X, point.Y, point.Z);
        }

        public static g3.DMesh3 ConvertToMesh(Rhino.Geometry.Mesh mesh)
        {
            int[] triangles;
            List<g3.Vector3f> vertices = new List<g3.Vector3f>();
            var normals = DecomposeRhinoMesh(mesh, out triangles, out vertices);
            var g3mesh = G3.CreateMesh(vertices, triangles, normals);

            return g3mesh;
        }

        public static Rhino.Geometry.Point3d ConvertToRhinoPoint(g3.Vector3d point)
        {
            return new Rhino.Geometry.Point3d(point.x, point.y, point.z);
        }

        public static Rhino.Geometry.Mesh ConvertToRhinoMesh(g3.DMesh3 mesh)
        {
            Rhino.Geometry.Mesh rhMesh = new Rhino.Geometry.Mesh();

            var vertex = mesh.Vertices();
            var triangle = mesh.Triangles();

            var faces = triangle.ToArray().Select(v => new Rhino.Geometry.MeshFace(v.a, v.b, v.c));
            var vertices = vertex.ToArray().Select(v => new Rhino.Geometry.Point3d(v.x, v.y, v.z));

            rhMesh.Faces.AddFaces(faces);
            rhMesh.Vertices.AddVertices(vertices);
            rhMesh.Normals.ComputeNormals();
            rhMesh.Compact();

            return rhMesh;
        }

        public static Rhino.Geometry.Mesh CreateMeshFromBreps(IEnumerable<Rhino.Geometry.Brep> breps, double gridSize)
        {
            Rhino.Geometry.Mesh rhMesh = new Rhino.Geometry.Mesh();

            int aspectRatio = 1;
            Rhino.Geometry.MeshingParameters parameters = new Rhino.Geometry.MeshingParameters()
            {
                MaximumEdgeLength = gridSize,
                MinimumEdgeLength = gridSize,
                GridAspectRatio = aspectRatio
            };

            foreach (Rhino.Geometry.Brep brep in breps)
            {
                Rhino.Geometry.Mesh[] meshPart = Rhino.Geometry.Mesh.CreateFromBrep(brep, parameters);
                foreach (Rhino.Geometry.Mesh mesh in meshPart)
                    rhMesh.Append(mesh);
            }

            return rhMesh;
        }

        public static Rhino.Geometry.Mesh CreateMeshFromMeshes(IEnumerable<Rhino.Geometry.Mesh> meshes)
        {
            Rhino.Geometry.Mesh rhMesh = new Rhino.Geometry.Mesh();

            foreach (Rhino.Geometry.Mesh mesh in meshes)
                rhMesh.Append(mesh);

            return rhMesh;
        }

        private static List<g3.Vector3f> DecomposeRhinoMesh(Rhino.Geometry.Mesh mesh, out int[] triangles, out List<g3.Vector3f> vertices)
        {
            triangles = mesh.Faces.ToIntArray(true);
            List<g3.Vector3f> vectors = new List<g3.Vector3f>();
            vertices = new List<g3.Vector3f>();

            var verticiMesh = mesh.Vertices;
            var vettoriMesh = mesh.Normals;

            for (int i = 0; i < verticiMesh.Count; i++)
            {
                vertices.Add(new g3.Vector3f((float)verticiMesh[i].X, (float)verticiMesh[i].Y, (float)verticiMesh[i].Z));
                vectors.Add(new g3.Vector3f((float)vettoriMesh[i].X, (float)vettoriMesh[i].Y, (float)vettoriMesh[i].Z));
            }

            return vectors;
        }
    }
}
