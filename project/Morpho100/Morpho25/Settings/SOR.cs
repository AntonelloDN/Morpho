namespace Morpho25.Settings
{
    /// <summary>
    /// SOR class.
    /// </summary>
    public class SOR
    {
        /// <summary>
        /// If you active it pressure field is calculated via 
        /// red-black-tree algorithm which allows 
        /// parallel computation of pressure field.
        /// </summary>
        public int SORMode { get; }

        /// <summary>
        /// Create a new SOR object.
        /// </summary>
        /// <param name="mode">If you active it pressure field is calculated via 
        /// red-black-tree algorithm which allows 
        /// parallel computation of pressure field.</param>
        public SOR (Active mode)
        {
            SORMode = (int) mode;
        }

        /// <summary>
        /// String representation of SOR object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::SOR";
    }

}
