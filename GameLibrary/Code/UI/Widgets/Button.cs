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
        private SpriteSheet _sheet;

        //private Texture2D _texture;
        //private Rectangle _textureRect;
        //private SpriteSheet _spriteSheet;

        // Properties
        public new string Text 
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }

        public bool HasImage 
        {
            get { return Image != null; }
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

        public Texture2D Image 
        {
            get { return _sheet.Texture; }
            set { _sheet.Texture = value; } 
        }

        public Rectangle ImageNormal { get; set; }
        public Rectangle ImageHovered { get; set; }
        public Rectangle ImageClicked { get; set; }
        public Rectangle ImageState { get; private set; }

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
            _sheet = new SpriteSheet();

            BackColor = Color.DarkGray;
        }

        // Methods
        protected override void OnLoad()
        {
            _label = new Label(this);
            //_label.Position = Position;
            _label.ShadowEnabled = false;
            _label.TextAlignment = Base.TextAlignment.Middle;
            _label.Font = Content.Load<SpriteFont>("Data\\Fonts\\D10");
            
            _soundEffectHover = Content.Load<SoundEffect>("Audio\\FX\\UI\\click2");
            _soundEffectClick = Content.Load<SoundEffect>("Audio\\FX\\UI\\click3");

            //_spriteSheet = new SpriteSheet();
            //_spriteSheet.Texture = Content.Load<Texture2D>("Textures\\UI\\Interface");
            //_spriteSheet.Add("buttonLong_beige", new Rectangle(0, 282, 190, 49));
            //_spriteSheet.Add("buttonLong_beige_pressed", new Rectangle(0, 237, 190, 49));
            //_spriteSheet.Add("buttonLong_brown", new Rectangle(0, 49, 190, 49));
            //_spriteSheet.Add("buttonLong_brown_pressed", new Rectangle(0, 98, 190, 49));
            //_spriteSheet.Load("Textures\\UI\\blueSheet.xml");

            Size = new Vector2(100, 25);
            

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
                    BackColor = Color.LightGray;

                    if (HasImage)
                    {
                        ImageState = ImageHovered;
                    }
                    break;
                case WidgetState.Clicked:
                    BackColor = Color.DimGray;
                    break;
                case WidgetState.Disabled:
                    break;
                case WidgetState.Focused:
                    break;
                default:
                    BackColor = Color.DarkGray;

                    if (HasImage)
                    {
                        ImageState = ImageNormal;
                    }
                    break;
            }

            //Size = Font.MeasureString(Text) + new Vector2(5);

            //_label.Position = Position;
            
            base.OnRefresh(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);
            //Graphics2D.SpriteBatch.Draw(_spriteSheet.Texture, Position, _textureRect, BackColor, 0.0f, Vector2.Zero, new Vector2(0.535f), SpriteEffects.None, 0.0f);
            //Graphics2D.DrawLine(ScreenPosition, new Vector2(Width, 0), new Color(27, 27, 27, 200));
            //Graphics2D.DrawLine(ScreenPosition, new Vector2(Width, Height), new Color(27, 27, 27, 200));
            //Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), BackColor);

            Graphics2D.SpriteBatch.Begin();

            if (HasImage)
            {
                Graphics2D.SpriteBatch.Draw(Image, ScreenPosition, ImageState, Color.White);
            }
            else
            {
                Graphics2D.SpriteBatch.Draw(Graphics2D.Pixel, Bounds, BackColor);
            }

            Graphics2D.SpriteBatch.End();            

            base.OnPaint(e);
        }
    }
}
