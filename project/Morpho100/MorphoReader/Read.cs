using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace MorphoReader
{
    /// <summary>
    /// Read class.
    /// </summary>
    class Read
    {
        /// <summary>
        /// Binary information.
        /// </summary>
        public Dictionary<string, string> Information { get; private set; }

        /// <summary>
        /// Create a new read object.
        /// </summary>
        /// <param name="path">Full path of the EDT file.</param>
        public Read(string path)
        {
            Information = GetDictionaryFromXml(path);
        }

        private string ReadEdxFile(string path)
        {
            string characters = @"[^\s()_<>/,\.A-Za-z0-9=""]+";
            Encoding isoLatin1 = Encoding.GetEncoding(28591); ;
            string text = System.IO.File.ReadAllText(path, isoLatin1);

            Regex.Replace(characters, "", text);

            return text.Replace("&", "")
                .Replace("<Remark for this Source Type>", "");
        }

        private static string GetValueFromXml(XDocument xml, string keyword)
        {
            var innerText = xml.Descendants(keyword)
              .Select(v => v.Value)
              .ToList()[0].ToString();

            return innerText;
        }

        private Dictionary<string, string> GetDictionaryFromXml(string path)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            XDocument xml = XDocument.Parse(ReadEdxFile(path));

            string projectName = GetValueFromXml(xml, "projectname");
            string simulationDate = GetValueFromXml(xml, "simulation_date")
                .Replace(" ", ""); ;
            string simulationTime = GetValueFromXml(xml, "simulation_time")
                .Replace(" ", ""); ;
            string locationName = GetValueFromXml(xml, "locationname");
            
            string dateContent = GetValueFromXml(xml, "data_content");
            string nameVariables = GetValueFromXml(xml, "name_variables");


            string spacingX = GetValueFromXml(xml, "spacing_x")
                .Replace(" ", "");
            string spacingY = GetValueFromXml(xml, "spacing_y")
                .Replace(" ", "");
            string spacingZ = GetValueFromXml(xml, "spacing_z")
                .Replace(" ", "");
            string nrXdata = GetValueFromXml(xml, "nr_xdata")
                .Replace(" ", "");
            string nrYdata = GetValueFromXml(xml, "nr_ydata")
                .Replace(" ", "");
            string nrZdata = GetValueFromXml(xml, "nr_zdata")
                .Replace(" ", "");

            values.Add("projectname", projectName);
            values.Add("locationname", locationName);
            values.Add("simulation_date", simulationDate);
            values.Add("simulation_time", simulationTime);

            values.Add("data_content", dateContent);

            values.Add("spacing_x", spacingX);
            values.Add("spacing_y", spacingY);
            values.Add("spacing_z", spacingZ);

            values.Add("nr_xdata", nrXdata);
            values.Add("nr_ydata", nrYdata);
            values.Add("nr_zdata", nrZdata);

            values.Add("name_variables", nameVariables);

            return values;
        }
    }
}
