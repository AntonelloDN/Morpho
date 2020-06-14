using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphoGeometry
{
    public class BoundaryBox
    {
        public Vector MinPoint { get; }
        public Vector MaxPoint { get; }

        public BoundaryBox(FaceGroup facegroup)
        {
            float[] coordinateX = new float[facegroup.Faces.Count];
            float[] coordinateY = new float[facegroup.Faces.Count];
            float[] coordinateZ = new float[facegroup.Faces.Count];

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
