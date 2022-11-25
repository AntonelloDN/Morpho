using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Morpho25.Utility
{
    /// <summary>
    /// Util class.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// From °C to Kelvin.
        /// </summary>
        public const double TO_KELVIN = 273.15;

        /// <summary>
        /// Accumulate list of double.
        /// </summary>
        /// <param name="sequence">List of double.</param>
        /// <returns>New list of value with partial sums.</returns>
        public static IEnumerable<double> Accumulate(IEnumerable<double> sequence)
        {
            double sum = 0;
            foreach (var item in sequence)
            {
                sum += item;
                yield return sum;
            }
        }

        /// <summary>
        /// Divide a list into chunks.
        /// </summary>
        /// <typeparam name="T">Type to use</typeparam>
        /// <param name="source">List to transform.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <returns>List of chunks.</returns>
        public static List<List<T>> ChunkBy<T>(this List<T> source, 
            int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        /// <summary>
        /// Convert a cell to number.
        /// </summary>
        /// <param name="cell">Cell to convert.</param>
        /// <returns>List of values.</returns>
        public static List<double> ConvertToNumber(string cell)
        {
            var spacing = cell.Split(',')
              .Select(v => Convert.ToDouble(v))
              .ToList();

            return spacing;
        }

        /// <summary>
        /// Return a sublist of an array using upper and lower limits.
        /// </summary>
        /// <param name="sequence">List to use.</param>
        /// <param name="upperLimit">Upper limit.</param>
        /// <param name="lowerLimit">Lower limit.</param>
        /// <returns>New list of values.</returns>
        public static IEnumerable<double> FilterByMinMax(double[] sequence, 
            double upperLimit, double lowerLimit)
        {
            List<double> values = new List<double>();

            foreach (double value in sequence)
            {
                if (value < upperLimit && value > lowerLimit)
                    values.Add(value);
            }

            return values;
        }

        /// <summary>
        /// Using an array get the index of the closest value 
        /// by a number.
        /// </summary>
        /// <param name="sequence">Array to use.</param>
        /// <param name="number">Number to use to get the 
        /// closest value.</param>
        /// <returns>Return the index of the closest value.</returns>
        public static int ClosestValue(double[] sequence, 
            double number)
        {
            double value = sequence.Aggregate((a, b) => Math.Abs(
                a - number) < Math.Abs(b - number) ? a : b);
            return Array.IndexOf(sequence, value);
        }

        /// <summary>
        /// Utility method to create envimet XML part.
        /// </summary>
        /// <param name="w">XML writer.</param>
        /// <param name="sectionTitle">Title of the section.</param>
        /// <param name="tags">Tags of the section.</param>
        /// <param name="values">Valued of the section.</param>
        /// <param name="flag">0, 1, 2 for section type.</param>
        /// <param name="attributes">Attributes of the root.</param>
        public static void CreateXmlSection(XmlTextWriter w, 
            string sectionTitle, string[] tags, string[] values, 
            int flag, string[] attributes)
        {
            w.WriteStartElement(sectionTitle);
            w.WriteString("\n ");
            foreach (var item in tags.Zip(values, (a, b) => new { A = a, B = b }))
            {
                if (flag == 0)
                {
                    w.WriteStartElement(item.A);
                    w.WriteString(item.B);
                    w.WriteEndElement();
                }
                else if (flag == 1)
                {
                    w.WriteStartElement(item.A);
                    w.WriteAttributeString("", "type", null, attributes[0]);
                    w.WriteAttributeString("", "dataI", null, attributes[1]);
                    w.WriteAttributeString("", "dataJ", null, attributes[2]);
                    w.WriteString(item.B);
                    w.WriteEndElement();
                }
                else
                {
                    w.WriteStartElement(item.A);
                    w.WriteAttributeString("", "type", null, attributes[0]);
                    w.WriteAttributeString("", "dataI", null, attributes[1]);
                    w.WriteAttributeString("", "dataJ", null, attributes[2]);
                    w.WriteAttributeString("", "zlayers", null, attributes[3]);
                    w.WriteAttributeString("", "defaultValue", null, attributes[4]);
                    w.WriteString(item.B);
                    w.WriteEndElement();
                }
                w.WriteString("\n ");

            }
            w.WriteEndElement();
            w.WriteString("\n");
        }
    }
}
