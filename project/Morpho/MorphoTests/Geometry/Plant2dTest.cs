using Morpho25.Geometry;
using Morpho25.Utility;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System;
using System.Linq;
using MorphoGeometry;
using System.Collections.Generic;

namespace MorphoTests.Geometry
{
    public class Plant2dTest
    {
        private Plant2d _plant2D;

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
            var facegroup = new FaceGroup(new List<Face> { face1, face2 });

            _plant2D = new Plant2d(facegroup, 1);
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _plant2D.Serialize();
            var jsonInput = "{\"name\":\"PlantGroup\",\"geometry\":{\"faces\":[{\"vertices\":[{\"x\":0.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]},{\"vertices\":[{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":5.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]}]},\"material\":{\"ids\":[\"0000XX\"]},\"id\":1}";

            Assert.That(jsonOuput, Is.EqualTo(jsonInput));

            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(Plant2d));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"name\":\"PlantGroup\",\"geometry\":{\"faces\":[{\"vertices\":[{\"x\":0.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]},{\"vertices\":[{\"x\":5.0,\"y\":0.0,\"z\":0.0},{\"x\":5.0,\"y\":5.0,\"z\":0.0},{\"x\":0.0,\"y\":5.0,\"z\":0.0}]}]},\"material\":{\"ids\":[\"0000XX\"]},\"id\":1}";
            var matOutput = Plant2d.Deserialize(jsonInput);
            Assert.That(matOutput, Is.EqualTo(_plant2D));

            jsonInput = "{\"ids\":\"0000AA\"}";
            Assert.Throws<Exception>(() => Material.Deserialize(jsonInput));
        }
    }
}
