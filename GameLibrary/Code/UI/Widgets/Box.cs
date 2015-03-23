using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Rendering;
using Faseway.GameLibrary.UI.Events;

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
        protected override void OnPaint(PaintEventArgs e)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(Graphics2D.Pixel, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color);
            _spriteBatch.End();
        }
    }
}
