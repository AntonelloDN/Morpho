using System.Collections.Generic;
using Rhino.Geometry;
using MorphoGeometry;

namespace MorphoRhino.RhinoAdapter
{
    /// <summary>
    /// Rhino convert class.
    /// </summary>
    public class RhinoConvert
    {
        /// <summary>
        /// Base tolerance.
        /// </summary>
        public const double TOLERANCE = 0.01;

        /// <summary>
        /// From rhino mesh to facegroup.
        /// </summary>
        /// <param name="rhMesh">Rhino mesh.</param>
        /// <returns>New facegroup.</returns>
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

        /// <summary>
        /// From face to rhino brep.
        /// </summary>
        /// <param name="face">Face to convert.</param>
        /// <returns>Rhino brep.</returns>
        public static Brep FromFaceToBrep(Face face)
        {
            Point3d pt1 = FromVectorToRhPoint(face.A);
            Point3d pt2 = FromVectorToRhPoint(face.B);
            Point3d pt3 = FromVectorToRhPoint(face.C);

            if (face.Vertices.Length > 3)
            { 
                Point3d pt4 = FromVectorToRhPoint(face.D);
                return Brep.CreateFromCornerPoints(pt1, pt2, pt3, pt4, TOLERANCE);
            }

            return Brep.CreateFromCornerPoints(pt1, pt2, pt3, TOLERANCE);

        }

        /// <summary>
        /// From faces to rhino mesh.
        /// </summary>
        /// <param name="faces">Faces to convert.</param>
        /// <returns>Rhino mesh.</returns>
        public static Mesh FromFacesToMesh(List<Face> faces)
        {
            Mesh mesh = new Mesh();

            MeshingParameters settings = new MeshingParameters();
            settings.SimplePlanes = true;

            foreach (Face face in faces)
            {
                Brep brep = FromFaceToBrep(face);
                mesh.Append(Mesh.CreateFromBrep(brep, settings)[0]);
            }

            return mesh;
        }

        /// <summary>
        /// From rhino point to vector.
        /// </summary>
        /// <param name="point">Rhino point to convert.</param>
        /// <returns>New vector.</returns>
        public static Vector FromRhPointToVector(Point3d point)
        {
            return new Vector((float)point.X, (float)point.Y, (float)point.Z);
        }

        /// <summary>
        /// From vector to rhino point.
        /// </summary>
        /// <param name="vector">Vector to convert.</param>
        /// <returns>Rhino point.</returns>
        public static Point3d FromVectorToRhPoint(Vector vector)
        {
            return new Point3d(vector.x, vector.y, vector.z);
        }

    }
}
