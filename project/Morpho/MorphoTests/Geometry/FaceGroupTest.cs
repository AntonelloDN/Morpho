using MorphoGeometry;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MorphoTests.Geometry
{
    public class FaceGroupTest
    {
        private FaceGroup _faceGroup;

        [SetUp]
        public void Setup()
        {
            var pts1 = new[]
            {
                new Vector(0, 0, 0),
                new Vector(5, 0, 0),
                new Vector(0, 5, 0)

            };
            var face1 = new Face(pts1);

            var pts2 = new[]
{
                new Vector(5, 0, 0),
                new Vector(5, 5, 0),
                new Vector(0, 5, 0)

            };
            var face2 = new Face(pts2);
            _faceGroup = new FaceGroup(new List<Face> { face1, face2 });
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _faceGroup.Serialize();
            var jsonInput = "{\"faces\":[{\"vertices\":[{\"x\":0.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]},{\"vertices\":[{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":5.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]}]}";

            Assert.That(jsonOuput, Is.EqualTo(jsonInput));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"faces\":[{\"vertices\":[{\"x\":0.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]},{\"vertices\":[{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":5.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]}]}";
            var faceOutput = FaceGroup.Deserialize(jsonInput);
            Assert.That(faceOutput, Is.EqualTo(_faceGroup));

            jsonInput = "{\"faces\":[{\"vertices\":[{\"x\":0.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":0.0,\"z\":0.0}]},{\"vertices\":[{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":5.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]}]}";
            Assert.Throws<Exception>(() => FaceGroup.Deserialize(jsonInput));
        }
    }
}
