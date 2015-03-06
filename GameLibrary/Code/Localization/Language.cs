using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Logging;
using System.Xml;

namespace Faseway.GameLibrary.Localization
{
    public class Language
    {
        // Properties
        public string Code { get; private set; }
        public string Name { get; private set; }
        public Dictionary<string, Category> Categories { get; private set; }

        // Constants
        public const string FILE_VERSION = "1.0";

        // Constructor
        public Language(string code, string name)
        {
            Code = code;
            Name = name;

            Categories = new Dictionary<string, Category>();
        }

        // Methods
        public bool HasCategory(string name)
        {
            return Categories.ContainsKey(name);
        }

        public Category GetCategory(string name)
        {
            if (HasCategory(name))
            {
                return Categories[name];
            }
            else
            {
                throw new NullReferenceException("Category " + name + " not found");
            }
        }

        public static Language Load(string path)
        {
            Logger.Log("Loading language {0} ...", path);

            try
            {
                var document = XDocument.Load(path);
                var language = new Language(string.Empty, string.Empty);

                if (document.Root.Attribute("version").Value != FILE_VERSION)
                {
                    Logger.Log("Language version is invalid ({0})", path);
                    return null;
                }

                language.Code = document.Root.Attribute("languagecode").Value;
                language.Name = document.Root.Attribute("languagename").Value;

                foreach (XElement element in document.Root.Descendants())
                {
                    if (element.Name == "category")
                    {
                        var category = new Category(element.Attribute("name").Value);
                        foreach (XElement entry in element.Descendants())
                        {
                            category.Entries.Add(entry.Attribute("name").Value, entry.Value);
                        }
                        language.Categories.Add(category.Name, category);
                    }
                }

                return language;
            }
            catch (XmlException ex)
            {
                throw ex;
                Logger.Log("Loading language {0} failed. Syntax error on line {1}, column {2}", path, ex.LineNumber, ex.LinePosition);
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
                Logger.Log("Loading language {0} failed", path);
                return null;
            }
        }
    }
}
