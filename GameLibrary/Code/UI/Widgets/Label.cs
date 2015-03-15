using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class Label : Widget
    {
        // Variables
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        // Properties
        public Color Color { get; set; }

        // Constructor
        public Label(WidgetContainer container)
            : base(container)
        {
            Color = Color.White;
        }

        // Methods
        protected override void OnContentLoad()
        {
            _spriteBatch = new SpriteBatch(Graphics);
            _spriteFont = Content.Load<SpriteFont>("Data//Fonts//Default");
        }

        protected override void OnPaint()
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_spriteFont, "Test", Position, Color);
            //_spriteBatch.DrawString(_spriteFont, "Test", Position, Color, 0, _spriteFont.MeasureString("Test") / 2, 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.End();
        }
    }
}
