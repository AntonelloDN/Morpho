using Morpho25.Geometry;
using Morpho25.Utility;
using NUnit.Framework;
using System.Linq;

namespace MorphoTests.Geometry
{
    public class GridTest
    {
        private NestingGrids _nestingGrids;

        [SetUp]
        public void Setup()
        {
            _nestingGrids = new NestingGrids(3, "000000", "000000");
        }

        [Test]
        public void CreateEquidistantGridTest()
        {
            var size = new Size(new MorphoGeometry.Vector(0, 0, 0),
                new CellDimension(3, 3, 3), 100, 100, 25);

            var grid = new Grid(size, _nestingGrids);

            Assert.IsTrue(grid.Xaxis.Count() == size.NumX);
            Assert.IsTrue(grid.Yaxis.Count() == size.NumY);
            Assert.IsTrue(grid.Zaxis.Count() == size.NumZ);
            Assert.IsTrue(grid.SequenceZ.Count() == size.NumZ);
        }

        [Test]
        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        public void CreateTelescopicGridTest(bool isSplitted, bool hasNestings)
        {
            var size = new Size(new MorphoGeometry.Vector(0, 0, 0),
                new CellDimension(3, 3, 3), 100, 100, 25);

            var grid = new Grid(size, 8.0, 5.0, isSplitted, hasNestings 
                ? _nestingGrids
                : null);

            Assert.IsTrue(grid.Xaxis.Count() == size.NumX);
            Assert.IsTrue(grid.Yaxis.Count() == size.NumY);
            Assert.IsTrue(grid.Zaxis.Count() == size.NumZ);
            Assert.IsTrue(grid.SequenceZ.Count() == size.NumZ);
            if (isSplitted)
            {
                Assert.IsTrue(grid.IsSplitted);
            }
            else { 
                Assert.IsFalse(grid.IsSplitted); 
            }
        }
    }
}