using Faseway.GameLibrary.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XnaGraphics = Microsoft.Xna.Framework.Graphics.GraphicsDevice;

namespace Faseway.GameLibrary.Rendering
{
    public class RenderingFactory : IComponent
    {
        // Properties
        public XnaGraphics Graphics { get; set; }

        // Constructor
        public RenderingFactory()
        {

        }

        // Methods
        public void CreateGraphics(XnaGraphics graphics)
        {
            Graphics = graphics;
        }
    }
}
