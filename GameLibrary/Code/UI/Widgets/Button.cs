using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Faseway.GameLibrary.UI.Widgets
{
    public class Button : Widget
    {
        // Variables
        private Box _box;
        private Label _label;
        private SoundEffect _sound;

        // Properties
        public string Text 
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }

        // Constructor
        public Button(WidgetContainer container)
            : base(container)
        {
            _box = new Box(container)
            {
                Color = Color.Gray
            };
            _box.MouseUp += new System.EventHandler<Events.MouseEventArgs>(OnMouseUpHandler);

            _label = new Label(container);
        }

        // Methods
        protected override void OnLoad()
        {
            _sound = Content.Load<SoundEffect>("Audio\\FX\\Cow");
        }
        
        private void OnMouseUpHandler(object sender, Events.MouseEventArgs e)
        {
            _sound.Play();
        }

        public override void Update(float elapsed)
        {
            _box.Position = Position;
            _label.Position = Position;

            _box.Size = Size;
            _label.Size = Size;

            _box.Update(elapsed);
            _label.Update(elapsed);
        }

        protected override void OnPaint()
        {
            _box.Draw();
            _label.Draw();
        }
    }
}
