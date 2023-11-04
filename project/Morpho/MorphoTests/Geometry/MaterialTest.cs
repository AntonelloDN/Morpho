using Morpho25.Geometry;
using NUnit.Framework;
using System;


namespace MorphoTests.Geometry
{
    public class MaterialTest
    {
        private Material _material;

        [SetUp]
        public void Setup()
        {
            var ids = new[]
            {
            "0000AA",
            "0000BB"
        };
            _material = new Material(ids);
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _material.Serialize();
            var jsonInput = "{\"ids\":[\"0000AA\",\"0000BB\"]}";

            Assert.That(jsonOuput, Is.EqualTo(jsonInput));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"ids\":[\"0000AA\",\"0000BB\"]}";
            var matOutput = Material.Deserialize(jsonInput);
            Assert.That(matOutput, Is.EqualTo(_material));

            jsonInput = "{\"ids\":\"0000AA\"}";
            Assert.Throws<Exception>(() => Material.Deserialize(jsonInput));
        }
    }
}
