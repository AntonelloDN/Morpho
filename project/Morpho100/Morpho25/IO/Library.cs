using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Morpho25.IO
{
    public class Library
    {
        public const string SOIL = "SOIL";
        public const string PROFILE = "PROFILE";
        public const string MATERIAL = "MATERIAL";
        public const string WALL = "WALL";
        public const string SOURCE = "SOURCE";
        public const string PLANT = "PLANT";
        public const string PLANT3D = "PLANT3D";
        public const string GREENING = "GREENING";

        public List<string> Code { get; private set; }
        public List<string> Description { get; private set; }
        public List<XElement> Detail { get; private set; }

        public Library(string file, string type, string keyword)
        {
            Code = new List<string>();
            Description = new List<string>();
            Detail = new List<XElement>();

            SetLibrary(file, type, keyword);
        }

        private string GetCompatibleText(string file)
        {
            string characters = @"[^\s()_<>/,\.A-Za-z0-9=""]+";
            Encoding isoLatin1 = Encoding.GetEncoding(28591);

            if (!System.IO.File.Exists(file))
                throw new Exception ($"{file} not found.");
            string text = System.IO.File.ReadAllText(file, isoLatin1);

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
            Detail = text.Select(e => e.Item3 ).ToList();
        }
    }
}
