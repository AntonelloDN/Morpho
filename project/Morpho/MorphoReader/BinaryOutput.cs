using Morpho25.Utility;
using MorphoGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MorphoReader
{
    /// <summary>
    /// Direction enum.
    /// </summary>
    public enum Direction
    {
        X,
        Y,
        Z
    }

    /// <summary>
    /// Binary output of envimet.
    /// </summary>
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

        public delegate Face FaceByDirection(float spacingX, float spacingY, 
            float spacingZ, Vector centroid);
        
        /// <summary>
        /// Create new binary output object.
        /// </summary>
        /// <param name="edx">EDX file.</param>
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

        /// <summary>
        /// Set variable names
        /// </summary>
        /// <param name="outputKeys">Keys.</param>
        protected void SetVariableName(Dictionary<string, string> outputKeys)
        {
            VariableName = outputKeys["name_variables"].Split(',');
        }

        /// <summary>
        /// Set X Y Z axis.
        /// </summary>
        /// <param name="outputKeys">Keys.</param>
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

        /// <summary>
        /// Set X Y Z sequences.
        /// </summary>
        /// <param name="outputKeys">Keys.</param>
        protected void Setsequence(Dictionary<string, string> outputKeys)
        {
            _sequenceX = Util.Accumulate(_spacingX).ToList();
            _sequenceY = Util.Accumulate(_spacingY).ToList();
            _sequenceZ = Util.Accumulate(_spacingZ).ToList();
        }

        /// <summary>
        /// Set number of cells X Y Z.
        /// </summary>
        /// <param name="outputKeys">Keys.</param>
        protected void SetNumberOfCells(Dictionary<string, string> outputKeys)
        {
            _numX = Convert.ToInt32(outputKeys["nr_xdata"]);
            _numY = Convert.ToInt32(outputKeys["nr_ydata"]);
            _numZ = Convert.ToInt32(outputKeys["nr_zdata"]);
        }

        /// <summary>
        /// Set generic information.
        /// </summary>
        /// <param name="outputKeys">Keys.</param>
        protected void SetGeneralInfo(Dictionary<string, string> outputKeys)
        {
            ProjectName = outputKeys["projectname"];
            DataContent = Convert.ToInt32(outputKeys["data_content"]);
            LocationName = outputKeys["locationname"];
            SimulationDate = outputKeys["simulation_date"];
            SimulationTime = outputKeys["simulation_time"];
        }

        /// <summary>
        /// Get building voxels from EDT EDX.
        /// </summary>
        /// <param name="faceByDirection">Facade direction.</param>
        /// <returns></returns>
        protected List<Voxel> GetFacadesFromBinary(FaceByDirection faceByDirection)
        {

            List<Voxel> voxels = new List<Voxel>();

            Vector vector;
            Face face;
            Voxel facade;

            for (int k = 0; k < _numZ; k++)
            {
                for (int j = 0; j < _numY; j++)
                {
                    for (int i = 0; i < _numX; i++)
                    {
                        vector = new Vector((float)_sequenceX[i] + BasePoint.x - (float)_spacingX[i], (float)_sequenceY[j] + BasePoint.y - (float)_spacingY[j], (float)_sequenceZ[k] + BasePoint.z);
                        face = faceByDirection((float)_spacingX[i], (float)_spacingY[j], (float)_spacingZ[k], vector);

                        facade = new Voxel(new Pixel(i, j, k), face);
                        voxels.Add(facade);
                    }
                }
            }

            return voxels;
        }

        /// <summary>
        /// Facade in X.
        /// </summary>
        /// <param name="spacingX">Spacing value in X.</param>
        /// <param name="spacingY">Spacing value in Y.</param>
        /// <param name="spacingZ">Spacing value in Z.</param>
        /// <param name="centroid">Centroid.</param>
        /// <returns>Selected face.</returns>
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

        /// <summary>
        /// Facade in Y.
        /// </summary>
        /// <param name="spacingX">Spacing value in X.</param>
        /// <param name="spacingY">Spacing value in Y.</param>
        /// <param name="spacingZ">Spacing value in Z.</param>
        /// <param name="centroid">Centroid.</param>
        /// <returns>Selected face.</returns>
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

        /// <summary>
        /// Facade in Z.
        /// </summary>
        /// <param name="spacingX">Spacing value in X.</param>
        /// <param name="spacingY">Spacing value in Y.</param>
        /// <param name="spacingZ">Spacing value in Z.</param>
        /// <param name="centroid">Centroid.</param>
        /// <returns>Selected face.</returns>
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

        /// <summary>
        /// Get voxels by direction.
        /// </summary>
        /// <param name="direction">Direction.</param>
        /// <returns>Collection of voxels.</returns>
        public List<Voxel> GetVoxels(Direction direction)
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

        /// <summary>
        /// Set values reading EDX file.
        /// </summary>
        /// <param name="edt">EDT file.</param>
        /// <param name="voxels">Facades.</param>
        /// <param name="variable">Index of the variable to read.</param>
        public abstract void SetValuesFromBinary(string edt, 
            List<Voxel> voxels, int variable);
    }
}
