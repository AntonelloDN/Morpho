namespace Morpho25.Geometry
{
    /// <summary>
    /// Nesting grids class.
    /// </summary>
    public class NestingGrids
    {
        /// <summary>
        /// First material.
        /// </summary>
        public string FirstMaterial { get; private set; }
        /// <summary>
        /// Second material.
        /// </summary>
        public string SecondMaterial { get; private set; }
        /// <summary>
        /// Number of cells.
        /// </summary>
        public uint NumberOfCells { get; private set; }

        /// <summary>
        /// Create a new nesting grids object.
        /// </summary>
        public NestingGrids()
        {
            NumberOfCells = 0;
            FirstMaterial = Material.DEFAULT_SOIL;
            SecondMaterial = Material.DEFAULT_SOIL;
        }

        /// <summary>
        /// Create a new nesting grids object.
        /// </summary>
        /// <param name="numberOfCells">Number of cells.</param>
        /// <param name="firstMaterial">First material.</param>
        /// <param name="secondMaterial">Second material.</param>
        public NestingGrids(uint numberOfCells, 
            string firstMaterial,
            string secondMaterial)
        {
            NumberOfCells = numberOfCells;
            FirstMaterial = firstMaterial;
            SecondMaterial = secondMaterial;
        }
    }
}
