using Morpho25.Settings;
using NUnit.Framework;
using System;

namespace MorphoTests.Simx
{
    internal class TimeStepsTest
    {
        [Test]
        public void InitTest()
        {
            var timestep = new TimeSteps();

            Assert.IsTrue(timestep.SunheightStep01 == 40);

            timestep.SunheightStep02 = 55;

            Assert.IsTrue(timestep.SunheightStep02 == 55);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                timestep.SunheightStep02 = -10;
            });
            Assert.IsTrue("You cannot insert negative numbers" == ex.Message);
        }

        [Test]
        public void XMLTest()
        {
            var timestep = new TimeSteps();

            var values = timestep.Values;

            Assert.IsTrue(values.Length == 5);
            Assert.IsTrue(values[0] == "40.00000");
            Assert.IsTrue(values[1] == "50.00000");

            var tags = timestep.Tags;

            Assert.IsTrue(tags.Length == 5);
            Assert.IsTrue(tags[0] == "sunheight_step01");

            Assert.IsTrue(timestep.Title == "TimeSteps");
        }
    }
}
