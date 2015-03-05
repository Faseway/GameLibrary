using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Faseway.GameLibrary.Game
{
    public class Score
    {
        // Properties

        // Constants
        public const string FILE_VERSION = "1.0";

        // Constructor
        public Score()
        {

        }

        // Methods
        public void Load()
        {

        }

        public void Save()
        {
            var rootElement = new XElement("score", new XAttribute("version", FILE_VERSION));

            rootElement.Add(new XElement("tutorial", new XAttribute("completed", false)));
            
            var missions = new XElement("missions");

            for (int i = 0; i < 15; i++)
            {
                var element = new XElement("mission", new XAttribute("index", i), new XAttribute("score", new Random().Next(0, 100)));
                missions.Add(element);
            }
            rootElement.Add(missions);

            var document = new XDocument(rootElement);
            document.Save("Save\\Score.xml");
        }
    }
}
