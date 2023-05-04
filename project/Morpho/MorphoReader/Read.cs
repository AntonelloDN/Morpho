using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

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
            string characters = @"[^\s()_<>/,\.A-Za-z0-9=""\P{IsBasicLatin}\p{IsLatin-1Supplement}]+";

            if (!System.IO.File.Exists(path))
                throw new Exception($"{path} not found.");
            string text = System.IO.File.ReadAllText(path);
            string res = Regex.Replace(text, characters, "");

            return res.Replace("<Remark for this Source Type>", "");
        }

        private static string GetValueFromXml(XmlNodeList nodeList, 
            string keyword)
        {
            // It must be just 1
            foreach (XmlNode child in nodeList)
            {
                return child.SelectSingleNode(keyword).InnerText;
            }
            return null;
        }

        private Dictionary<string, string> GetDictionaryFromXml(string path)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            XmlDocument xml = new XmlDocument();

            var text = ReadEdxFile(path);
            xml.LoadXml(text);

            XmlNodeList modeldescription = xml.DocumentElement.SelectNodes("modeldescription");
            string projectName = GetValueFromXml(modeldescription, "projectname");
            string simulationDate = GetValueFromXml(modeldescription, "simulation_date")
                .Replace(" ", ""); ;
            string simulationTime = GetValueFromXml(modeldescription, "simulation_time")
                .Replace(" ", ""); ;
            string locationName = GetValueFromXml(modeldescription, "locationname");

            XmlNodeList variables = xml.DocumentElement.SelectNodes("variables");
            string nameVariables = GetValueFromXml(variables, "name_variables");


            XmlNodeList datadescription = xml.DocumentElement.SelectNodes("datadescription");
            string dateContent = GetValueFromXml(datadescription, "data_content");
            string spacingX = GetValueFromXml(datadescription, "spacing_x")
                .Replace(" ", "");
            string spacingY = GetValueFromXml(datadescription, "spacing_y")
                .Replace(" ", "");
            string spacingZ = GetValueFromXml(datadescription, "spacing_z")
                .Replace(" ", "");
            string nrXdata = GetValueFromXml(datadescription, "nr_xdata")
                .Replace(" ", "");
            string nrYdata = GetValueFromXml(datadescription, "nr_ydata")
                .Replace(" ", "");
            string nrZdata = GetValueFromXml(datadescription, "nr_zdata")
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
