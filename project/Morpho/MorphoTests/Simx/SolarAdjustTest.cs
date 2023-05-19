using Morpho25.Settings;
using NUnit.Framework;
using System;

namespace MorphoTests.Simx
{
    internal class SolarAdjustTest
    {
        [Test]
        public void InitTest()
        {
            var solar = new SolarAdjust();
            Assert.IsTrue(solar.SWfactor == 1);

            solar.SWfactor = 1.2;
            Assert.IsTrue(solar.SWfactor == 1.2);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                solar.SWfactor = 10;
            });
            Assert.IsTrue("Sw factor must be in range (0.5, 1.50)." == ex.Message);
        }

        [Test]
        public void XMLTest()
        {
            var solar = new SolarAdjust();
            var values = solar.Values;

            Assert.IsTrue(values.Length == 1);
            Assert.IsTrue(values[0] == "1.00000");

            var tags = solar.Tags;

            Assert.IsTrue(tags.Length == 1);
            Assert.IsTrue(tags[0] == "SWFactor");

            Assert.IsTrue(solar.Title == "SolarAdjust");
        }
    }
}
