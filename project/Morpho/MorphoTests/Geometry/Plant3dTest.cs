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
    public class Plant3dTest
    {
        private Plant3d _plant3D;

        [SetUp]
        public void Setup()
        {
            var pt = new Vector(10, 10, 0);
            _plant3D = new Plant3d(pt);
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _plant3D.Serialize();
            var jsonInput = "{\"name\":\"PlantGroup\",\"geometry\":{\"x\":10.0,\"y\":10.0,\"z\":0.0},\"material\":{\"ids\":[\"0000C2\"]},\"id\":0}";

            Assert.That(jsonOuput, Is.EqualTo(jsonInput));

            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(Plant3d));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"name\":\"PlantGroup\",\"geometry\":{\"x\":10.0,\"y\":10.0,\"z\":0.0},\"material\":{\"ids\":[\"0000C2\"]},\"id\":0}";
            var matOutput = Plant3d.Deserialize(jsonInput);
            Assert.That(matOutput, Is.EqualTo(_plant3D));

            jsonInput = "{\"ids\":\"0000AA\"}";
            Assert.Throws<Exception>(() => Material.Deserialize(jsonInput));
        }
    }
}
