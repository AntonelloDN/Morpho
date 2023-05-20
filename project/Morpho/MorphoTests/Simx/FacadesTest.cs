using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class FacadesTest
    {
        [Test]
        public void InitTest()
        {
            var facades = new Facades();

            Assert.IsTrue(facades.FacadeMode == FacadeMod.DIN6946);

            facades.FacadeMode = FacadeMod.MO;
            Assert.IsTrue(facades.FacadeMode == FacadeMod.MO);
        }

        [Test]
        public void XMLTest()
        {
            var facade = new Facades();
            var values = facade.Values;

            Assert.IsTrue(values.Length == 1);
            Assert.IsTrue(values[0] == "1");

            var tags = facade.Tags;

            Assert.IsTrue(tags.Length == 1);
            Assert.IsTrue(tags[0] == "FacadeMode");

            Assert.IsTrue(facade.Title == "Facades");
        }
    }
}
