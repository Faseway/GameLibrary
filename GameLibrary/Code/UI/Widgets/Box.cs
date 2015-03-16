using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class Box : Widget
    {
        // Variables
        SpriteBatch _spriteBatch;
        Texture2D _texture;

        // Properties
        public Color Color { get; set; }

        // Constructor
        public Box(WidgetContainer container)
            : base(container)
        {
            Color = Color.White;

            _spriteBatch = new SpriteBatch(Graphics);
            _texture = new Texture2D(Graphics, 1, 1);
            _texture.SetData(new Color[] { Color });
        }

        // Methods
        protected override void OnPaint()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color);
            _spriteBatch.End();
        }
    }
}
