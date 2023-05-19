using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class TurbulenceTest
    {
        [Test]
        public void InitTest()
        {
            var turbulence = new Turbulence();
            Assert.IsTrue(turbulence.TurbulenceModel == TurbolenceType.Bruse);
            
            turbulence.TurbulenceModel = TurbolenceType.KatoAndLaunder;
            Assert.IsTrue(turbulence.TurbulenceModel == TurbolenceType.KatoAndLaunder);
        }

        [Test]
        public void XMLTest()
        {
            var turbulence = new Turbulence();

            var values = turbulence.Values;

            Assert.IsTrue(values.Length == 1);
            Assert.IsTrue(values[0] == "3");

            var tags = turbulence.Tags;

            Assert.IsTrue(tags.Length == 1);
            Assert.IsTrue(tags[0] == "turbulenceModel");

            Assert.IsTrue(turbulence.Title == "Turbulence");
        }
    }
}
