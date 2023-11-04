using Morpho25.Geometry;
using Morpho25.Utility;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System;
using System.Linq;

namespace MorphoTests.Geometry
{
    public class GridTest
    {
        private readonly string _eqJson = "{\"size\":{\"cellDimension\":{\"x\":3.0,\"y\":3.0,\"z\":3.0},\"numX\":100,\"numY\":100,\"numZ\":25,\"origin\":{\"x\":0.0,\"y\":0.0,\"z\":0.0}},\"nestingGrids\":{\"firstMaterial\":\"000000\",\"secondMaterial\":\"000000\",\"numberOfCells\":3},\"telescope\":0.0,\"startTelescopeHeight\":0.0,\"combineGridType\":false}";
        private readonly string _eqErrJson = "{\"size\":{\"cellDimension\":{\"x\":3.0,\"y\":3.0,\"z\":3.0}},\"nestingGrids\":{\"firstMaterial\":\"000000\",\"secondMaterial\":\"000000\",\"numberOfCells\":3},\"telescope\":0.0,\"startTelescopeHeight\":0.0,\"combineGridType\":false}";
        private readonly string _telJson = "{\"size\":{\"cellDimension\":{\"x\":3.0,\"y\":3.0,\"z\":3.0},\"numX\":100,\"numY\":100,\"numZ\":25,\"origin\":{\"x\":0.0,\"y\":0.0,\"z\":0.0}},\"nestingGrids\":{\"firstMaterial\":\"000000\",\"secondMaterial\":\"000000\",\"numberOfCells\":3},\"telescope\":8.0,\"startTelescopeHeight\":5.0,\"combineGridType\":true}";

        private NestingGrids _nestingGrids;
        private Size _size;
        private Grid _eqGrid;
        private Grid _telGrid;

        [SetUp]
        public void Setup()
        {
            _nestingGrids = new NestingGrids(3, "000000", "000000");
            _size = new Size(new MorphoGeometry.Vector(0, 0, 0),
                new CellDimension(3, 3, 3), 100, 100, 25);
            _eqGrid = new Grid(_size, _nestingGrids);
            _telGrid = new Grid(_size, 8.0, 5.0, true, _nestingGrids);
        }

        [Test]
        public void SerializeTest()
        {
            var jsonEqOuput = _eqGrid.Serialize();
            var jsonTelOutput = _telGrid.Serialize();

            Assert.That(jsonEqOuput, Is.EqualTo(_eqJson));
            Assert.That(jsonTelOutput, Is.EqualTo(_telJson));

            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(Grid));
        }

        [Test]
        public void DeserializeTest()
        {
            var eqOut = Grid.Deserialize(_eqJson);
            Assert.That(eqOut, Is.EqualTo(_eqGrid));

            var telOut = Grid.Deserialize(_telJson);
            Assert.That(telOut, Is.EqualTo(_telGrid));

            Assert.Throws<Exception>(() => Grid.Deserialize(_eqErrJson));
        }

        [Test]
        public void CreateEquidistantGridTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_eqGrid.Xaxis.Count(), Is.EqualTo(100));
                Assert.That(_eqGrid.Yaxis.Count(), Is.EqualTo(100));
                Assert.That(_eqGrid.Zaxis.Count(), Is.EqualTo(25));
                Assert.That(_eqGrid.SequenceZ.Count(), Is.EqualTo(25));
            });
        }

        [Test]
        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        public void CreateTelescopicGridTest(bool isSplitted, bool hasNestings)
        {
            var grid = new Grid(_size, 8.0, 5.0, isSplitted, hasNestings 
                ? _nestingGrids
                : null);
            
            Assert.Multiple(() =>
            {
                Assert.That(grid.Xaxis.Count() == 100, Is.True);
                Assert.That(grid.Yaxis.Count() == 100, Is.True);
                Assert.That(grid.Zaxis.Count() == 25, Is.True);
                Assert.That(grid.SequenceZ.Count() == 25, Is.True);
            });

            if (isSplitted)
            {
                Assert.That(grid.IsSplitted, Is.True);
            }
            else 
            {
                Assert.That(grid.IsSplitted, Is.False);
            }
        }
    }
}