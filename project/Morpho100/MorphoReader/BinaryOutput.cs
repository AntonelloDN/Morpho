using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MorphoReader
{
    public enum Direction
    {
        X,
        Y,
        Z
    }

    public abstract class BinaryOutput
    {
        protected int _numX;
        protected int _numY;
        protected int _numZ;

        protected List<double> _sequenceX;
        protected List<double> _sequenceY;
        protected List<double> _sequenceZ;

        protected List<double> _spacingX;
        protected List<double> _spacingY;
        protected List<double> _spacingZ;

        protected int _offset;
        protected int _buffer;

        public int NumX => _numX;
        public int NumY => _numY;
        public int NumZ => _numZ;

        public string[] VariableName { get; private set; }
        public Vector BasePoint { get; protected set; }

        public int DataContent { get; protected set; }
        public string ProjectName { get; protected set; }
        public string LocationName { get; protected set; }
        public string SimulationDate { get; protected set; }
        public string SimulationTime { get; protected set; }

        public delegate Face FaceByDirection(float spacingX, float spacingY, float spacingZ, Vector centroid);

        public BinaryOutput(string edx)
        {
            Read edxFile = new Read(edx);
            var outputKeys = edxFile.Information;

            SetNumberOfCells(outputKeys);
            SetSpacing(outputKeys);
            Setsequence(outputKeys);
            SetVariableName(outputKeys);
            SetGeneralInfo(outputKeys);
        }

        protected void SetVariableName(Dictionary<string, string> outputKeys)
        {
            VariableName = outputKeys["name_variables"].Split(',');
        }

        protected void SetSpacing(Dictionary<string, string> outputKeys)
        {
            _spacingX = outputKeys["spacing_x"]
                .Split(',')
                .Select(_ => Convert.ToDouble(_))
                .ToList();
            _spacingY = outputKeys["spacing_y"]
                .Split(',')
                .Select(_ => Convert.ToDouble(_))
                .ToList();
            _spacingZ = outputKeys["spacing_z"]
                .Split(',')
                .Select(_ => Convert.ToDouble(_))
                .ToList();
        }

        protected void Setsequence(Dictionary<string, string> outputKeys)
        {
            _sequenceX = Util.Accumulate(_spacingX).ToList();
            _sequenceY = Util.Accumulate(_spacingY).ToList();
            _sequenceZ = Util.Accumulate(_spacingZ).ToList();
        }

        protected void SetNumberOfCells(Dictionary<string, string> outputKeys)
        {
            _numX = Convert.ToInt32(outputKeys["nr_xdata"]);
            _numY = Convert.ToInt32(outputKeys["nr_ydata"]);
            _numZ = Convert.ToInt32(outputKeys["nr_zdata"]);
        }

        protected void SetGeneralInfo(Dictionary<string, string> outputKeys)
        {
            ProjectName = outputKeys["projectname"];
            DataContent = Convert.ToInt32(outputKeys["data_content"]);
            LocationName = outputKeys["locationname"];
            SimulationDate = outputKeys["simulation_date"];
            SimulationTime = outputKeys["simulation_time"];
        }

        protected List<Facade> GetFacadesFromBinary(FaceByDirection faceByDirection)
        {

            List<Facade> facades = new List<Facade>();

            Vector vector;
            Face face;
            Facade facade;

            for (int k = 0; k < _numZ; k++)
            {
                for (int j = 0; j < _numY; j++)
                {
                    for (int i = 0; i < _numX; i++)
                    {
                        vector = new Vector((float)_sequenceX[i] + BasePoint.x - (float)_spacingX[i], (float)_sequenceY[j] + BasePoint.y - (float)_spacingY[j], (float)_sequenceZ[k] + BasePoint.z);
                        face = faceByDirection((float)_spacingX[i], (float)_spacingY[j], (float)_spacingZ[k], vector);

                        facade = new Facade(new Pixel(i, j, k), face);
                        facades.Add(facade);
                    }
                }
            }

            return facades;
        }

        protected Face FaceX(float spacingX, float spacingY, float spacingZ, Vector centroid)
        {
            var points = new Vector[]
            {
                new Vector(centroid.x - (spacingX / 2), centroid.y - (spacingY / 2), centroid.z - spacingZ),
                new Vector(centroid.x - (spacingX / 2), centroid.y + (spacingY / 2), centroid.z - spacingZ),
                new Vector(centroid.x - (spacingX / 2), centroid.y + (spacingY / 2), centroid.z),
                new Vector(centroid.x - (spacingX / 2), centroid.y - (spacingY / 2), centroid.z)
            };

            Face face = new Face(points);

            return face;
        }

        protected Face FaceY(float spacingX, float spacingY, float spacingZ, Vector centroid)
        {

            var points = new Vector[]
            {
                new Vector(centroid.x - (spacingX / 2), centroid.y - (spacingY / 2), centroid.z - spacingZ),
                new Vector(centroid.x + (spacingX / 2), centroid.y - (spacingY / 2), centroid.z - spacingZ),
                new Vector(centroid.x + (spacingX / 2), centroid.y - (spacingY / 2), centroid.z),
                new Vector(centroid.x - (spacingX / 2), centroid.y - (spacingY / 2), centroid.z)
            };

            Face face = new Face(points);

            return face;
        }

        protected Face FaceZ(float spacingX, float spacingY, float spacingZ, Vector centroid)
        {

            var points = new Vector[]
            {
                new Vector(centroid.x - (spacingX / 2), centroid.y - (spacingY / 2), centroid.z - spacingZ),
                new Vector(centroid.x + (spacingX / 2), centroid.y - (spacingY / 2), centroid.z - spacingZ),
                new Vector(centroid.x + (spacingX / 2), centroid.y + (spacingY / 2), centroid.z - spacingZ),
                new Vector(centroid.x - (spacingX / 2), centroid.y + (spacingY / 2), centroid.z - spacingZ)
            };

            Face face = new Face(points);

            return face;
        }

        public List<Facade> GetFacades(Direction direction)
        {

            if (direction == Direction.X)
            {
                return GetFacadesFromBinary(FaceX);
            }
            else if (direction == Direction.Y)
            {
                return GetFacadesFromBinary(FaceY);
            }
            else
            {
                return GetFacadesFromBinary(FaceZ);
            }

        }

        public abstract void SetValuesFromBinary(string edt, List<Facade> facades, int variable);
    }
}
