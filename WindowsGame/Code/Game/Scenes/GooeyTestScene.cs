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

        private TextBox _textbox;

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

            _button = new Button(WidgetContainer)
            {
                Position = new Vector2(5, 25),
                Text = "Window"
            };
            _button.Click += (s, e) => { _window.ToggleVisibility(); };

            _textbox = new TextBox(WidgetContainer)
            {
                Position = new Vector2(300, 5)
            };

            _window = new Window(WidgetContainer)
            {
                Message = "Möchten Sie das Spiel beenden?"
            };

            _debug = new DebugWindow(WidgetContainer);

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
            base.Draw(gameTime);
        }

        // Event Handler
    }
}
