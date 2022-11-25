namespace Morpho25.Settings
{
    /// <summary>
    /// IVS class.
    /// </summary>
    public class IVS
    {
        /// <summary>
        /// Use Index View Sphere (IVS) for radiation transfer.
        /// </summary>
        public int IVSOn { get; }

        /// <summary>
        /// Do you want to store the values in the memory?
        /// Storing the IVS values of every grid cell in 
        /// your computer memory makes the simulation faster, 
        /// but it require higher RAM demand.
        /// </summary>
        public int IVSMem { get; }

        /// <summary>
        /// Create new IVS.
        /// </summary>
        /// <param name="ivsOn">Index View Sphere (IVS) 
        /// for radiation transfer.</param>
        /// <param name="ivsMem">Store the values in the memory.</param>
        public IVS(Active ivsOn, 
            Active ivsMem)
        {
            IVSOn = (int)ivsOn;
            IVSMem = (int)ivsMem;
        }

        /// <summary>
        /// String representation of IVS.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::IVS";
    }

}
