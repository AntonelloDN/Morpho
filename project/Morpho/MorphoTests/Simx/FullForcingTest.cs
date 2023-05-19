using Morpho25.Management;
using Morpho25.Settings;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace MorphoTests.Simx
{
    internal class FullForcingTest
    {
        private string _epw;
        private Workspace _workspace;
        private FullForcing _fullForcing;

        [SetUp]
        public void Setup()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _epw = Path.Combine(dir, "Assets/ITA_Campobasso.162520_IGDG.epw");
            _workspace = new Workspace(@"C:\Envimet\TestExample", DatabaseSource.Project);

            _fullForcing = new FullForcing(_epw, _workspace); // Create fox file and obj
        }

        [Test]
        public void InitTest()
        {
            Assert.IsNotNull(_fullForcing);

            Assert.IsTrue(_fullForcing.ForceRadClouds == Active.YES);
            Assert.IsTrue(_fullForcing.ForceWind == Active.YES);
            Assert.IsTrue(_fullForcing.ForcePrecipitation == Active.NO);

            _fullForcing.ForcePrecipitation = Active.YES;
            Assert.IsTrue(_fullForcing.ForcePrecipitation == Active.YES);
        }

        [Test]
        public void XMLMethod()
        {
            var values = _fullForcing.Values;

            Assert.IsTrue(values.Length == 13);
            Assert.IsTrue(values[6] == "linear");
            Assert.IsTrue(values[10] == "0");

            var tags = _fullForcing.Tags;

            Assert.IsTrue(tags.Length == 13);
            Assert.IsTrue(tags[3] == "forceWind");

            Assert.IsTrue(_fullForcing.Title == "FullForcing");
        }
    }
}
