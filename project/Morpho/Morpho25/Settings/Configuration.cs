using System;


namespace Morpho25.Settings
{
    /// <summary>
    /// Configuration class.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Check if value is positive.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <exception cref="ArgumentException">Negative.</exception>
        protected void ItIsPositive(double value)
        {
            if (value < 0)
                throw new ArgumentException("You cannot insert negative numbers");
        }
        /// <summary>
        /// Check if relative humidity value is between 0% and 100%.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <exception cref="ArgumentException">Wrong value.</exception>
        protected void IsHumidityOk(double value)
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Relative humidity go from 0 to 100.");
        }
    }

    /// <summary>
    /// Active enum.
    /// </summary>
    public enum Active
    {
        NO = 0,
        YES = 1
    }
    /// <summary>
    /// Pollutant enum.
    /// </summary>
    public enum Pollutant
    {
        PM = 0,
        CO = 1,
        CO2 = 2,
        NO = 3,
        NO2 = 4,
        SO2 = 5,
        NH3 = 6,
        H2O2 = 7,
        SPRAY = 8,
    }
    /// <summary>
    /// Turbulence enum.
    /// </summary>
    public enum TurbolenceType
    {
        MellorAndYamada,
        KatoAndLaunder,
        Lopez,
        Bruse
    }

    /// <summary>
    /// Wind at facade settings enum.
    /// </summary>
    public enum FacadeMod
    {
        MO,
        DIN6946
    }

    /// <summary>
    /// Lateral Boundary Condition enum.
    /// </summary>
    public enum BoundaryCondition
    {
        Open = 1,
        Forced,
        Cyclic
    }
}
