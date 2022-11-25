using System;
using System.Linq;

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

        public int IsPointBehind(Vector point)
        {
            var v = Vector.VectorFrom2Points(A, point);
            if (Normal.Dot(v) > 0) return 1;
            else if (Normal.Dot(v) < 0) return -1;
            else return 0;
        }


        public Vector Normal
        {
            get
            {
                var dir = (B.Sub(A)).Cross(C.Sub(A));
                var norm = dir.Normalize();
                return norm;
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

        public Vector Min()
        {
            var x = Vertices.Select(_ => _.x).Min();
            var y = Vertices.Select(_ => _.y).Min();
            var z = Vertices.Select(_ => _.z).Min();

            return new Vector(x, y, z);
        }

        public Vector Max()
        {
            var x = Vertices.Select(_ => _.x).Max();
            var y = Vertices.Select(_ => _.y).Max();
            var z = Vertices.Select(_ => _.z).Max();

            return new Vector(x, y, z);
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
