using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class InflowAVGTest
    {
        [Test]
        public void InitTest()
        {
            var avg = new InflowAvg();
            Assert.IsTrue(avg.Avg == Active.YES);

            avg.Avg = Active.NO;
            Assert.IsTrue(avg.Avg == Active.NO);
        }

        [Test]
        public void XMLTest()
        {
            var avg = new InflowAvg();
            var values = avg.Values;

            Assert.IsTrue(values.Length == 1);
            Assert.IsTrue(values[0] == "1");

            var tags = avg.Tags;

            Assert.IsTrue(tags.Length == 1);
            Assert.IsTrue(tags[0] == "inflowAvg");

            Assert.IsTrue(avg.Title == "InflowAvg");
        }
    }
}
