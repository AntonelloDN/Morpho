using System;
using System.Linq;

namespace MorphoGeometry
{
    /// <summary>
    /// Face class.
    /// </summary>
    public class Face
    {

        private Vector[] _vertices;

        /// <summary>
        /// First vertex.
        /// </summary>
        public Vector A => Vertices[0];

        /// <summary>
        /// Second vertex.
        /// </summary>
        public Vector B => Vertices[1];

        /// <summary>
        /// Third vertex.
        /// </summary>
        public Vector C => Vertices[2];

        /// <summary>
        /// Fourth vertex
        /// </summary>
        public Vector D => Vertices[3];

        /// <summary>
        /// Vertices of the face.
        /// </summary>
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

        /// <summary>
        /// Is point behind a Face.
        /// </summary>
        /// <param name="point">Point for testing.</param>
        /// <returns>0, 1, -1. 0 onto the face. 1 in front 
        /// of the face. -1 behind the face</returns>
        public int IsPointBehind(Vector point)
        {
            var v = Vector.VectorFrom2Points(A, point);
            if (Normal.Dot(v) > 0) return 1;
            else if (Normal.Dot(v) < 0) return -1;
            else return 0;
        }

        /// <summary>
        /// Normal vector.
        /// </summary>
        public Vector Normal
        {
            get
            {
                var dir = (B.Sub(A)).Cross(C.Sub(A));
                var norm = dir.Normalize();
                return norm;
            }
        }

        /// <summary>
        /// Create a new face.
        /// </summary>
        /// <param name="vertices">Vertices.</param>
        public Face(Vector[] vertices)
        {
            Vertices = vertices;
        }

        /// <summary>
        /// Face with 4 vertices.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool IsQuad()
        {
            return (Vertices.Length == 4);
        }

        /// <summary>
        /// Minimun point of the face.
        /// </summary>
        /// <returns>Vector.</returns>
        public Vector Min()
        {
            var x = Vertices.Select(_ => _.x).Min();
            var y = Vertices.Select(_ => _.y).Min();
            var z = Vertices.Select(_ => _.z).Min();

            return new Vector(x, y, z);
        }

        /// <summary>
        /// Maximum point of the face.
        /// </summary>
        /// <returns>Vector.</returns>
        public Vector Max()
        {
            var x = Vertices.Select(_ => _.x).Max();
            var y = Vertices.Select(_ => _.y).Max();
            var z = Vertices.Select(_ => _.z).Max();

            return new Vector(x, y, z);
        }

        /// <summary>
        /// From quadrangular face to triangular face.
        /// </summary>
        /// <param name="face">Face to divide.</param>
        /// <returns>Array of triangular faces.</returns>
        public static Face[] Triangulate(Face face)
        {
            return new Face[]
            {
                new Face( new Vector[3] { face.A, face.B, face.C } ),
                new Face( new Vector[3] { face.C, face.D, face.A } )
            };
        }

        /// <summary>
        /// String representation of the face.
        /// </summary>
        /// <returns>String representation.</returns>
        public override String ToString()
        {
            return string.Format("Face::{0}", Vertices.Length);
        }

    }
}
