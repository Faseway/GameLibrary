using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

using Microsoft.Xna.Framework;

using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Rendering;
using Faseway.GameLibrary.UI;

namespace Faseway.GameLibrary.Serialization
{
    /// <summary>
    /// Provides a serializer for seralizing sprite sheets.
    /// </summary>
    public class SpriteSheetSerializer : ISerializer
    {
        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Serialization.SpriteSheetSerializer"/> class.
        /// </summary>
        public SpriteSheetSerializer()
        {
        }

        // Methods
        public void Serialize(object instance, Stream stream)
        {
            var container = instance as SpriteSheet;
            if (container == null)
            {
                Logger.Log("Serializing {0} failed ...", instance.GetType());
            }
            else
            {
                Logger.Log("Serializing {0} ...", instance.GetType());

                var document = new XDocument();
                var root = new XElement("SpriteSheet", new XAttribute("Version", 1.0f), new XAttribute("Texture", "Textures\\UI\\Cursors.xnb"));
                var list = instance.GetType().GetField("_sprites", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance) as Dictionary<string, Rectangle>;

                foreach (var item in list)
                {
                    var element = new XElement("Sprite", new XAttribute("Name", item.Key),
                        new XAttribute("X", item.Value.X),
                        new XAttribute("Y", item.Value.Y),
                        new XAttribute("W", item.Value.Width),
                        new XAttribute("H", item.Value.Height));
                    root.Add(element);
                }
                document.Add(root);

                var text = document.ToString(SaveOptions.None);
                var buffer = Encoding.Default.GetBytes(text);
                
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                stream.Dispose();

                if (stream.GetType() == typeof(FileStream))
                {
                    Logger.Log("Serializing {0} to {1} done ...", instance.GetType(), ((FileStream)stream).Name);
                }
                else
                {
                    Logger.Log("Serializing {0} done ...", instance.GetType());
                }
            }
        }

        public object Deserialize(Type type, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
