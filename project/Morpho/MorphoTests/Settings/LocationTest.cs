using Morpho25.Settings;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using NUnit.Framework;
using System;

namespace MorphoTests.Settings
{
    public class LocationTest
    {
        private Location _location;

        [SetUp]
        public void Setup()
        {
            _location = new Location(42, 12);
        }

        [Test]
        public void SerializeTest()
        {
            var jsonOuput = _location.Serialize();
            var jsonInput = "{\"locationName\":\"Envimet Location\",\"latitude\":42.0,\"longitude\":12.0,\"timezoneReference\":15.0,\"timeZone\":\"GMT\",\"modelRotation\":0.0}";
            Assert.IsTrue(jsonOuput.Equals(jsonInput));

            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(Location));
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonInput = "{\"locationName\":\"Envimet Location\",\"latitude\":42.0,\"longitude\":12.0,\"timezoneReference\":15.0,\"timeZone\":\"GMT\",\"modelRotation\":0.0}";
            var matOutput = Location.Deserialize(jsonInput);
            Assert.That(matOutput, Is.EqualTo(_location));

            jsonInput = "{\"locationName\":\"Envimet Location\",\"latitude\":91.0,\"longitude\":12.0,\"timezoneReference\":15.0,\"timeZone\":\"GMT\",\"modelRotation\":0.0}";
            Assert.Throws<Exception>(() => Location.Deserialize(jsonInput));
        }
    }
}
