using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Scenes;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Widgets;

namespace Faseway.GameLibrary.TestGame.Game.Scenes
{
    public class TestScene : Scene
    {
        // Variables
        private WidgetContainer _container;
        private SpriteBatch _spriteBatch;
        private Texture2D _spriteTexture;
        private Song _mainTheme;

        // Methods
        public override void OnEnter()
        {
            
        }

        public override void OnLeave()
        {
            
        }

        public override void LoadContent()
        {
            _container = new WidgetContainer();

            //_mainTheme = Content.Load<Song>("Audio\\Soundtrack\\MainTheme");
            //MediaPlayer.Play(_mainTheme);
            //MediaPlayer.Volume = 0.2f;

            new Box(_container)
            {
                Position = new Vector2(Graphics.PresentationParameters.BackBufferWidth - 55, 5),
                Size = new Vector2(50),
                Color = Color.LightCoral
            };
            new Box(_container)
            {
                Position = new Vector2(Graphics.PresentationParameters.BackBufferWidth - 55, 60),
                Size = new Vector2(50),
                Color = Color.LightCoral
            };
            new Box(_container)
            {
                Position = new Vector2(Graphics.PresentationParameters.BackBufferWidth - 110, 5),
                Size = new Vector2(50),
                Color = Color.LightCoral
            };
            new Label(_container)
            {
                Position = new Vector2(5, 5)
            };

            new Button(_container)
            {
                Position = new Vector2(300, 5),
                Size = new Vector2(100, 20),
                Text = "Button"
            };

            _spriteBatch = new SpriteBatch(Graphics);
            _spriteTexture = Content.Load<Texture2D>("Textures\\Objects");

            IsLoaded = true;
        }

        public override void Update(float elapsed)
        {
            _container.Update(elapsed);

            _container.Get<Label>(3).Text = string.Format(
                "Faseway Game Library\nVersion: {0}\nBuild: {1}\nFrameIndex: {2:000000}\nFrameRate: {3}",
                Seed.Version,
                Seed.BuildDate,
                Seed.Components.GetAndRequire<GameLoop>().FrameIndex,
                Seed.Components.GetAndRequire<GameLoop>().FramesPerSecond);

            base.Update(elapsed);
        }

        public override void Draw()
        {
            _container.Draw();

            _spriteBatch.Begin();
            _spriteBatch.Draw(_spriteTexture, new Vector2(5, 100), Color.White);
            //_spriteBatch.Draw(_spriteTexture, new Vector2(5, 5), new Rectangle(0, 0, 54, 54), Color.White);
            _spriteBatch.End();

            base.Draw();
        }
    }
}
