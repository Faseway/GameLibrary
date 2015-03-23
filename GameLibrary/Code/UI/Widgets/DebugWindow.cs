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
        // Properties
        public StringBuilder Builder { get; set; }

        // Constructor
        public DebugWindow(WidgetContainer container)
            : base(container)
        {
        }

        // Methods
        protected override void OnLoad()
        {
            BackColor = new Color(0, 0, 0, 200);

            Visible = false;

            Builder = new StringBuilder();

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
            
            Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, new Rectangle(10, 10, Window.ClientBounds.Width - 20, Window.ClientBounds.Height - 70), BackColor);
            Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, new Rectangle(10, Window.ClientBounds.Height - 50, Window.ClientBounds.Width - 20, 40), BackColor);
            Graphics2D.SpriteBatch.DrawString(Graphics2D.Font, Logger.CatchedLog, new Vector2(15, 15), Color.White);
            Graphics2D.SpriteBatch.DrawString(Graphics2D.Font, Builder.ToString(), new Vector2(15, Window.ClientBounds.Height - 45), Color.White);
            
            Graphics2D.SpriteBatch.End();

            base.OnPaint(e);
        }
    }
}
