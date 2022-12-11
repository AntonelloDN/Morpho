using Morpho25.Geometry;
using NUnit.Framework;
using System.Linq;

namespace MorphoTests.Geometry
{
    public class Matrix2dTest
    {
        [Test]
        [TestCase("000000")]
        public void CreateEquidistantGridTest(string defValue = null)
        {
            var matrix = new Matrix2d(10, 10, defValue);
            matrix[1, 1] = "0100SD";
         
            Assert.That(matrix[1, 1], Is.EqualTo("0100SD"));
            Assert.That(matrix.GetLengthY(), Is.EqualTo(10));
            Assert.That(matrix.GetLengthX(), Is.EqualTo(10));
        }
    }
}
