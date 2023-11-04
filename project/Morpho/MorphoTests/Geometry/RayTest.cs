using MorphoGeometry;
using NUnit.Framework;
using System;

namespace MorphoTests.Geometry
{
    public class RayTest
    {
        private Ray _ray;

        [SetUp]
        public void Setup()
        {
            var origin = new Vector(0, 0, 0);
            var dir = new Vector(0, 0, 1);

            _ray = new Ray(origin, dir);
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _ray.Serialize();
            var jsonInput = "{\"origin\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"direction\":{\"x\":0.0,\"y\":0.0,\"z\":1.0}}";

            Assert.That(jsonOuput, Is.EqualTo(jsonInput));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"origin\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"direction\":{\"x\":0.0,\"y\":0.0,\"z\":1.0}}";
            var rayOutput = Ray.Deserialize(jsonInput);
            Assert.That(rayOutput, Is.EqualTo(_ray));

            jsonInput = "{\"origin\":{\"x\":0.0,\"y\":0.0},\"direction\":{\"x\":0.0,\"y\":0.0,\"z\":1.0}}";
            Assert.Throws<Exception>(() => Ray.Deserialize(jsonInput));
        }
    }
}
