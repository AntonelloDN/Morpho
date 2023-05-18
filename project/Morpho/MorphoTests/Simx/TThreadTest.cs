using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class TThreadTest
    {
        [Test]
        public void RadSchemeInitTest()
        {
            var tthread = new TThread();

            Assert.IsTrue(tthread.TThreadpriority == 4);
            
            tthread.TThreadpriority = 5;

            Assert.IsTrue(tthread.TThreadpriority == 5);

            tthread.TThreadpriority = 100;
            Assert.IsTrue(tthread.TThreadpriority == 31);
        }

        [Test]
        public void XMLTest()
        {
            var tthread = new TThread();
            tthread.UseTreading = Active.YES;

            var values = tthread.Values;

            Assert.IsTrue(values.Length == 2);
            Assert.IsTrue(values[0] == "1");
            Assert.IsTrue(values[1] == "4");

            var tags = tthread.Tags;
            
            Assert.IsTrue(tags.Length == 2);
            Assert.IsTrue(tags[0] == "UseTThread_CallMain");

            Assert.IsTrue(tthread.Title == "TThread");
        }

    }
}
