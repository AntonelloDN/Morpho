using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class BuildingSettingsTest
    {
        [Test]
        public void InitTest()
        {
            var building = new BuildingSettings();
            Assert.IsTrue(building.IndoorTemp == 293);

            building.IndoorTemp = 22;
            Assert.IsTrue(building.IndoorTemp == 295.15);

            building.AirCondHeat = Active.YES;
            Assert.IsTrue(building.AirCondHeat == Active.YES);
        }

        [Test]
        public void XMLTest()
        {
            var building = new BuildingSettings();
            var values = building.Values;

            Assert.IsTrue(values.Length == 4);
            Assert.IsTrue(values[0] == "293.00000");
            Assert.IsTrue(values[2] == "1");

            var tags = building.Tags;

            Assert.IsTrue(tags.Length == 4);
            Assert.IsTrue(tags[0] == "surfaceTemp");

            Assert.IsTrue(building.Title == "Building");
        }
    }
}
