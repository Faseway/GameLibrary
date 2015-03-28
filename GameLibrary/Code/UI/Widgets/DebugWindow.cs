using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Faseway.GameLibrary.UI.Base;
using Faseway.GameLibrary.UI.Events;
using Faseway.GameLibrary.Rendering;
using System.IO;
using System;
using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class DebugWindow : Widget
    {
        // Variables
        private ScrollBar _scrollbar;

        // Properties
        public StringBuilder Builder { get; set; }
        public SpriteFont Font { get; set; }

        // Constructor
        public DebugWindow(WidgetContainer container)
            : base(container)
        {
            Font = Content.Load<SpriteFont>("Data\\Fonts\\D10");
        }

        // Methods
        protected override void OnLoad()
        {
            BackColor = new Color(0, 0, 0, 200);

            Visible = false;

            Builder = new StringBuilder();

            _scrollbar = new ScrollBar(this)
            {
                Position = new Vector2(Window.ClientBounds.Width - 31, 10),
                ScrollbarHeight = Window.ClientBounds.Height - 70
            };

            base.OnLoad();
        }

        protected override void OnKeyPress(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                ToggleVisibility();
            }
            else
            {
                Builder.Append(e.KeyCode.ToString());
            }
            base.OnKeyPress(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics2D.SpriteBatch.Begin();

            var width = Window.ClientBounds.Width - 20;
            Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, new Rectangle(10, 10, width, Window.ClientBounds.Height - 70), BackColor);
            Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, new Rectangle(10, Window.ClientBounds.Height - 50, width, 40), BackColor);
            Graphics2D.SpriteBatch.DrawString(Font, Font.Wrap(Logger.CatchedLog, width), new Vector2(15, 15), Color.White);
            Graphics2D.SpriteBatch.DrawString(Font, Builder.ToString(), new Vector2(15, Window.ClientBounds.Height - 45), Color.White);
            
            Graphics2D.SpriteBatch.End();

            base.OnPaint(e);
        }
    }
}
