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
        public string Text { get; set; }

        // Constructor
        public Label(WidgetContainer container)
            : base(container)
        {
            Color = Color.White;

            _spriteBatch = new SpriteBatch(Graphics);
            _spriteFont = Content.Load<SpriteFont>("Data//Fonts//Default");
        }

        // Methods
        protected override void OnPaint()
        {
            if (!string.IsNullOrEmpty(Text))
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_spriteFont, Text, Position, Color);
                //_spriteBatch.DrawString(_spriteFont, Text, Position, Color, 0, _spriteFont.MeasureString(Text) / 2, 1.0f, SpriteEffects.None, 0.5f);
                _spriteBatch.End();
            }
        }
    }
}
