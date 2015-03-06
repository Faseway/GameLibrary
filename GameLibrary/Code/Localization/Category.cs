using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Localization
{
    /// <summary>
    /// Represents a category for language entries.
    /// </summary>
    public class Category
    {
        // Properties
        /// <summary>
        /// Gets the name of the <see cref="Faseway.GameLibrary.Localization.Category"/>.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets a collection of all entries.
        /// </summary>
        public Dictionary<string, string> Entries { get; private set; }

        // Constructor
        /// <summary>
        /// Initializing a new instance of the <see cref="Faseway.GameLibrary.Localization.Category"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="Faseway.GameLibrary.Localization.Category"/>.</param>
        public Category(string name)
        {
            Name = name;

            Entries = new Dictionary<string, string>();
        }

        // Methods
        /// <summary>
        /// Determines whether the category contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>true if the category contains an element with the specified key; otherwise, false.</returns>
        public bool HasEntry(string key)
        {
            return Entries.ContainsKey(key);
        }

        /// <summary>
        /// Returns an element with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>An element.</returns>
        public string GetEntry(string key)
        {
            return HasEntry(key) ? Entries[key] : string.Format("{0}.{1}", Name, key);
        }
    }
}
