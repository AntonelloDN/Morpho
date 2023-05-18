using Morpho25.Settings;
using Morpho25.Geometry;
using Morpho25.Management;
using NUnit.Framework;
using System;

namespace MorphoTests.Simx
{
    internal class MainSettingsTest
    {
        public MainSettings MainSettings { get; set; }

        [SetUp]
        public void Setup()
        {
            var grid = new Grid(new Size());
            var workspace = new Workspace(@"C:\Envimet\TestExample", DatabaseSource.Project);
            var location = new Location(10, 10);
            var model = new Model(grid, location, workspace);

            MainSettings = new MainSettings("MySimulation", model);
        }

        [Test]
        public void MainSettingsInitTest()
        {
            Assert.IsTrue(MainSettings.Inx.Plant2dObjects.Count == 0);
            Assert.IsTrue(MainSettings.SimDuration == 24);

            MainSettings.WindAccuracy = WindAccuracy.quick;

            Assert.IsTrue(MainSettings.WindAccuracy == WindAccuracy.quick);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                MainSettings.SpecificHumidity = -23;
            });
            Assert.IsTrue("You cannot insert negative numbers" == ex.Message);
        }

        [Test]
        public void XMLMethod()
        {
            MainSettings.SimDuration = 48;
            MainSettings.InitialTemperature = 23;

            var values = MainSettings.Values;

            Assert.IsTrue(values.Length == 15);
            Assert.IsTrue(values[6] == "48");
            Assert.IsTrue(values[10] == "296.15000");

            var tags = MainSettings.Tags;

            Assert.IsTrue(tags.Length == 15);
            Assert.IsTrue(tags[3] == "outDir");

            Assert.IsTrue(MainSettings.Title == "mainData");
        }
    }
}
