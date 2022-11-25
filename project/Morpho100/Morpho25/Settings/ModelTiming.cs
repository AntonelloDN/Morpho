namespace Morpho25.Settings
{
    /// <summary>
    /// Model timing settings.
    /// </summary>
    public class ModelTiming : Configuration
    {
        private int _surfaceSteps;
        private int _flowSteps;
        private int _radiationSteps;
        private int _plantSteps;
        private int _sourcesSteps;
        /// <summary>
        /// Update Surface Data each ? sec.
        /// </summary>
        public int SurfaceSteps
        {
            get { return _surfaceSteps; }
            set
            {
                ItIsPositive(value);
                _surfaceSteps = value;
            }
        }
        /// <summary>
        /// Update Wind field each ? sec.
        /// </summary>
        public int FlowSteps
        {
            get { return _flowSteps; }
            set
            {
                ItIsPositive(value);
                _flowSteps = value;
            }
        }
        /// <summary>
        /// Update Radiation and Shadows each ? sec.
        /// </summary>
        public int RadiationSteps
        {
            get { return _radiationSteps; }
            set
            {
                ItIsPositive(value);
                _radiationSteps = value;
            }
        }
        /// <summary>
        /// Update Plant Data each ? sec.
        /// </summary>
        public int PlantSteps
        {
            get { return _plantSteps; }
            set
            {
                ItIsPositive(value);
                _plantSteps = value;
            }
        }
        /// <summary>
        /// Update Emmission Data each ? sec.
        /// </summary>
        public int SourcesSteps
        {
            get { return _sourcesSteps; }
            set
            {
                ItIsPositive(value);
                _sourcesSteps = value;
            }
        }
        /// <summary>
        /// Create model timing object.
        /// </summary>
        public ModelTiming()
        {
            SurfaceSteps = 30;
            FlowSteps = 900;
            RadiationSteps = 600;
            PlantSteps = 600;
            SourcesSteps = 600;
        }
        /// <summary>
        /// String representation of ModelTiming object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::ModelTiming";
    }

}
