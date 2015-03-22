using System.Collections.Generic;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Logging;


namespace Faseway.GameLibrary.Rendering
{
    public class SpriteSheet
    {
        // Properties
        public Dictionary<string, Sprite> Sprites { get; set; }
        public Texture2D Texture { get; set; }

        public string TexturePath { get; set; }

        // Constants
        public const string FILE_VERSION = "1.0";

        // Constructor
        public SpriteSheet()
        {
            Sprites = new Dictionary<string, Sprite>();
        }

        // Methods
        public void Load(string path)
        {
            var document = XDocument.Load(path);

            if (document.Root.Attribute("version").Value != FILE_VERSION)
            {
                Logger.Log("SpriteSheet version is invalid ({0})", path);
                return;
            }

            TexturePath = document.Root.Attribute("texture").Value;

            foreach (XElement element in document.Root.Descendants())
            {
                if (element.Name == "sprite")
                {
                    var sprite = new Sprite(
                        int.Parse(element.Attribute("x").Value), 
                        int.Parse(element.Attribute("y").Value), 
                        int.Parse(element.Attribute("with").Value),
                        int.Parse(element.Attribute("height").Value))
                        {
                            Name = element.Attribute("name").Value
                        };

                    Sprites.Add(sprite.Name, sprite);
                }
            }

            Texture = Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<ContentManager>().Load<Texture2D>(TexturePath);
        }
    }

    public class Sprite
    {
        // Properties
        public Rectangle Rectangle { get; set; }
        public int X 
        {
            get { return Rectangle.X; }
        }
        public int Y
        {
            get { return Rectangle.Y; }
        }
        public int Width
        {
            get { return Rectangle.Width; }
        }
        public int Height
        {
            get { return Rectangle.Height; }
        }
        public string Name { get; set; }

        // Constructor
        public Sprite()
            : this(0, 0, 0, 0)
        {
        }

        public Sprite(int x, int y)
            : this(x, y, 0, 0)
        {
        }

        public Sprite(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }
    }
}
