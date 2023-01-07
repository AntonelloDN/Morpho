using Morpho25.Geometry;
using Morpho25.Utility;
using MorphoReader;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MorphoTests.Reader
{
    public class GridOutputTest
    {
        private string _edx;
        private string _edt;

        [SetUp]
        public void Setup()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _edx = Path.Combine(dir, "Assets/Larino_sim_AT_2018-06-23_12.00.01.EDX");
            _edt = Path.Combine(dir, "Assets/Larino_sim_AT_2018-06-23_12.00.01.EDT");
        }

        [Test]
        public void GridOutputInit()
        {
            const int AIR_TEMPERATURE = 8;

            var output = new GridOutput(_edx);
            // Get voxels
            var voxels = output.GetVoxels(Direction.Z);
            // Set values. 8 is Air Temperature
            output.SetValuesFromBinary(_edt, voxels, AIR_TEMPERATURE);
            // Filter
            voxels = Voxel.GetSliceByPixelCoordinate(voxels, AIR_TEMPERATURE, Direction.Z);

            // Values
            var values = Voxel.GetValueZFromVoxels(voxels);

            Assert.IsTrue(values.Count == output.NumY * output.NumX);
        }
    }
}
