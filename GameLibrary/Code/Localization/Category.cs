using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Localization
{
    public class Category
    {
        // Properties
        public string Name { get; private set; }
        public Dictionary<string, string> Entries { get; private set; }

        // Constructor
        public Category(string name)
        {
            Name = name;

            Entries = new Dictionary<string, string>();
        }

        // Methods
        public bool HasEntry(string key)
        {
            return Entries.ContainsKey(key);
        }

        public string GetEntry(string key)
        {
            return HasEntry(key) ? Entries[key] : string.Format("{0}.{1}", Name, key);
        }
    }
}
