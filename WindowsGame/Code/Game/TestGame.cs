using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Scenes;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Widgets;

using Faseway.GameLibrary.TestGame.Game.Scenes;
using Faseway.GameLibrary.Rendering;
using Faseway.GameLibrary.Serialization;

namespace Faseway.GameLibrary.TestGame.Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TestGame : Microsoft.Xna.Framework.Game, IComponent
    {
        // Properties
        public GraphicsDeviceManager Graphics { get; private set; }
        public SceneManager SceneManager
        {
            get { return Seed.Components.GetAndRequire<SceneManager>(); }
        }
        public XnaReference Reference
        {
            get { return Seed.Components.GetAndRequire<XnaReference>(); }
        }
        public GameLoop GameLoop
        {
            get { return Seed.Components.GetAndRequire<GameLoop>(); }
        }
        public Cursor Cursor
        {
            get { return Seed.Components.GetAndRequire<Cursor>(); }
        }

        // Constructor
        public TestGame()
        {
            Logger.Log("Initializing TestGame ...");

            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        // Methods
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // install components
            Seed.Components.Install(new XnaReference());
            Seed.Components.Install(new SceneManager());
            Seed.Components.Install(new GameLoop());
            Seed.Components.Install(new Cursor());
            Seed.Components.Install(this);
            
            // link
            Reference.Link(Content);
            Reference.Link(GraphicsDevice);

            Seed.Components.Install(new Graphics2D(GraphicsDevice));
            Seed.Components.Get<Graphics2D>().Window = Window;

            // game loop
            GameLoop.Subscribe(Cursor);
            GameLoop.Subscribe(SceneManager);

            // install scenes
            //SceneManager.Add(new DummyScene());
            SceneManager.Add(new TestScene());
            SceneManager.Add(new WorldScene());
            //SceneManager.Add(new GooeyTestScene());
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // TODO: use this.Content to load your game content here

            Cursor.Texture = Content.Load<Texture2D>("Textures\\UI\\Cursors");

            Cursor.Add("Cursor01", new Rectangle(0, 0, 32, 32));
            Cursor.Add("Cursor01b", new Rectangle(0, 32, 32, 32));
            Cursor.Add("Cursor02", new Rectangle(32, 0, 32, 32));
            Cursor.Add("Cursor02b", new Rectangle(32, 32, 32, 32));

            Cursor.Add("Pointer01", new Rectangle(160, 0, 30, 30));
            Cursor.Add("Pointer02", new Rectangle(190, 0, 30, 30));
            Cursor.Add("Pointer03", new Rectangle(220, 0, 30, 30));

            Cursor.Add("Sword01", new Rectangle(160, 30, 34, 37));
            Cursor.Add("Sword02", new Rectangle(160, 67, 34, 37));
            Cursor.Add("Sword03", new Rectangle(160, 104, 34, 37));

            Cursor.Change("Sword01");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            // Allows the game to exit
#if XBOX
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
#else
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
#endif
            {
                Exit();
            }
            
            GameLoop.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameLoop.Draw(gameTime);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Called before the exiting the game.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/>.</param>
        protected override void OnExiting(object sender, EventArgs args)
        {
            Logger.Log("Destroying TestGame ...");

            Seed.Components.Dispose();

            base.OnExiting(sender, args);
        }
    }
}
