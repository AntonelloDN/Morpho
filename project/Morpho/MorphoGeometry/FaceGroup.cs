using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MorphoGeometry
{
    /// <summary>
    /// Facegroup class.
    /// </summary>
    public class FaceGroup : IEquatable<FaceGroup>
    {
        [JsonProperty("faces", Required = Required.Always)]
        /// <summary>
        /// Faces of the facegroup.
        /// </summary>
        public List<Face> Faces { get; }

        [JsonConstructor]
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

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static FaceGroup Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<FaceGroup>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(FaceGroup other)
        {
            if (other == null)
                return false;

            if (other != null
                && Enumerable.SequenceEqual(other.Faces, this.Faces))
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var fgObj = obj as FaceGroup;
            if (fgObj == null)
                return false;
            else
                return Equals(fgObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Faces.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(FaceGroup faceGroup1, FaceGroup faceGroup2)
        {
            if (((object)faceGroup1) == null || ((object)faceGroup2) == null)
                return Object.Equals(faceGroup1, faceGroup2);

            return faceGroup1.Equals(faceGroup2);
        }

        public static bool operator !=(FaceGroup faceGroup1, FaceGroup faceGroup2)
        {
            return !(faceGroup1 == faceGroup2);
        }
    }
}
