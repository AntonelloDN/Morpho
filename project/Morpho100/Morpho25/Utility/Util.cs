using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Morpho25.Utility
{
    public class Util
    {
        public const double TO_KELVIN = 273.15;

        public static IEnumerable<double> Accumulate(IEnumerable<double> sequence)
        {
            double sum = 0;
            foreach (var item in sequence)
            {
                sum += item;
                yield return sum;
            }
        }

        public static List<double> ConvertToNumber(string cell)
        {
            var spacing = cell.Split(',')
              .Select(v => Convert.ToDouble(v))
              .ToList();

            return spacing;
        }

        public static IEnumerable<double> FilterByMinMax(double[] sequence, double upperLimit, double lowerLimit)
        {
            List<double> values = new List<double>();

            foreach (double value in sequence)
            {
                if (value < upperLimit && value > lowerLimit)
                    values.Add(value);
            }

            return values;
        }

        public static int ClosestValue(double[] sequence, double number)
        {
            double value = sequence.Aggregate((a, b) => Math.Abs(a - number) < Math.Abs(b - number) ? a : b);
            return Array.IndexOf(sequence, value);
        }

        public static void CreateXmlSection(XmlTextWriter w, string sectionTitle, string[] tags, string[] values, int flag, string[] attributes)
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

    public struct Pixel
    {
        public int I { get; set; }
        public int J { get; set; }
        public int K { get; set; }
    }
}
