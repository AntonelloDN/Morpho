using Morpho25.Settings;
using NUnit.Framework;

namespace MorphoTests.Simx
{
    internal class RadSchemeTest
    {
        public RadScheme RadScheme { get; set; }

        [SetUp]
        public void Setup()
        {
            RadScheme = new RadScheme();
        }

        [Test]
        public void RadSchemeInitTest()
        {
            Assert.IsTrue(RadScheme.MRTCalculationMethod == MRTCalculationMethod.TwoDirectional);

            RadScheme.AdvCanopyRadTransfer = Active.YES;
            RadScheme.IVSHeightAngleHighRes = 10;
            RadScheme.IVSAzimutAngleHighRes = 15;
            RadScheme.RadiationHeightBoundary = 15;
            RadScheme.MRTProjectionMethod = MRTProjectionMethod.Rayman;
            RadScheme.MRTCalculationMethod = MRTCalculationMethod.SixDirectional;

            Assert.IsTrue(RadScheme.IVSAzimutAngleLowRes == 30);
            Assert.IsTrue(RadScheme.IVSHeightAngleLowRes == 15);
            Assert.IsTrue(RadScheme.MRTProjectionMethod == MRTProjectionMethod.Rayman);
        }

        [Test]
        public void XMLTest()
        {
            RadScheme.AdvCanopyRadTransfer = Active.YES;
            RadScheme.IVSHeightAngleHighRes = 10;
            RadScheme.IVSAzimutAngleHighRes = 15;
            RadScheme.RadiationHeightBoundary = 15;
            RadScheme.MRTProjectionMethod = MRTProjectionMethod.Rayman;
            RadScheme.MRTCalculationMethod = MRTCalculationMethod.SixDirectional;

            var values = RadScheme.Values;

            Assert.IsTrue(values.Length == 11);
            Assert.IsTrue(values[3] == "30");
            Assert.IsTrue(values[10] == "2");

            var tags = RadScheme.Tags;

            Assert.IsTrue(tags.Length == 11);
            Assert.IsTrue(tags[3] == "IVSAziAngle_LoRes");

            Assert.IsTrue(RadScheme.Title == "RadScheme");
        }
    }
}
