using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Content;

namespace Faseway.GameLibrary.Localization
{
    public class LocalizationManager : IComponent
    {
        // Properties
        /// <summary>
        /// Gets the current <see cref="Faseway.GameLibrary.Localization.Language"/>.
        /// </summary>
        public Language CurrentLanguage { get; private set; }
        /// <summary>
        /// Gets a <see cref="System.Collections.Generic.List"/> containing all <see cref="Faseway.GameLibrary.Localization.Language"/>s.
        /// </summary>
        public List<Language> Languages { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Localization.LocalizationManager"/> class.
        /// </summary>
        public LocalizationManager()
        {
            Languages = new List<Language>();
        }

        // Methods
        /// <summary>
        /// Changes the current <see cref="Faseway.GameLibrary.Localization.Language"/>.
        /// </summary>
        /// <param name="language">The <see cref="Faseway.GameLibrary.Localization.Language"/>.</param>
        public void ChangeLanguage(string language)
        {
            Language currentLanguage = Languages.FirstOrDefault(f => f.Code == language);
            if (currentLanguage == null)
            {
                Logger.Log("Language {0} does not exist. Could not change language.", language);
                return;
            }

            CurrentLanguage = currentLanguage;
            Logger.Log("Language changed to {0}", language);
        }

        /// <summary>
        /// Returns a localized value for the specified category and key.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="key">The identifier key.</param>
        public string Get(string category, string key)
        {
            if (CurrentLanguage == null)
            {
                throw new NullReferenceException("CurrentLanguage is empty");
            }
            else
            {
                return CurrentLanguage.GetCategory(category).GetEntry(key);
            }
        }

        /// <summary>
        /// Returns a localized format for the specified category and key and appends format parameters.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="key">The identifier key.</param>
        /// <param name="parameters">The parameters.</param>
        public string Get(string category, string key, params object[] parameters)
        {
            return string.Format(Get(category, key), parameters);
        }

        /// <summary>
        /// Adds the directory to the root directories and loads all languages inside it.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void LoadLanguages(string directory)
        {
            if (string.IsNullOrEmpty(directory))
            {
                Logger.Log("Language directory is not defined");
                return;
            }

            if (!ResourceSystem.ExistsDirectory(directory))
            {
                Logger.Log("Language directory {0} does not exist", directory);
                return;
            }

            Logger.Log("Loading languages from {0}", directory);

            foreach (string file in System.IO.Directory.GetFiles(directory))
            {
                Languages.Add(Language.Load(file));
            }

            Logger.Log("Loaded {0} languages", Languages.Count);
        }
    }
}
