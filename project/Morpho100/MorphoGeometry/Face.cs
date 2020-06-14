using System;

namespace MorphoGeometry
{
    public class Face
    {

        private Vector[] _vertices;

        public Vector A => Vertices[0];
        public Vector B => Vertices[1];
        public Vector C => Vertices[2];
        public Vector D => Vertices[3];

        public Vector[] Vertices
        {
            get { return _vertices; }
            private set
            {
                if (value.Length == 3 || value.Length == 4)
                    _vertices = value;
                else
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} has contain 3 or 4 vectors.");
            }
        }

        public Face(Vector[] vertices)
        {
            Vertices = vertices;
        }

        public bool IsQuad()
        {
            return (Vertices.Length == 4);
        }

        public static Face[] Triangulate(Face face)
        {
            return new Face[]
            {
                new Face( new Vector[3] { face.A, face.B, face.C } ),
                new Face( new Vector[3] { face.C, face.D, face.A } )
            };
        }

        public override String ToString()
        {
            return string.Format("Face::{0}", Vertices.Length);
        }

    }
}
