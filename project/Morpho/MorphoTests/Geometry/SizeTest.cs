using Morpho25.Geometry;
using MorphoGeometry;
using NUnit.Framework;
using System;

namespace MorphoTests.Geometry
{
    public class SizeTest
    {
        private Size _size;

        [SetUp]
        public void Setup()
        {
            var origin = new Vector(0, 0, 0);
            var dimension = new CellDimension(3.0, 3.0, 3.0);

            _size = new Size(origin, dimension, 50, 50, 24);
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _size.Serialize();
            var jsonInput = "{\"cellDimension\":{\"x\":3.0,\"y\":3.0,\"z\":3.0},\"numX\":50,\"numY\":50,\"numZ\":24,\"origin\":{\"x\":0.0,\"y\":0.0,\"z\":0.0}}";
            Assert.IsTrue(jsonOuput.Equals(jsonInput));
        }
    }
}
