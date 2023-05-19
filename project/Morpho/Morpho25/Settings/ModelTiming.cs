namespace Morpho25.Settings
{
    /// <summary>
    /// Model timing settings.
    /// </summary>
    public class ModelTiming : Configuration
    {
        private uint _surfaceSteps;
        private uint _flowSteps;
        private uint _radiationSteps;
        private uint _plantSteps;
        private uint _sourcesSteps;
        /// <summary>
        /// Update Surface Data each ? sec.
        /// </summary>
        public uint SurfaceSteps
        {
            get { return _surfaceSteps; }
            set
            {
                _surfaceSteps = value;
            }
        }
        /// <summary>
        /// Update Wind field each ? sec.
        /// </summary>
        public uint FlowSteps
        {
            get { return _flowSteps; }
            set
            {
                _flowSteps = value;
            }
        }
        /// <summary>
        /// Update Radiation and Shadows each ? sec.
        /// </summary>
        public uint RadiationSteps
        {
            get { return _radiationSteps; }
            set
            {
                _radiationSteps = value;
            }
        }
        /// <summary>
        /// Update Plant Data each ? sec.
        /// </summary>
        public uint PlantSteps
        {
            get { return _plantSteps; }
            set
            {
                _plantSteps = value;
            }
        }
        /// <summary>
        /// Update Emmission Data each ? sec.
        /// </summary>
        public uint SourcesSteps
        {
            get { return _sourcesSteps; }
            set
            {
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
        /// Title of the XML section
        /// </summary>
        public string Title => "ModelTiming";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            SurfaceSteps.ToString(),
            FlowSteps.ToString(),
            RadiationSteps.ToString(),
            PlantSteps.ToString(),
            SourcesSteps.ToString()
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "surfaceSteps",
            "flowSteps",
            "radiationSteps",
            "plantSteps",
            "sourcesSteps"
        };

        /// <summary>
        /// String representation of ModelTiming object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::ModelTiming";
    }

}
