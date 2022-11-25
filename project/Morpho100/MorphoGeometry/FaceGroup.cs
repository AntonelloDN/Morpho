using System;
using System.Collections.Generic;
using System.Linq;

namespace MorphoGeometry
{
    public class FaceGroup
    {
        public List<Face> Faces { get; }

        public FaceGroup(List<Face> faces)
        {
            Faces = TriangulateFaces(faces);
        }

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

        public override String ToString()
        {
            return string.Format("FaceGroup::{0}", Faces.Count);
        }
    }
}
