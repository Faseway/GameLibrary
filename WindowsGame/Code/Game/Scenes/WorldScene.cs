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
    public class WorldScene : Scene
    {
        // Variables
        private DebugWindow _debug;
        private Button _button;
        private Button _buttonMenu;
        private Label _debugText;

        // Properties
        protected World World { get; set; }
        protected Entity Player { get; set; }
        protected EntityController Controller { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldScene"/> class.
        /// </summary>
        public WorldScene()
            : base()
        {
        }

        // Methods
        public override void LoadContent()
        {
            #region Load UI

            _button = new Button(WidgetContainer)
            {
                Position = new Vector2(115, 5),
                Text = "Show Debug"
            };
            _button.Click += new System.EventHandler<MouseEventArgs>(ButtonClickHandler);

            _buttonMenu = new Button(WidgetContainer)
            {
                Position = new Vector2(5, 5),
                Text = "Menu"
            };
            _buttonMenu.Click += new System.EventHandler<MouseEventArgs>(ButtonMenuClickHandler);

            _debugText = new Label(WidgetContainer)
            {
                Position = new Vector2(5, 40),
                TextAlignment = UI.Base.TextAlignment.None
            };

            _debug = new DebugWindow(WidgetContainer);

            #endregion

            #region Load World

            World = new World();
            World.Camera.Max = World.Map.Size;

            Player = World.Environment.Factory.Create();
            Player.Name = "Test Player";
            Player.Transform.Position = new Vector2(125, 225);
            Player.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Objects");
            Player.Rendering.SpriteSheet.Add("Entity", new Rectangle(128, 0, 64, 64));

            World.Camera.Target = Player;

            var house = World.Environment.Factory.Create();
            house.Transform.Position = new Vector2(200, 280);
            house.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Objects");
            house.Rendering.SpriteSheet.Add("Entity", new Rectangle(320, 0, 100, 100));

            Controller = new EntityController(Player);

            #endregion

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
            Controller.Update(gameTime);

            World.Update(gameTime);

            _debugText.Text = string.Format
            (
                "Environment\nCount: {0}\n\nTest Player\nName: {1}\nPosition: {2}",
                World.Environment.EntityCount,
                Player.Name,
                Player.Transform.Position
            );

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            World.Draw(gameTime);

            base.Draw(gameTime);
        }

        // Event Handler
        private void ButtonClickHandler(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (_debug.Visible)
                {
                    button.Text = "Show Debug";
                    _debug.Hide();
                }
                else
                {
                    button.Text = "Hide Debug";
                    _debug.Show();
                }
            }
        }

        private void ButtonMenuClickHandler(object sender, MouseEventArgs e)
        {
            ChangeScene(SceneManager.GetScene<TestScene>());
        }
    }
}
