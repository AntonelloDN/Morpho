using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class SORTest
    {
        [Test]
        public void InitTest()
        {
            var sor = new SOR();
            Assert.IsTrue(sor.SORMode == Active.YES);

            sor.SORMode = Active.NO;
            Assert.IsTrue(sor.SORMode == Active.NO);
        }

        [Test]
        public void XMLTest()
        {
            var sor = new SOR();
            var values = sor.Values;

            Assert.IsTrue(values.Length == 1);
            Assert.IsTrue(values[0] == "1");

            var tags = sor.Tags;

            Assert.IsTrue(tags.Length == 1);
            Assert.IsTrue(tags[0] == "SORMode");

            Assert.IsTrue(sor.Title == "SOR");
        }
    }
}
