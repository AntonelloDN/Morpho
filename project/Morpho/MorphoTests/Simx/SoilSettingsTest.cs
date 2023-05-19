using Morpho25.Settings;
using NUnit.Framework;
using System;

namespace MorphoTests.Simx
{
    internal class SoilSettingsTest
    {
        [Test]
        public void InitTest()
        {
            var soilSettings = new SoilSettings();

            Assert.IsTrue(soilSettings.TempUpperlayer == 293);

            soilSettings.TempUpperlayer = 23.85;

            Assert.IsTrue(soilSettings.TempUpperlayer == 297);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                soilSettings.WaterDeeplayer = -200;
            });
            Assert.IsTrue("Relative humidity go from 0 to 100." == ex.Message);
        }

        [Test]
        public void XMLTest()
        {
            var soilSettings = new SoilSettings();

            var values = soilSettings.Values;

            Assert.IsTrue(values.Length == 8);
            Assert.IsTrue(values[0] == "293.00000");
            Assert.IsTrue(values[4] == "70.00000");

            var tags = soilSettings.Tags;

            Assert.IsTrue(tags.Length == 8);
            Assert.IsTrue(tags[0] == "tempUpperlayer");

            Assert.IsTrue(soilSettings.Title == "Soil");
        }
    }
}
