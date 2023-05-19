using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class SourcesTest
    {
        [Test]
        public void InitTest()
        {
            var sources = new Sources();

            Assert.IsTrue(sources.UserPolluName == "My Pollutant");

            sources.UserPolluType = Pollutant.PM;

            Assert.IsTrue(sources.UserPolluType == Pollutant.PM);
        }

        [Test]
        public void XMLTest()
        {
            var sources = new Sources();

            var values = sources.Values;

            Assert.IsTrue(values.Length == 7);
            Assert.IsTrue(values[0] == "My Pollutant");
            Assert.IsTrue(values[2] == "10.00000");

            var tags = sources.Tags;

            Assert.IsTrue(tags.Length == 7);
            Assert.IsTrue(tags[0] == "userPolluName");

            Assert.IsTrue(sources.Title == "Sources");
        }
    }
}
