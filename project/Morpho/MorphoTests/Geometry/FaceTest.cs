using MorphoGeometry;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MorphoTests.Geometry
{
    public class FaceTest
    {
        private Face _face;

        [SetUp]
        public void Setup()
        {
            var pts = new[]
            {
                new Vector(0, 0, 0),
                new Vector(5, 0, 0),
                new Vector(5, 5, 0),
                new Vector(0, 5, 0)

            };
            _face = new Face(pts);
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _face.Serialize();
            var jsonInput = "{\"vertices\":[{\"x\":0.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":5.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]}";

            Assert.That(jsonOuput, Is.EqualTo(jsonInput));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"vertices\":[{\"x\":0.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":5.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]}";
            var faceOutput = Face.Deserialize(jsonInput);
            Assert.That(faceOutput, Is.EqualTo(_face));

            jsonInput = "{\"vertices\":[{\"x\":0.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":0.0,\"z\":0.0}]}";
            Assert.Throws<Exception>(() => Face.Deserialize(jsonInput));
        }
    }
}
