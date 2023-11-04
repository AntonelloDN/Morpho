using MorphoGeometry;
using NUnit.Framework;
using System;

namespace MorphoTests.Geometry
{
    public class VectorTest
    {
        private Vector _vector;

        [SetUp]
        public void Setup()
        {
            _vector = new Vector(1, 2, 3);
        }
        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _vector.Serialize();
            var jsonInput = "{\"x\":1.0,\"y\":2.0,\"z\":3.0}";

            Assert.That(jsonOuput, Is.EqualTo(jsonInput));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"x\":1.0,\"y\":2.0,\"z\":3.0}";
            var vectorOutput = Vector.Deserialize(jsonInput);
            Assert.That(vectorOutput, Is.EqualTo(_vector));

            jsonInput = "{\"x\":1.0,\"y\":2.0}";
            Assert.Throws<Exception>(() => Vector.Deserialize(jsonInput));
        }
    }
}
