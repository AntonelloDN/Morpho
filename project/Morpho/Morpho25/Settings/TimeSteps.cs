namespace Morpho25.Settings
{
    /// <summary>
    /// Timesteps class.
    /// </summary>
    public class TimeSteps : Configuration
    {
        private double _sunheightStep01;
        private double _sunheightStep02;
        private double _dtStep00;
        private double _dtStep01;
        private double _dtStep02;
        /// <summary>
        /// Sun height for switching dt(0). From 0 deg to 40.00.
        /// </summary>
        public double SunheightStep01 {
            get { return _sunheightStep01; }
            set
            {
                ItIsPositive(value);
                _sunheightStep01 = value;
            }
        }
        /// <summary>
        /// Sun height for switching dt(1). From 40.00 deg to 50.00 deg.
        /// </summary>
        public double SunheightStep02
        {
            get { return _sunheightStep02; }
            set
            {
                ItIsPositive(value);
                _sunheightStep02 = value;
            }
        }
        /// <summary>
        /// Time step (s) for interval 1 dt(0).
        /// </summary>
        public double DtStep00
        {
            get { return _dtStep00; }
            set
            {
                ItIsPositive(value);
                _dtStep00 = value;
            }
        }
        /// <summary>
        /// Time step (s) for interval 1 dt(1).
        /// </summary>
        public double DtStep01
        {
            get { return _dtStep01; }
            set
            {
                ItIsPositive(value);
                _dtStep01 = value;
            }
        }
        /// <summary>
        /// Time step (s) for interval 1 dt(2).
        /// </summary>
        public double DtStep02
        {
            get { return _dtStep02; }
            set
            {
                ItIsPositive(value);
                _dtStep02 = value;
            }
        }
        /// <summary>
        /// Create time step settings.
        /// </summary>
        public TimeSteps()
        {
            SunheightStep01 = 40.00000;
            SunheightStep02 = 50.00000;
            DtStep00 = 2.00000;
            DtStep01 = 2.00000;
            DtStep02 = 1.00000;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "TimeSteps";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new[] {
            SunheightStep01.ToString("n5"),
            SunheightStep02.ToString("n5"),
            DtStep00.ToString("n5"),
            DtStep01.ToString("n5"),
            DtStep02.ToString("n5")
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "sunheight_step01",
            "sunheight_step02",
            "dt_step00",
            "dt_step01",
            "dt_step02"
        };

        /// <summary>
        /// String representation of TimeSteps object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::TimeSteps";
    }

}
