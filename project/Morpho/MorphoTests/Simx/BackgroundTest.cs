using Morpho25.Settings;
using NUnit.Framework;
using System;

namespace MorphoTests.Simx
{
    internal class BackgroundTest
    {
        [Test]
        public void InitTest()
        {
            var background = new Background();
            Assert.IsTrue(background.Pm10 == 0);

            background.Pm25 = 5;
            Assert.IsTrue(background.Pm25 == 5);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                background.No2 = -23;
            });
            Assert.IsTrue("You cannot insert negative numbers" == ex.Message);
        }

        [Test]
        public void XMLTest()
        {
            var background = new Background();

            var values = background.Values;

            Assert.IsTrue(values.Length == 6);
            Assert.IsTrue(values[0] == "0.00000");
            Assert.IsTrue(values[1] == "0.00000");

            var tags = background.Tags;

            Assert.IsTrue(tags.Length == 6);
            Assert.IsTrue(tags[0] == "userSpec");

            Assert.IsTrue(background.Title == "Background");
        }
    }
}
