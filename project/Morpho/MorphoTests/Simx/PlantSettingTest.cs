using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class PlantSettingTest
    {
        [Test]
        public void InitTest()
        {
            var plant = new PlantSetting();

            Assert.IsTrue(plant.CO2 == 400);

            plant.CO2 = 500;

            Assert.IsTrue(plant.CO2 == 500);

            plant.TreeCalendar = Active.NO;
            Assert.IsTrue(plant.TreeCalendar == Active.NO);
        }

        [Test]
        public void XMLTest()
        {
            var plant = new PlantSetting();
            var values = plant.Values;

            Assert.IsTrue(values.Length == 3);
            Assert.IsTrue(values[0] == "400.00000");
            Assert.IsTrue(values[1] == "1");

            var tags = plant.Tags;

            Assert.IsTrue(tags.Length == 3);
            Assert.IsTrue(tags[0] == "CO2BackgroundPPM");

            Assert.IsTrue(plant.Title == "PlantModel");
        }
    }
}
