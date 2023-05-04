using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Morpho25.IO
{
    /// <summary>
    /// Library material class.
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Soil type (for soil).
        /// </summary>
        public const string SOIL = "SOIL";
        /// <summary>
        /// Profile type (for soil).
        /// </summary>
        public const string PROFILE = "PROFILE";
        /// <summary>
        /// Material type (for buildings).
        /// </summary>
        public const string MATERIAL = "MATERIAL";
        /// <summary>
        /// Wall type (for buildings).
        /// </summary>
        public const string WALL = "WALL";
        /// <summary>
        /// Source type (for source).
        /// </summary>
        public const string SOURCE = "SOURCE";
        /// <summary>
        /// Plant type (for plant 2D).
        /// </summary>
        public const string PLANT = "PLANT";
        /// <summary>
        /// Plant 3D type (for plant 3D).
        /// </summary>
        public const string PLANT3D = "PLANT3D";
        /// <summary>
        /// Greening type (for building).
        /// </summary>
        public const string GREENING = "GREENING";
        /// <summary>
        /// Collection of material's code.
        /// </summary>
        public List<string> Code { get; private set; }
        /// <summary>
        /// Collection of material's description.
        /// </summary>
        public List<string> Description { get; private set; }
        /// <summary>
        /// Collection of material's detail.
        /// </summary>
        public List<string> Detail { get; private set; }
        /// <summary>
        /// Create a new library object.
        /// </summary>
        /// <param name="file">File path of the DB to read.</param>
        /// <param name="type">Section of DB to read.</param>
        /// <param name="keyword">Filter by keyword.</param>
        public Library(string file,
            string type, string keyword)
        {
            Code = new List<string>();
            Description = new List<string>();
            Detail = new List<string>();

            SetLibrary(file, type, keyword);
        }

        private string GetCompatibleText(string file)
        {
            string characters = @"[^\s()_<>/,\.A-Za-z0-9=""\P{IsBasicLatin}]+";

            if (!System.IO.File.Exists(file))
                throw new Exception($"{file} not found.");
            string text = System.IO.File.ReadAllText(file);
            string res = Regex.Replace(text, characters, "");

            return res;
        }

        private void SetLibrary(string file, string type, string keyword)
        {
            string innerText = GetCompatibleText(file);

            string word = (type != GREENING) ? "Description" : "Name";

            XmlDocument xmlDcoument = new XmlDocument();
            xmlDcoument.LoadXml(innerText);
            XmlNodeList data = xmlDcoument.DocumentElement.SelectNodes(type);

            var idContainer = new string[data.Count];
            var descriptionContainer = new string[data.Count];
            var dataContainer = new string[data.Count];

            Parallel.For(0, data.Count, i =>
            {
                var description = data[i].SelectSingleNode(word).InnerText;
                if (keyword != null)
                    if (!description.ToUpper()
                    .Contains(keyword?.ToUpper())) return;

                dataContainer[i] = data[i].OuterXml;
                descriptionContainer[i]= description;
                var id = data[i].SelectSingleNode("ID").InnerText;
                idContainer[i] = id.Replace(" ", "");
            });

            Code.AddRange(idContainer.Where(_ => _ != null));
            Description.AddRange(descriptionContainer.Where(_ => _ != null));
            Detail.AddRange(dataContainer.Where(_ => _ != null));
        }
    }
}
