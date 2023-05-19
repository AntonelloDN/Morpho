using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class ParallelCPUTest
    {
        [Test]
        public void InitTest()
        {
            var parallelCPU = new ParallelCPU();
            Assert.IsTrue(parallelCPU.CPUDemand == "ALL");
        }

        [Test]
        public void XMLTest()
        {
            var parallelCPU = new ParallelCPU();
            var values = parallelCPU.Values;

            Assert.IsTrue(values.Length == 1);
            Assert.IsTrue(values[0] == "ALL");

            var tags = parallelCPU.Tags;

            Assert.IsTrue(tags.Length == 1);
            Assert.IsTrue(tags[0] == "CPUdemand");

            Assert.IsTrue(parallelCPU.Title == "Parallel");
        }
    }
}
