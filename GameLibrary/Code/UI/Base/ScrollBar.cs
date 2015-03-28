using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Faseway.GameLibrary.Rendering;

namespace Faseway.GameLibrary.UI.Base
{
    public class ScrollBar : Widget
    {
        // Properties
        public int Scrolling { get; set; }
        public SpriteSheet Sheet { get; set; }

        //public int Height { get; set; }
        public int SliderHeight { get; set; }
        public Vector2 SliderOrigin { get; set; }

        public int ScrollbarHeight { get; set; }

        public bool DraggingSlider { get; set; }

        // Constructor
        public ScrollBar(WidgetContainer container)
            : base(container)
        {
            Sheet = new SpriteSheet();
            Sheet.Texture = Content.Load<Texture2D>("Textures\\UI\\Window");

            Sheet.Add("ScrollBarBackgroundNormal", new Rectangle(427, 27, 21, 81));
            Sheet.Add("ScrollBarBackgroundNormalUpper", new Rectangle(427, 27, 21, 30));
            Sheet.Add("ScrollBarBackgroundNormalLower", new Rectangle(427, 78, 21, 30));
            Sheet.Add("ScrollBarBackgroundPart", new Rectangle(427, 60, 21, 5));
            Sheet.Add("ScrollBarPartNormal", new Rectangle(493, 36, 17, 42));
            Sheet.Add("ScrollBarUpperNormal", new Rectangle(493, 27, 17, 5));
            Sheet.Add("ScrollBarLowerNormal", new Rectangle(493, 32, 17, 4));
        }

        // Methods
        protected override void OnRefresh(Events.RefreshEventArgs e)
        {
            var mouse = Mouse.GetState();

            Scrolling = (mouse.ScrollWheelValue / 10);
            if (Scrolling < 0)
            {
                Scrolling = 0;
            }

            base.OnRefresh(e);
        }

        protected override void OnPaint(Events.PaintEventArgs e)
        {
            Graphics2D.SpriteBatch.Begin();

            var bgNormalUpper = Sheet.Get("ScrollBarBackgroundNormalUpper");
            var bgNormalPart = Sheet.Get("ScrollBarBackgroundPart");
            var bgNormalLower = Sheet.Get("ScrollBarBackgroundNormalLower");
            var bgHeight = ScrollbarHeight - (Sheet.Get("ScrollBarBackgroundNormalUpper").Height * 2);

            var sliderUpper = Sheet.Get("ScrollBarUpperNormal");
            var sliderPart = Sheet.Get("ScrollBarPartNormal");
            var sliderLower = Sheet.Get("ScrollBarLowerNormal");

            var slideUpperRect = new Rectangle((int)Position.X + 1, Scrolling + (int)Position.Y + 23, bgNormalPart.Width - 2, sliderUpper.Height);
            var slidePartRect = new Rectangle((int)Position.X + 1, slideUpperRect.Y + slideUpperRect.Height, bgNormalPart.Width - 2, 35);
            var slideLowerRect = new Rectangle((int)Position.X + 1, slidePartRect.Y + slidePartRect.Height, bgNormalPart.Width - 2, sliderLower.Height);

            Graphics2D.SpriteBatch.Draw(Sheet.Texture, Position, bgNormalUpper, Color.White);
            Graphics2D.SpriteBatch.Draw(Sheet.Texture, new Rectangle((int)Position.X, (int)Position.Y + bgNormalUpper.Height, bgNormalPart.Width, bgHeight), bgNormalPart, Color.White);
            Graphics2D.SpriteBatch.Draw(Sheet.Texture, Position + new Vector2(0, ScrollbarHeight - bgNormalLower.Height), bgNormalLower, Color.White);

            Graphics2D.SpriteBatch.Draw(Sheet.Texture, slideUpperRect, sliderUpper, Color.White);
            Graphics2D.SpriteBatch.Draw(Sheet.Texture, slidePartRect, sliderPart, Color.White);
            Graphics2D.SpriteBatch.Draw(Sheet.Texture, slideLowerRect, sliderLower, Color.White);

            Graphics2D.SpriteBatch.DrawString(Graphics2D.Font, Scrolling.ToString(), Position - new Vector2(+50, 0), Color.Red);

            Graphics2D.SpriteBatch.End();

            base.OnPaint(e);
        }
    }
}
