using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Faseway.GameLibrary.Extra;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class Window : Widget
    {
        // Variables
        private bool _moved;
        private Vector2 _move;

        private Button _buttonAccept;
        private Button _buttonCancel;

        // Properties
        public Color ForeColor { get; set; }
        public SpriteFont Font { get; set; }
        public SpriteFont SecondFont { get; set; }

        public string Message { get; set; }

        public Button AcceptButton 
        {
            get { return _buttonAccept; }
        }

        // Constructor
        public Window(WidgetContainer container)
            : base(container)
        {
            Moveable = true;

            ForeColor = Color.White;
            BackColor = Color.Gray;

            Font = Content.Load<SpriteFont>("Data\\Fonts\\D10B");
            SecondFont = Content.Load<SpriteFont>("Data\\Fonts\\D10");

            Size = new Vector2(285, 115);
            Position = new Vector2((Graphics.Viewport.Width / 2) - (Width / 2), (Graphics.Viewport.Height / 2) - (Height / 2));
            
            Text = "Window";
            Message = string.Empty;

            Visible = false;
        }

        // Methods
        protected override void OnLoad()
        {
            _buttonAccept = new Button(this);
            _buttonAccept.Position = new Vector2(25, 70);
            _buttonAccept.Text = "Spiel beenden";
            _buttonAccept.Size = new Vector2(110, 22);

            _buttonCancel = new Button(this);
            _buttonCancel.Position = new Vector2(150, 70);
            _buttonCancel.Text = "Abbrechen";
            _buttonCancel.Size = new Vector2(110, 22);
            _buttonCancel.Click += (s, e) => { Hide(); };

            base.OnLoad();
        }

        protected override void OnMouseMove(Events.MouseEventArgs e)
        {
            if (e.Mouse.LeftButton == ButtonState.Pressed)
            {
                if (!_moved)
                {
                    _move = Vector2.Negate(Position - e.Position);
                    _moved = true;
                }

                Position = e.Position - _move;
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(Events.MouseEventArgs e)
        {
            _moved = false;

            base.OnMouseUp(e);
        }

        protected override void OnRefresh(Events.RefreshEventArgs e)
        {
            base.OnRefresh(e);
        }

        protected override void OnPaint(Events.PaintEventArgs e)
        {
            Graphics2D.SpriteBatch.Begin();

            Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, Position, Bounds, BackColor);
            Graphics2D.SpriteBatch.DrawString(Font, Text, (Position + new Vector2(10)).Floor(), ForeColor);

            //Graphics2D.DrawLine(Position + new Vector2(10, 28), new Vector2(265, 28), new Color(27, 27, 27, 200));

            Graphics2D.SpriteBatch.DrawString(SecondFont, Message, (Position + new Vector2(10, 31)).Floor(), ForeColor);
            
            Graphics2D.SpriteBatch.End();

            base.OnPaint(e);
        }
    }
}
