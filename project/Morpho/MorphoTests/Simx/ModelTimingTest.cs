using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class ModelTimingTest
    {
        [Test]
        public void ModelTimingInitTest()
        {
            var modelTiming = new ModelTiming();
            Assert.IsTrue(modelTiming.FlowSteps == 900);

            modelTiming.SurfaceSteps = 60;
            Assert.IsTrue(modelTiming.SurfaceSteps == 60);
        }

        [Test]
        public void XMLTest()
        {
            var modelTiming = new ModelTiming();
            modelTiming.SurfaceSteps = 60;

            var values = modelTiming.Values;

            Assert.IsTrue(values.Length == 5);
            Assert.IsTrue(values[0] == "60");
            Assert.IsTrue(values[1] == "900");

            var tags = modelTiming.Tags;

            Assert.IsTrue(tags.Length == 5);
            Assert.IsTrue(tags[0] == "surfaceSteps");

            Assert.IsTrue(modelTiming.Title == "ModelTiming");
        }
    }
}
