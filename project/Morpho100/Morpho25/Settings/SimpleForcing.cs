using Morpho25.Utility;
using System;
using System.Collections.Generic;


namespace Morpho25.Settings
{
    /// <summary>
    /// Simple forcing class.
    /// </summary>
    public class SimpleForcing
    {
        /// <summary>
        /// List of temperature values to use as boundary condition (°C).
        /// </summary>
        public string Temperature { get; }
        /// <summary>
        /// List of relative humidity values to use as boundary condition (%).
        /// </summary>
        public string RelativeHumidity { get; }
        /// <summary>
        /// Number of values.
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// Create a simple forcing object.
        /// </summary>
        /// <param name="temperature">List of temperature values to use as boundary condition (°C).</param>
        /// <param name="relativeHumidity">List of relative humidity values to use as boundary condition (%)</param>
        /// <exception cref="ArgumentException"></exception>
        public SimpleForcing(List<double> temperature, List<double> relativeHumidity)
        {
            Count = temperature.Count;

            if (Count != relativeHumidity.Count)
                throw new ArgumentException("Temperature List size = Relative Humidity List size.");

            List<double> temperatureKelvin = new List<double>();
            foreach (double num in temperature)
                temperatureKelvin.Add(num + Util.TO_KELVIN);
            Temperature = String.Join(",", temperatureKelvin);
            RelativeHumidity = String.Join(",", relativeHumidity);
            Count = temperature.Count;
        }
        /// <summary>
        /// String representation of simple forcing settigns.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::SimpleForcing::count {Count}";
    }

}
