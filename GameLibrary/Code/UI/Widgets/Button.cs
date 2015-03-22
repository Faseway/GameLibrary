using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Faseway.GameLibrary.UI.Base;
using Faseway.GameLibrary.UI.Events;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class Button : Widget
    {
        // Variables
        private Label _label;
        private SoundEffect _soundEffectHover;
        private SoundEffect _soundEffectClick;
        private Texture2D _texture;

        // Properties
        public string Text 
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }

        public Color ForeColor
        {
            get { return _label.ForeColor; }
            set { _label.ForeColor = value; }
        }
        public Color ShadowColor
        {
            get { return _label.ShadowColor; }
            set { _label.ShadowColor = value; }
        }

        public SpriteFont Font
        {
            get { return _label.Font; }
            set { _label.Font = value; }
        }

        public TextAlignment TextAlignment
        {
            get { return _label.TextAlignment; }
            set { _label.TextAlignment = value; }
        }

        // Constructor
        public Button(WidgetContainer container)
            : base(container)
        {
            //_box = new Box(container)
            //{
            //    Color = Color.Gray
            //};
            //_box.MouseEnter += (s, e) => { _click.Play(); _box.Color = Color.LightGreen; };
            //_box.MouseDown += (s, e) => { _box.Color = Color.DarkSeaGreen; };
            //_box.MouseUp += (s, e) => { _sound.Play(); _box.Color = Color.LightGreen; this.OnMouseUp(e);  };
            //_box.MouseLeave += (s, e) => { _box.Color = Color.Gray; };

            _label = new Label(this);
        }

        // Methods
        protected override void OnLoad()
        {
            _soundEffectHover = Content.Load<SoundEffect>("Audio\\FX\\UI\\click2");
            _soundEffectClick = Content.Load<SoundEffect>("Audio\\FX\\UI\\click3");
            
            base.OnLoad();
        }

        protected override void OnBoundsChanged()
        {
            _label.Width = Width;
            _label.Height = Height;

            base.OnBoundsChanged();
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            _soundEffectHover.Play();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _soundEffectClick.Play();

            base.OnMouseUp(e);
        }

        protected override void OnRefresh()
        {
            switch (State)
            {
                case WidgetState.Hovered:
                    BackColor = Color.LightGreen;
                    break;
                case WidgetState.Clicked:
                    BackColor = Color.DarkSeaGreen;
                    break;
                case WidgetState.Disabled:
                    BackColor = Color.DarkGray;
                    break;
                case WidgetState.Focused:
                    BackColor = Color.LightYellow;
                    break;
                default:
                    BackColor = Color.Gray;
                    break;
            }
            _label.Position = Position;
            _label.Update(0f);
            
            base.OnRefresh();
        }

        protected override void OnPaint()
        {
            Graphics2D.SpriteBatch.Begin();
            Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), BackColor);
            Graphics2D.SpriteBatch.End();

            _label.Draw();

            base.OnPaint();
        }
    }
}
