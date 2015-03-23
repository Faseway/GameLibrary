using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Base;
using Faseway.GameLibrary.UI.Events;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class Label : Widget
    {
        // Properties
        public bool ShadowEnabled { get; set; }

        public string Text { get; set; }

        public Color ForeColor { get; set; }
        public Color ShadowColor { get; set; }

        public SpriteFont Font { get; set; }

        public TextAlignment TextAlignment { get; set; }

        // Constructor
        public Label(WidgetContainer container)
            : base(container)
        {
            Font = Graphics2D.Font;
            
            ForeColor = Color.White;
            ShadowColor = new Color(67, 67, 74, 255);
            ShadowEnabled = true;

            TextAlignment = TextAlignment.Middle;
        }

        // Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!string.IsNullOrEmpty(Text) && Font != null)
            {
                Vector2 fontPosition = Vector2.Zero;
                Vector2 origin = Vector2.Zero;
                Vector2 position = Vector2.Zero;

                switch (TextAlignment)
                {
                    case TextAlignment.Left:
                        break;
                    case TextAlignment.Middle:
                        fontPosition = Font.MeasureString(Text) / 4f;

                        origin = new Vector2(Width / 2f, Height / 2f);
                        position = ScreenPosition + origin - fontPosition;
                        break;
                    case TextAlignment.Right:
                        break;
                    default:
                        break;
                }

                Graphics2D.SpriteBatch.Begin();

                if (ShadowEnabled)
                {
                    Graphics2D.SpriteBatch.DrawString(Font, Text, new Vector2(Position.X + 1, Position.Y + 1), ShadowColor);
                }

                Graphics2D.SpriteBatch.DrawString(Font, Text, Position, ForeColor);

                //Graphics2D.SpriteBatch.DrawString(Font, Text, Position, ForeColor, 0.0f, _spriteFont.MeasureString(Text) / 2, 1.0f, SpriteEffects.None, 0.5f);
                //Graphics2D.SpriteBatch.DrawString(Font, Text, position, ForeColor, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);

                Graphics2D.SpriteBatch.End();
            }
        }
    }
}
