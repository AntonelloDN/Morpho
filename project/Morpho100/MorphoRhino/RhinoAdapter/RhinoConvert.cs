using System.Collections.Generic;
using Rhino.Geometry;
using MorphoGeometry;

namespace MorphoRhino.RhinoAdapter
{
    public class RhinoConvert
    {
        public static FaceGroup FromRhMeshToFacegroup(Mesh rhMesh)
        {
            var rhFaces = rhMesh.Faces;
            var vertices = rhMesh.Vertices;

            List<Face> faces = new List<Face>();

            foreach (MeshFace rhFace in rhFaces)
            {
                Face face;

                Point3d pt1 = vertices[rhFace.A];
                Point3d pt2 = vertices[rhFace.B];
                Point3d pt3 = vertices[rhFace.C];

                if (rhFace.IsQuad)
                {
                    Point3d pt4 = vertices[rhFace.D];
                    face = new Face(new Vector[4] {
                      new Vector((float) pt1.X, (float) pt1.Y, (float) pt1.Z),
                      new Vector((float) pt2.X, (float) pt2.Y, (float) pt2.Z),
                      new Vector((float) pt3.X, (float) pt3.Y, (float) pt3.Z),
                      new Vector((float) pt4.X, (float) pt4.Y, (float) pt4.Z)
                      });
                }
                else
                {
                    face = new Face(new Vector[3] {
                      new Vector((float) pt1.X, (float) pt1.Y, (float) pt1.Z),
                      new Vector((float) pt2.X, (float) pt2.Y, (float) pt2.Z),
                      new Vector((float) pt3.X, (float) pt3.Y, (float) pt3.Z)
                      });
                }
                faces.Add(face);
            }

            return new FaceGroup(faces);
        }

        public static Vector FromRhPointToVector(Point3d point)
        {
            return new Vector((float)point.X, (float)point.Y, (float)point.Z);
        }

        public static Point3d FromVectorToRhPoint(Vector vector)
        {
            return new Point3d(vector.x, vector.y, vector.z);
        }

    }
}
