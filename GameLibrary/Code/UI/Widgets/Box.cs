using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Faseway.GameLibrary.Rendering;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class Box : Widget
    {
        // Variables
        SpriteBatch _spriteBatch;

        // Properties
        public Color Color { get; set; }

        // Constructor
        public Box(WidgetContainer container)
            : base(container)
        {
            Color = Color.White;

            _spriteBatch = new SpriteBatch(Graphics);
        }

        // Methods
        protected override void OnPaint()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(Pixel, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color);
            _spriteBatch.End();
        }
    }
}
