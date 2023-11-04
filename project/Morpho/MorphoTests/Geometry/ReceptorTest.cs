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
    public class ReceptorTest
    {
        private Receptor _receptor;

        [SetUp]
        public void Setup()
        {
            var pt = new Vector(10, 10, 0);
            _receptor = new Receptor(pt, "MyReceptor");
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _receptor.Serialize();
            var jsonInput = "{\"name\":\"MyReceptor\",\"geometry\":{\"x\":10.0,\"y\":10.0,\"z\":0.0},\"id\":0}";

            Assert.That(jsonOuput, Is.EqualTo(jsonInput));

            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(Receptor));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"name\":\"MyReceptor\",\"geometry\":{\"x\":10.0,\"y\":10.0,\"z\":0.0},\"id\":0}";
            var matOutput = Receptor.Deserialize(jsonInput);
            Assert.That(matOutput, Is.EqualTo(_receptor));

            jsonInput = "{\"ids\":\"0000AA\"}";
            Assert.Throws<Exception>(() => Material.Deserialize(jsonInput));
        }
    }
}
