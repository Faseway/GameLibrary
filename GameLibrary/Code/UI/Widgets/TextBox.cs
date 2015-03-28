using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class TextBox : Widget
    {
        // Properties
        public SpriteFont Font { get; set; }

        // Constructor
        public TextBox(WidgetContainer container)
            : base(container)
        {
            Font = Content.Load<SpriteFont>("Data\\Fonts\\D10");

            Size = new Vector2(125, 25);

            Text = "Textbox, yo this is so cool";
        }

        // Methods
        protected override void OnPaint(Events.PaintEventArgs e)
        {
            var raster = new RasterizerState()
            { 
                ScissorTestEnable = true
            };

            Graphics2D.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, raster);

            Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, Bounds, Color.Black);
            Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, new Rectangle(Bounds.X + 1, Bounds.Y + 1, Bounds.Width - 2, Bounds.Height - 2), Color.Gray);

            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = Graphics2D.SpriteBatch.GraphicsDevice.ScissorRectangle;

            //Set the current scissor rectangle
            Graphics2D.SpriteBatch.GraphicsDevice.ScissorRectangle = Bounds;

            //Draw the text at the top left of the scissor rectangle
            Graphics2D.SpriteBatch.DrawString(Font, string.IsNullOrEmpty(Text) ? string.Empty : Text, Position, Color.White);

            //Reset scissor rectangle to the saved value
            Graphics2D.SpriteBatch.GraphicsDevice.ScissorRectangle = currentRect;

            Graphics2D.SpriteBatch.End();

            base.OnPaint(e);
        }
    }
}
