using Morpho25.Settings;
using NUnit.Framework;
using System;

namespace MorphoTests.Simx
{
    internal class CloudsTest
    {
        [Test]
        public void InitTest()
        {
            var clouds = new Cloud();

            Assert.IsTrue(clouds.HighClouds == 0);
            
            clouds.HighClouds = 4;
            Assert.IsTrue(clouds.HighClouds == 4);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                clouds.HighClouds = 10;
            });
            Assert.IsTrue("Value must be in range (0, 8)." == ex.Message);
        }

        [Test]
        public void XMLTest()
        {
            var cloud = new Cloud();

            var values = cloud.Values;

            Assert.IsTrue(values.Length == 3);
            Assert.IsTrue(values[0] == "0.00000");
            Assert.IsTrue(values[1] == "0.00000");

            var tags = cloud.Tags;

            Assert.IsTrue(tags.Length == 3);
            Assert.IsTrue(tags[0] == "lowClouds");

            Assert.IsTrue(cloud.Title == "Clouds");
        }
    }
}
