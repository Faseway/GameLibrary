using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Localization
{
    /// <summary>
    /// Represents a language.
    /// </summary>
    public class Language
    {
        // Properties
        /// <summary>
        /// Gets the code of the language.
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// Gets the name of the language.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets a collection of all categories.
        /// </summary>
        public Dictionary<string, Category> Categories { get; private set; }

        // Constants
        public const string FILE_VERSION = "1.0";

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Localization.Language"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="name">The name.</param>
        public Language(string code, string name)
        {
            Code = code;
            Name = name;

            Categories = new Dictionary<string, Category>();
        }

        // Methods
        /// <summary>
        /// Determines whether the language contains the specified category.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>true if the language contains an element with the specified category; otherwise, false.</returns>
        public bool HasCategory(string name)
        {
            return Categories.ContainsKey(name);
        }

        /// <summary>
        /// Returns a <see cref="Faseway.GameLibrary.Localization.Category"/> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A <see cref="Faseway.GameLibrary.Localization.Category"/> with the specified name.</returns>
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

        /// <summary>
        /// Loads a <see cref="Faseway.GameLibrary.Localization.Language"/>.
        /// </summary>
        /// <param name="path">The path of the language.</param>
        /// <returns>A new instance of the <see cref="Faseway.GameLibrary.Localization.Language"/> class.</returns>
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
