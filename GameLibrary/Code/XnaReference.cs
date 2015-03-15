using Faseway.GameLibrary.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using XnaGraphics = Microsoft.Xna.Framework.Graphics.GraphicsDevice;

namespace Faseway.GameLibrary
{
    public class XnaReference : IComponent
    {
        // Properties
        public XnaGraphics Graphics { get; set; }
        public ContentManager Content { get; set; }

        // Constructor
        public XnaReference()
        {

        }

        // Methods
        public void CreateGraphics(XnaGraphics graphics)
        {
            Graphics = graphics;
        }

        public void CreateContent(ContentManager content)
        {
            Content = content;
        }
    }
}
