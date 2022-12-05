using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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
        public List<XElement> Detail { get; private set; }
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
            Detail = new List<XElement>();

            SetLibrary(file, type, keyword);
        }

        private string GetCompatibleText(string file)
        {
            string characters = @"[^\s()_<>/,\.A-Za-z0-9=""]+";
            Encoding encoding = Encoding.UTF8;

            if (!System.IO.File.Exists(file))
                throw new Exception($"{file} not found.");
            string text = System.IO.File.ReadAllText(file, encoding);

            text = System.Net.WebUtility.HtmlDecode(text);
            Regex.Replace(characters, "", text);

            return text.Replace("&", "");
        }

        private void SetLibrary(string file, string type, string keyword)
        {
            string innerText = GetCompatibleText(file);

            XDocument xml = XDocument.Parse(innerText);

            string word = (type != GREENING) ? "Description" : "Name";

            var text = (keyword != null) ?
              from data in xml.Descendants(type)
              from description in data.Descendants(word)
              from id in data.Descendants("ID")
              where description.Value.ToUpper().Contains(keyword.ToUpper())
              select Tuple.Create(id.Value.ToUpper(), description.Value.ToUpper(), data) :
              from data in xml.Descendants(type)
              from description in data.Descendants(word)
              from id in data.Descendants("ID")
              select Tuple.Create(id.Value.ToUpper(), description.Value.ToUpper(), data);

            Code = text.Select(e => e.Item1.Replace(" ", "")).ToList();
            Description = text.Select(e => e.Item2).ToList();
            Detail = text.Select(e => e.Item3).ToList();
        }
    }
}
