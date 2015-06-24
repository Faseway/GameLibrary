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
using Faseway.GameLibrary.Serialization;
using Faseway.GameLibrary.TestGame.Game.Scenes.Comp;

namespace Faseway.GameLibrary.TestGame.Game.Scenes
{
    public class WorldScene : Scene
    {
        // Variables
        private DebugWindow _debug;
        private Button _button;
        private Button _buttonMenu;
        private Label _debugText;
        private Box _box;

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

            _box = new Box(WidgetContainer)
            {
                Size = new Vector2(100, 300),
                Color = Color.DarkGray,
                Visible = false
            };

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

            var serializer = new SpriteSheetSerializer();

            World = new World();
            World.Camera.Max = World.Map.Size;

            Player = World.Environment.Factory.CreatePerson();
            Player.Name = "Test Player";
            Player.UserData = "Player";
            Player.Transform.Position = new Vector2(125, 225);
            Player.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Player");
            Player.Rendering.SpriteSheet.Add("Entity", new Rectangle(0, 0, 64, 64));
            serializer.Deserialize(ResourceSystem.OpenFile("Content\\Textures\\Player.xml"), Player.Rendering.SpriteSheet);
            Player.Animation.Animations.Add("PlayerDownIdle");
            Player.Animation.Animations.Add("PlayerUpIdle");
            Player.Animation.Animations.Add("PlayerLeftIdle");
            Player.Animation.Animations.Add("PlayerRightIdle");

            World.Camera.Target = Player;

            var house = World.Environment.Factory.Create();
            house.Transform.Position = new Vector2(200, 280);
            house.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Objects");
            house.Rendering.SpriteSheet.Add("Entity", new Rectangle(320, 0, 100, 100));

            house = World.Environment.Factory.Create();
            house.Transform.Position = new Vector2(300, 280);
            house.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Objects");
            house.Rendering.SpriteSheet.Add("Entity", new Rectangle(320, 0, 100, 100));

            house = World.Environment.Factory.Create();
            house.Transform.Position = new Vector2(500, 200);
            house.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Objects");
            house.Rendering.SpriteSheet.Add("Entity", new Rectangle(0, 200, 260, 185));

            var tree = World.Environment.Factory.Create();
            tree.Transform.Position = new Vector2(320, 64);
            tree.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Objects");
            tree.Rendering.SpriteSheet.Add("Entity", new Rectangle(139, 0, 40, 64));

            tree = World.Environment.Factory.Create();
            tree.Transform.Position = new Vector2(352, 96);
            tree.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Objects");
            tree.Rendering.SpriteSheet.Add("Entity", new Rectangle(139, 0, 40, 64));

            tree = World.Environment.Factory.Create();
            tree.Transform.Position = new Vector2(384, 128);
            tree.Rendering.SpriteSheet.Texture = Content.Load<Texture2D>("Textures\\Objects");
            tree.Rendering.SpriteSheet.Add("Entity", new Rectangle(139, 0, 40, 64));

            Controller = new EntityController(Player);

            #endregion

            #region Load Script

            try
            {
                //var script = Seed.Components.GetAndRequire<ScriptCompiler>().GetCompiled("Freeplay").ConvertTo<MissionScript>();
                //script.Start();
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MsgBox.Show(MsgBoxIcon.Error, "World::LoadContent", "{0} - {1}", ex.Message, ex.FileName);
                Seed.Components.Get<TestGame>().Exit();
                return;
            }
            catch (System.Exception ex)
            {
                MsgBox.Show(MsgBoxIcon.Error, "World::LoadContent", "Unhandled exception of type {0} occured:\n{1}\n{2}", ex.GetType().Name, ex.Message, ex.StackTrace);
                //Seed.Components.Get<TestGame>().Exit();
                //return;
            }

#endregion

            base.Add(new MiniMap(World));
            base.Add(new PlayerStatusBar(World));

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
            if (Keyboard.IsKeyDown(Keys.Add))
            {
                World.Camera.Zoom += 0.01f;
            }
            else if (Keyboard.IsKeyDown(Keys.Subtract))
            {
                World.Camera.Zoom -= 0.01f;
            }
            if (Keyboard.IsKeyDown(Keys.NumPad8))
            {
                EntityController.ShowDebugHelper = !EntityController.ShowDebugHelper;
            }
            
            Controller.Update(gameTime);

            World.Update(gameTime);

            _debugText.Text = string.Format
            (
                "Environment\nEntityCount: {0}\n\nTest Player\nName: {1}\nPosition: {2}\nCamera Zoom: {3}",
                World.Environment.EntityCount,
                Player.Name,
                Player.Transform.Position,
                World.Camera.Zoom
            );

            if (Mouse.RightButton == ButtonState.Pressed)
            {
                var entityRect = new Rectangle((int)Controller.CurrentPosition.X, (int)Controller.CurrentPosition.Y, 64, 64);
                if (entityRect.Contains(Mouse.X, Mouse.Y))
                {
                    _box.Position = Controller.CurrentPosition;
                    _box.ToggleVisibility();
                    //MsgBox.Show("yo");
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Clear(Color.Black);

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
