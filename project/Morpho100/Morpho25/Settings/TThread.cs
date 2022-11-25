namespace Morpho25.Settings
{
    /// <summary>
    /// TThread class.
    /// </summary>
    public class TThread
    {
        /// <summary>
        /// Is active?
        /// </summary>
        public int UseTreading { get; }
        /// <summary>
        /// Thread priority.
        /// </summary>
        public int TThreadpriority { get; }
        /// <summary>
        /// Create a new tthread object.
        /// </summary>
        /// <param name="useTreading">Active.</param>
        public TThread(Active useTreading)
        {
            UseTreading = (int) useTreading;
            TThreadpriority = 5;
        }
        /// <summary>
        /// String representation of tthread object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::Treading {UseTreading}";
    }

}
