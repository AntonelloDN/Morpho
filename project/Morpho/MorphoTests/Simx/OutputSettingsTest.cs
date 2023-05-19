using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class OutputSettingsTest
    {
        [Test]
        public void InitTest()
        {
            var output = new OutputSettings();
            Assert.IsTrue(output.WriteSurface == Active.YES);

            output.WriteAgents = Active.YES;
            Assert.IsTrue(output.WriteAgents == Active.YES);
        }

        [Test]
        public void XMLTest()
        {
            var output = new OutputSettings();

            var values = output.Values;

            Assert.IsTrue(values.Length == 17);
            Assert.IsTrue(values[0] == "60");
            Assert.IsTrue(values[1] == "60");
            Assert.IsTrue(values[6] == "0");
            Assert.IsTrue(values[7] == "1");

            var tags = output.Tags;

            Assert.IsTrue(tags.Length == 17);
            Assert.IsTrue(tags[0] == "mainFiles");

            Assert.IsTrue(output.Title == "OutputSettings");
        }
    }
}
