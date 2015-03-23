using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Logging;


namespace Faseway.GameLibrary.Rendering
{
    public class TextureSheet
    {
        // Properties
        public Dictionary<string, Rectangle> Clips { get; set; }
        public Texture2D BaseTexture { get; set; }

        public string TexturePath { get; set; }

        private ContentManager Content
        {
            get { return Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<ContentManager>(); }
        }

        // Constants
        public const string FILE_VERSION = "1.0";

        // Constructor
        public TextureSheet()
        {
            Clips = new Dictionary<string, Rectangle>();
        }

        // Methods
        public void Load(string path)
        {
            var document = XDocument.Load(Content.RootDirectory + "\\" + path);

            if (document.Root.Attribute("version").Value != FILE_VERSION)
            {
                Logger.Log("TextureSheet version is invalid ({0})", path);
                return;
            }

            TexturePath = document.Root.Attribute("texture").Value;
            //Logger.Log("TextureSheet base texture {0}", TexturePath);

            foreach (XElement element in document.Root.Descendants())
            {
                if (element.Name == "sprite")
                {
                    var x = int.Parse(element.Attribute("x").Value);
                    var y = int.Parse(element.Attribute("y").Value);
                    var width = int.Parse(element.Attribute("width").Value);
                    var height = int.Parse(element.Attribute("height").Value);

                    //Logger.Log("TextureSheet add texture clip {0}", element.Attribute("name").Value);

                    Clips.Add(element.Attribute("name").Value, new Rectangle(x, y, width, height));
                }
            }

            BaseTexture = Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<ContentManager>().Load<Texture2D>(TexturePath);
        }

        public Rectangle GetTextureRectangle(string name)
        {
            return Clips[name];
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
