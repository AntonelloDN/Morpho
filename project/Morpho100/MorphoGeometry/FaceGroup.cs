using System;
using System.Collections.Generic;
using System.Linq;

namespace MorphoGeometry
{
    /// <summary>
    /// Facegroup class.
    /// </summary>
    public class FaceGroup
    {
        /// <summary>
        /// Faces of the facegroup.
        /// </summary>
        public List<Face> Faces { get; }

        /// <summary>
        /// Create a new facegroup.
        /// </summary>
        /// <param name="faces">Faces.</param>
        public FaceGroup(List<Face> faces)
        {
            Faces = TriangulateFaces(faces);
        }

        /// <summary>
        /// Convert facegroup to array of float.
        /// </summary>
        /// <returns>Jagged array.</returns>
        public float[][][] ToArray()
        {
            return Faces
                .Select(face => {
                    return face.Vertices
                        .Select(_ => _.ToArray())
                        .ToArray();
                }).ToArray();
        }

        private static List<Face> TriangulateFaces(List<Face> faces)
        {
            List<Face> outFaces = new List<Face>();
            foreach (Face face in faces)
            {
                if (face.IsQuad())
                {
                    Face[] tFaces = Face.Triangulate(face);
                    outFaces.Add(tFaces[0]);
                    outFaces.Add(tFaces[1]);
                }
                else
                {
                    outFaces.Add(face);
                }
            }
            return outFaces;
        }

        /// <summary>
        /// String representation of facegroup.
        /// </summary>
        /// <returns>String representation.</returns>
        public override String ToString()
        {
            return string.Format("FaceGroup::{0}", Faces.Count);
        }
    }
}
