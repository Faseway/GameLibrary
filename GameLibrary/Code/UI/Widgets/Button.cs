using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Faseway.GameLibrary.UI.Base;
using Faseway.GameLibrary.UI.Events;
using Faseway.GameLibrary.Rendering;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class Button : Widget
    {
        // Variables
        private Label _label;
        private SoundEffect _soundEffectHover;
        private SoundEffect _soundEffectClick;
        private Texture2D _texture;
        private Rectangle _textureRect;

        private TextureSheet _textures;

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
        }

        // Methods
        protected override void OnLoad()
        {
            _label = new Label(this);
            _label.ShadowEnabled = false;

            _soundEffectHover = Content.Load<SoundEffect>("Audio\\FX\\UI\\click2");
            _soundEffectClick = Content.Load<SoundEffect>("Audio\\FX\\UI\\click3");

            _textures = new TextureSheet();
            _textures.Load("Textures\\UI\\blueSheet.xml");

            Font = Content.Load<SpriteFont>("Data\\Fonts\\KenPixel");

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

        protected override void OnRefresh(RefreshEventArgs e)
        {
            switch (State)
            {
                case WidgetState.Hovered:
                    //BackColor = Color.LightGreen;
                    _textureRect = _textures.GetTextureRectangle("blue_button05.png");
                    break;
                case WidgetState.Clicked:
                    //BackColor = Color.DarkSeaGreen;
                    _textureRect = _textures.GetTextureRectangle("blue_button03.png");
                    break;
                case WidgetState.Disabled:
                    BackColor = Color.LightGray;
                    _textureRect = _textures.GetTextureRectangle("blue_button01.png");
                    break;
                case WidgetState.Focused:
                    //BackColor = Color.LightYellow;
                    break;
                default:
                    //BackColor = Color.Gray;
                    BackColor = Color.White;
                    _textureRect = _textures.GetTextureRectangle("blue_button01.png");
                    break;
            }
            _label.Position = Position;
            _label.Update(e.Time);
            
            base.OnRefresh(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);
            Graphics2D.SpriteBatch.Begin();
            Graphics2D.SpriteBatch.Draw(_textures.BaseTexture, Position, _textureRect, BackColor, 0.0f, Vector2.Zero, new Vector2(0.535f), SpriteEffects.None, 0.0f);
            //Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), BackColor);
            Graphics2D.SpriteBatch.End();

            _label.Draw(e.Time);

            base.OnPaint(e);
        }
    }
}
