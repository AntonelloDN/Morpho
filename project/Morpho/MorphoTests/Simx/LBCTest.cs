using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class LBCTest
    {
        [Test]
        public void InitTest()
        {
            var lbc = new LBC();
            Assert.IsTrue(lbc.TemperatureHumidity == BoundaryCondition.Open);

            lbc.TemperatureHumidity = BoundaryCondition.Cyclic;
            Assert.IsTrue(lbc.TemperatureHumidity == BoundaryCondition.Cyclic);
        }

        [Test]
        public void XMLTest()
        {
            var lbc = new LBC();
            var values = lbc.Values;

            Assert.IsTrue(values.Length == 2);
            Assert.IsTrue(values[0] == "1");

            var tags = lbc.Tags;

            Assert.IsTrue(tags.Length == 2);
            Assert.IsTrue(tags[0] == "LBC_TQ");

            Assert.IsTrue(lbc.Title == "LBC");
        }
    }
}
