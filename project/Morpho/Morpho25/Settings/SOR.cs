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
        public Active SORMode { get; set; }

        /// <summary>
        /// Create a new SOR object.
        /// </summary>
        /// <param name="mode">If you active it pressure field is calculated via 
        /// red-black-tree algorithm which allows 
        /// parallel computation of pressure field.</param>
        public SOR ()
        {
            SORMode = Active.NO;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "SOR";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            ((int)SORMode).ToString()
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "SORMode"
        };

        /// <summary>
        /// String representation of SOR object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::SOR";
    }

}
