﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MorphoGeometry
{
    /// <summary>
    /// Intersection class.
    /// </summary>
    public class Intersection
    {
        /// <summary>
        /// Ray face intersection.
        /// </summary>
        /// <param name="ray">Ray.</param>
        /// <param name="face">Face.</param>
        /// <param name="reverse">Reverse the face.</param>
        /// <param name="project">Project the intersection points.</param>
        /// <returns>Intersection point.</returns>
        public static Vector RayFaceIntersect(Ray ray, Face face, 
            bool reverse = false, bool project = false)
        {
            Vector v0, v1, v2;

            if (reverse)
            {
                v1 = face.A;
                v0 = face.B;
                v2 = face.C;
            }
            else
            {
                v0 = face.A;
                v1 = face.B;
                v2 = face.C;
            }

            Vector v0v1 = v1.Sub(v0);
            Vector v0v2 = v2.Sub(v0);
            Vector pvec = ray.direction.Cross(v0v2);
            float det = v0v1.Dot(pvec);

            if (det < 0.000001)
                return null;

            float invDet = (float)(1.0 / det);
            Vector tvec = ray.origin.Sub(v0);
            float u = tvec.Dot(pvec) * invDet;

            if (u < 0 || u > 1)
                return null;

            Vector qvec = tvec.Cross(v0v1);
            float v = ray.direction.Dot(qvec) * invDet;

            if (v < 0 || u + v > 1)
                return null;

            float distance = (v0v2.Dot(qvec) * invDet);
            float c = (float) Math.Sqrt(ray.direction.x * 
                ray.direction.x + ray.direction.y * ray.direction.y +
                ray.direction.z * ray.direction.z);
            float angle = distance / c;

            Vector intersection = new Vector(ray.origin.x + 
                ray.direction.x * angle, ray.origin.y + 
                ray.direction.y * angle, ray.origin.z + ray.direction.z * angle);

            if (project)
                intersection = new Vector(ray.origin.x, ray.origin.y, ray.origin.z);

            return intersection;
        }

        /// <summary>
        /// Rays facegroup intersection.
        /// </summary>
        /// <param name="rays">Rays.</param>
        /// <param name="facegroup">Facegroup to hit.</param>
        /// <param name="RayMethod">Intersection method.</param>
        /// <param name="reverse">Reverse the faces.</param>
        /// <param name="project">Project the intersection points.</param>
        /// <returns>Intersection points.</returns>
        public static IEnumerable<Vector> RaysFaceGroupIntersect(IEnumerable<Ray> rays,
            FaceGroup facegroup,
            Func<Ray, Face, bool, bool, Vector> RayMethod,
            bool reverse = false,
            bool project = false)
        {
            var faceIntersectArr = new List<Vector>[facegroup.Faces.Count];

            Parallel.For(0, facegroup.Faces.Count, i =>
            {
                var partial = new List<Vector>();
                var face = facegroup.Faces[i];
                var min = face.Min();
                var max = face.Max();

                foreach (var ray in rays)
                {
                    if (ray.origin.x < min.x) continue;
                    if (ray.origin.y < min.y) continue;
                    if (ray.origin.x > max.x) continue;
                    if (ray.origin.y > max.y) continue;

                    var intersection = RayMethod(ray, face, reverse, project);
                    if (intersection != null)
                    {
                        partial.Add(intersection);
                    }
                }
                faceIntersectArr[i] = partial;
            });
            var intersections = faceIntersectArr
                .SelectMany(i => i)
                .Where(_ => _ != null);

            return intersections;
        }

        /// <summary>
        /// Ray face intersection both face directions.
        /// </summary>
        /// <param name="ray">Ray.</param>
        /// <param name="face">Face.</param>
        /// <param name="reverse">Reverse the face.</param>
        /// <param name="project">Project intersection points.</param>
        /// <returns>Intersection point.</returns>
        public static Vector RayFaceIntersectFrontBack(Ray ray, 
            Face face, bool reverse, bool project)
        {
            if (face.IsPointBehind(ray.origin) < 0)
            {
                return RayFaceIntersect(ray, face, true, project);
            }
            else
            {
                return RayFaceIntersect(ray, face, reverse, project);
            }
        }

        /// <summary>
        /// Is point inside a facegroup.
        /// </summary>
        /// <param name="vectors">Points to test.</param>
        /// <param name="facegroup">Facegroup.</param>
        /// <returns>Inside points.</returns>
        public static IEnumerable<Vector> IsPointInsideFaceGroup(List<Vector> vectors,
            FaceGroup facegroup)
        {
            var points = new Vector[vectors.Count()];

            for (int i = 0; i < vectors.Count; i++)
            {
                if (IsPointInside(facegroup, vectors[i]))
                {
                    points[i] = vectors[i];
                }
            }
            var intersections = points
                .Where(_ => _ != null);

            return intersections;
        }

        /// <summary>
        /// Is point inside a facegroup.
        /// </summary>
        /// <param name="facegroup">Facegroup.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>True or false.</returns>
        public static bool IsPointInside(FaceGroup facegroup, Vector point)
        {
            var triangles = facegroup.ToArray();
            var P = point.ToArray();

            foreach (var tri in triangles)
                foreach (var pt in tri)
                    for (var i = 0; i < pt.Length; i++)
                        pt[i] -= P[i];

            var qM = new float[triangles.Length][][];
            var Mnorm = new float[triangles.Length][];
            for (var i = 0; i < triangles.Length; i++)
            {
                var tri = triangles[i];
                qM[i] = new float[tri.Length][];
                Mnorm[i] = new float[tri.Length];
                for (var j = 0; j < tri.Length; j++)
                {
                    var pt = tri[j];
                    qM[i][j] = new float[pt.Length];
                    for (int k = 0; k < pt.Length; k++)
                    {
                        qM[i][j][k] = Convert.ToSingle(Math.Pow((double)pt[k], 
                            (double)2));
                    }
                    Mnorm[i][j] = Convert.ToSingle(Math
                        .Sqrt((double)qM[i][j].Sum()));
                }
            }

            var winding_number = 0.0;
            for (var i = 0; i < Mnorm.Length; i++)
            {
                triangles[i].Deconstruct(out float[] A, out float[] B, out float[] C);
                Mnorm[i].Deconstruct(out float a, out float b, out float c);

                var j = (a * b * c);
                var jj = c * (Vector.FromArray(A)
                    .Dot(Vector.FromArray(B)));
                var jjj = a * (Vector.FromArray(B)
                    .Dot(Vector.FromArray(C)));
                var jjjj = b * (Vector.FromArray(C)
                    .Dot(Vector.FromArray(A)));

                var tot = j + jj + jjj + jjjj;

                var det = Vector.Det3x3(new float[3][] { A, B, C });
                winding_number += Math.Atan2((double)det, (double)tot - 1);

            }
            return winding_number >= (2.0 * Math.PI);
        }
    }
}