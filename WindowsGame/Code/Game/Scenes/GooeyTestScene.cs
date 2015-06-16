using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Campaigns;
using Faseway.GameLibrary.Game.Entities;
using Faseway.GameLibrary.Game.Env;
using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.Game.Scenes;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Scripting;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Events;
using Faseway.GameLibrary.UI.Widgets;

namespace Faseway.GameLibrary.TestGame.Game.Scenes
{
    public class GooeyTestScene : Scene
    {
        // Variables
        private DebugWindow _debug;
        private Label _label;
        private Button _button;
        private Window _window;
        private Frame _menuFrame;

        private Button _buttonConnect;
        private Button _buttonControl;
        private Button _buttonOptions;
        private Button _buttonExit;

        private TextBox _textbox;

        private Texture2D background;

        // Properties

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GooeyTestScene"/> class.
        /// </summary>
        public GooeyTestScene()
            : base()
        {
        }

        // Methods
        public override void LoadContent()
        {
            _label = new Label(WidgetContainer)
            {
                Position = new Vector2(5),
                TextAlignment = UI.Base.TextAlignment.None
            };

            //_button = new Button(WidgetContainer)
            //{
            //    Position = new Vector2(5, 25),
            //    Text = "Window"
            //};
            //_button.Click += (s, e) => { _window.ToggleVisibility(); };

            //_textbox = new TextBox(WidgetContainer)
            //{
            //    Position = new Vector2(300, 5)
            //};

            _window = new Window(WidgetContainer)
            {
                Message = "Möchten Sie das Spiel beenden?"
            };

            _menuFrame = new Frame(WidgetContainer)
            {
                Position = new Vector2(27, 382)
            };

            _buttonConnect = new Button(_menuFrame)
            {
                Image = Content.Load<Texture2D>("Textures\\UI\\Menu"),
                ImageNormal = new Rectangle(0, 0, 126, 36),
                ImageHovered = new Rectangle(126, 0, 126, 36),
                Position = new Vector2(15, 80),
                Size = new Vector2(126, 36)
            };
            _buttonControl = new Button(_menuFrame)
            {
                Image = Content.Load<Texture2D>("Textures\\UI\\Menu"),
                ImageNormal = new Rectangle(0, 181, 126, 36),
                ImageHovered = new Rectangle(126, 181, 126, 36),
                Position = new Vector2(15, 190),
                Size = new Vector2(126, 36)
            };
            _buttonOptions = new Button(_menuFrame)
            {
                Image = Content.Load<Texture2D>("Textures\\UI\\Menu"),
                ImageNormal = new Rectangle(0, 36, 126, 36),
                ImageHovered = new Rectangle(126, 36, 126, 36),
                Position = new Vector2(15, 228),
                Size = new Vector2(126, 36)
            };
            _buttonExit = new Button(_menuFrame)
            {
                Image = Content.Load<Texture2D>("Textures\\UI\\Menu"),
                ImageNormal = new Rectangle(0, 72, 126, 36),
                ImageHovered = new Rectangle(126, 72, 126, 36),
                Position = new Vector2(15, 265),
                Size = new Vector2(126, 36)
            };
            _buttonExit.Click += (s, e) => 
            {
                _window.Show();
                _window.Message = "Möchten Sie das Spiel beenden?";
                _window.AcceptButton.Click += (s2, e2) => { Seed.Components.Get<TestGame>().Exit(); };
            };

            _debug = new DebugWindow(WidgetContainer);

            background = Content.Load<Texture2D>("Textures\\Background");
            
            base.LoadContent();
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLeave()
        {
            base.OnLeave();
        }

        public override void Update(GameTime gameTime)
        {
            _label.Text = string.Format("Mouse: {{ {0} : {1} }}", Mouse.X, Mouse.Y);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            WidgetContainer.Graphics2D.SpriteBatch.Begin();
            WidgetContainer.Graphics2D.SpriteBatch.Draw(background, Graphics.Viewport.Bounds, new Rectangle(0, 0, 1024, 768), Color.White);
            WidgetContainer.Graphics2D.SpriteBatch.End();

            base.Draw(gameTime);
        }

        // Event Handler
    }
}
