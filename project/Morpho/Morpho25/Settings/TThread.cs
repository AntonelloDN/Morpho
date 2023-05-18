namespace Morpho25.Settings
{
    /// <summary>
    /// TThread class.
    /// </summary>
    public class TThread
    {
        private uint _tThreadpriority;

        /// <summary>
        /// Is active?
        /// </summary>
        public Active UseTreading { get; set; }

        /// <summary>
        /// Thread priority on Windows
        /// </summary>
        public uint TThreadpriority
        { 
            get { return _tThreadpriority; }
            set 
            {
                if (value > 31)
                    value = 31;

                _tThreadpriority = value; 
            }
        }

        /// <summary>
        /// Create a new tthread object.
        /// </summary>
        /// <param name="useTreading">Active.</param>
        public TThread()
        {
            UseTreading = Active.NO;
            TThreadpriority = 4;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "TThread";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            ((int) UseTreading).ToString(),
            TThreadpriority.ToString()
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "UseTThread_CallMain",
            "TThreadPRIO",
        };

        /// <summary>
        /// String representation of tthread object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Treading {UseTreading}";
    }

}
