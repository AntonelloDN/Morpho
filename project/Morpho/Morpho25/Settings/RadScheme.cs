using System;
using System.Linq;

namespace Morpho25.Settings
{
    public enum MRTCalculationMethod 
    { 
        TwoDirectional = 0,
        SixDirectional = 1,
    }

    public enum MRTProjectionMethod
    {
        Envimet = 0,
        Solweig = 1,
        Rayman = 2,
        CityComfort = 3,
    }

    /// <summary>
    /// IVS class.
    /// </summary>
    public class RadScheme
    {
        public static readonly int[] AngleCategory = new[] { 2, 5, 10, 15, 30, 45, -1 };

        private static readonly double[][] Steps = new double[][]{
            new double[] { 0.25000, 0.50000 }, 
            new double[] { 0.50000, 0.75000 }
        };

        private int _ivsHeightAngleHighRes;
        private int _ivsAzimutAngleHighRes;
        private double _heightCap;
        private Active _lowResolution;

        /// <summary>
        /// Height angle for IVS calculation
        /// </summary>
        public int IVSHeightAngleHighRes 
        {
            get 
            {
                return _ivsHeightAngleHighRes;
            }
            set
            {
                if (!AngleCategory.Contains(value))
                    throw new Exception("Value must be 2, 5, 10, 15, 30, 45");
                _ivsHeightAngleHighRes = value;
                UpdateCalculatedFields();
            }
        }

        /// <summary>
        /// Raytracing precision
        /// </summary>
        public Active LowResolution
        {
            get
            {
                return _lowResolution;
            }
            set
            {
                _lowResolution = value;
                if (_lowResolution == Active.NO)
                {
                    HighStep = Steps[0][0];
                    LowStep = Steps[0][1];
                }
                else
                {
                    HighStep = Steps[1][0];
                    LowStep = Steps[1][1];
                }
            }
        }

        /// <summary>
        /// Advance canopy radiation transfer module
        /// </summary>
        public Active AdvCanopyRadTransfer { get; set; }

        /// <summary>
        /// Update interval for View Factor calculation
        /// </summary>
        public uint ViewFactorInterval { get; set; }

        /// <summary>
        /// Raytrace step eidth high resolution
        /// </summary>
        public double HighStep { get; private set; }

        /// <summary>
        /// Raytrace step eidth low resolution
        /// </summary>
        public double LowStep { get; private set; }

        /// <summary>
        /// MRT Calculation method
        /// </summary>
        public MRTCalculationMethod MRTCalculationMethod { get; set; }

        /// <summary>
        /// Human projector factor
        /// </summary>
        public MRTProjectionMethod MRTProjectionMethod { get; set; }
        
        /// <summary>
        /// Azimut angle for IVS calculation
        /// </summary>
        public int IVSAzimutAngleHighRes
        {
            get
            {
                return _ivsAzimutAngleHighRes;
            }
            set
            {
                if (!AngleCategory.Contains(value))
                    throw new Exception("Value must be 2, 5, 10, 15, 30, 45");
                _ivsAzimutAngleHighRes = value;
                UpdateCalculatedFields();
            }
        }

        /// <summary>
        /// Height angle for IVS calculation (low resolution)
        /// </summary>
        public int IVSHeightAngleLowRes { get; private set; }

        /// <summary>
        /// Azimut angle for IVS calculation (low resolution)
        /// </summary>
        public int IVSAzimutAngleLowRes { get; private set; }
        
        /// <summary>
        /// Height cap in meters above ground below which higher precision is used
        /// </summary>
        public double RadiationHeightBoundary
        {
            get
            {
                return _heightCap;
            }
            set
            {
                _heightCap = value;
                UpdateCalculatedFields();
            }
        }
        
        private void UpdateCalculatedFields()
        {
            if (RadiationHeightBoundary < 0)
            {
                IVSHeightAngleLowRes = IVSHeightAngleHighRes;
            }
            else
            {
                var hIndex = AngleCategory.ToList().IndexOf(IVSHeightAngleHighRes);
                if (IVSHeightAngleHighRes == -1) hIndex -= 1;

                IVSHeightAngleLowRes = AngleCategory.ElementAt(hIndex + 1);
            }

            if (RadiationHeightBoundary < 0)
            {
                IVSAzimutAngleLowRes = IVSAzimutAngleHighRes;
            }
            else
            {
                var aIndex = AngleCategory.ToList().IndexOf(IVSAzimutAngleHighRes);
                if (IVSAzimutAngleHighRes == -1) aIndex -= 1;

                IVSAzimutAngleLowRes = AngleCategory.ElementAt(aIndex + 1);
            }
        }

        /// <summary>
        /// Create new radiation settings
        /// </summary>
        public RadScheme()
        {
            IVSHeightAngleHighRes = -1;
            IVSAzimutAngleHighRes = -1;
            RadiationHeightBoundary = -1;
            LowResolution = Active.NO;
            AdvCanopyRadTransfer = Active.YES;
            ViewFactorInterval = 10;
            MRTCalculationMethod = MRTCalculationMethod.TwoDirectional;
            MRTProjectionMethod = MRTProjectionMethod.Envimet;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "RadScheme";

        /// <summary>
        /// Values of the XML section
        /// </summary>
        public string[] Values => new [] {
            IVSHeightAngleHighRes.ToString(),
            IVSAzimutAngleHighRes.ToString(),
            IVSHeightAngleLowRes.ToString(),
            IVSAzimutAngleLowRes.ToString(),
            ((int)AdvCanopyRadTransfer).ToString(),
            ViewFactorInterval.ToString(),
            HighStep.ToString("n6"),
            LowStep.ToString("n6"),
            RadiationHeightBoundary.ToString("n6"),
            ((int)MRTCalculationMethod).ToString(),
            ((int)MRTProjectionMethod).ToString(),
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new [] {
            "IVSHeightAngle_HiRes",
            "IVSAziAngle_HiRes",
            "IVSHeightAngle_LoRes",
            "IVSAziAngle_LoRes",
            "AdvCanopyRadTransfer",
            "ViewFacUpdateInterval",
            "RayTraceStepWidthHighRes",
            "RayTraceStepWidthLowRes",
            "RadiationHeightBoundary",
            "MRTCalcMethod",
            "MRTProjFac",
        };

        /// <summary>
        /// String representation of IVS.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => "Config::RadScheme";
    }

}
