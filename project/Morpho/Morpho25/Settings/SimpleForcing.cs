using Morpho25.Utility;
using System;
using System.Collections.Generic;


namespace Morpho25.Settings
{
    /// <summary>
    /// Simple forcing class.
    /// </summary>
    public class SimpleForcing : Configuration
    {
        /// <summary>
        /// List of temperature values to use as boundary condition (°C).
        /// </summary>
        public IEnumerable<double> Temperature { get; }
        /// <summary>
        /// List of relative humidity values to use as boundary condition (%).
        /// </summary>
        public IEnumerable<double> RelativeHumidity { get; }

        /// <summary>
        /// Create a simple forcing object.
        /// </summary>
        /// <param name="temperature">List of temperature values to use as boundary condition (°C).</param>
        /// <param name="relativeHumidity">List of relative humidity values to use as boundary condition (%)</param>
        /// <exception cref="ArgumentException">Wrong number of values</exception>
        public SimpleForcing(
            List<double> temperature, 
            List<double> relativeHumidity)
        {
            var temperatureNum = temperature.Count;
            var relativeHumidityNum = relativeHumidity.Count;

            if (temperatureNum != relativeHumidityNum)
                throw new ArgumentException("Temperature List size = Relative Humidity List size.");

            if (temperatureNum != 24 || relativeHumidityNum != 24)
                throw new ArgumentException("Please, provide 24 values for each variable. Settings of a typical day to use for forcing.");

            var temperatureKelvin = new List<double>();
            foreach (double num in temperature) temperatureKelvin.Add(num + Util.TO_KELVIN);
            
            // Check rel humidity
            foreach (double num in relativeHumidity) IsHumidityOk(num);

            Temperature = temperatureKelvin;
            RelativeHumidity = relativeHumidity;
        }

        /// <summary>
        /// Title of the XML section
        /// </summary>
        public string Title => "SimpleForcing";

        /// <summary>
        /// Values of the XML section
        /// TODO: It will be updated soon
        /// </summary>
        public string[] Values => new[] {
            String.Join(",", Temperature),
            String.Join(",", RelativeHumidity)
        };

        /// <summary>
        /// Tags of the XML section
        /// </summary>
        public string[] Tags => new[] {
            "TAir",
            "Qrel"
        };

        /// <summary>
        /// String representation of simple forcing settigns.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"Config::SimpleForcing";
    }

}
