using System.Linq;

namespace MorphoGeometry
{
    /// <summary>
    /// Boundary box class.
    /// </summary>
    public class BoundaryBox
    {
        private const int NUM_VERT = 3;
        
        /// <summary>
        /// Lower left point.
        /// </summary>
        public Vector MinPoint { get; }

        /// <summary>
        /// Upper right point.
        /// </summary>
        public Vector MaxPoint { get; }

        /// <summary>
        /// Create a new boundary box.
        /// </summary>
        /// <param name="facegroup">Facegroup.</param>
        public BoundaryBox(FaceGroup facegroup)
        {
            float[] coordinateX = new float[facegroup.Faces.Count * NUM_VERT];
            float[] coordinateY = new float[facegroup.Faces.Count * NUM_VERT];
            float[] coordinateZ = new float[facegroup.Faces.Count * NUM_VERT];

            for (int i = 0; i < facegroup.Faces.Count; i++)
            {
                coordinateX[i] = facegroup.Faces[i].A.x;
                coordinateX[i] = facegroup.Faces[i].B.x;
                coordinateX[i] = facegroup.Faces[i].C.x;

                coordinateY[i] = facegroup.Faces[i].A.y;
                coordinateY[i] = facegroup.Faces[i].B.y;
                coordinateY[i] = facegroup.Faces[i].C.y;

                coordinateZ[i] = facegroup.Faces[i].A.z;
                coordinateZ[i] = facegroup.Faces[i].B.z;
                coordinateZ[i] = facegroup.Faces[i].C.z;
            }

            float minX = coordinateX.Min();
            float minY = coordinateY.Min();
            float minZ = coordinateZ.Min();

            float maxX = coordinateX.Max();
            float maxY = coordinateY.Max();
            float maxZ = coordinateZ.Max();

            MinPoint = new Vector(minX, minY, minZ);
            MaxPoint = new Vector(maxX, maxY, maxZ);
        }
    }
}
