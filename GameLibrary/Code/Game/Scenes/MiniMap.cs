using Microsoft.Xna.Framework;

using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Env;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Rendering;

namespace Faseway.GameLibrary.Game.Scenes
{
    public class MiniMap : Scene
    {
        // Variables
        private Color _backgroundColor;
        private Color _playerColor;
        private Color _neutralColor;
        private Color _enemyColor;
        private Color _transparentColor;

        // Properties
        public World World { get; private set; }
        public int Factor { get; set; }

        // Constructor
        public MiniMap(World world)
            : base()
        {
            World = world;

            Factor = 8;
        }

        // Methods
        public override void OnEnter()
        {
            
        }

        public override void OnLeave()
        {
            
        }

        public override void LoadContent()
        {
            var mapDataPath = "Content\\Maps\\" + World.Map.Name;
            if (!System.IO.File.Exists(mapDataPath + ".tga"))
            {
                Logger.Log("Loading minimap ({0}) ...", mapDataPath);
            }

            _backgroundColor = Color.DarkSlateGray;
            _playerColor = Color.Green;
            _neutralColor = Color.White;
            _enemyColor = Color.DarkRed;
            _transparentColor = Color.Transparent;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var graphics = Seed.Components.GetAndRequire<Graphics2D>();

            Vector2 offset = new Vector2(graphics.Window.ClientBounds.Width - 20 - World.Map.Bounds.Width / Factor, 20);
            Rectangle destination = new Rectangle((int)offset.X, (int)offset.Y, World.Map.Bounds.Width / Factor, World.Map.Bounds.Height / Factor);
            Rectangle bounds = new Rectangle(0, 0, 640, 640);

            //MsgBox.Show(offset);

            graphics.SpriteBatch.Begin();
            graphics.SpriteBatch.Draw(graphics.Pixel, destination, _backgroundColor);
            graphics.SpriteBatch.End();

            foreach (var entity in World.Environment)
            {
                if (entity == null) continue;
                if (!World.Map.Bounds.Contains(new Point((int)entity.Transform.Position.X, (int)entity.Transform.Position.Y))) continue;

                int x = (int)entity.Transform.Position.X / Factor;
                int y = (int)entity.Transform.Position.Y / Factor;

                Vector2 position = new Vector2(x, y) + offset;
                Color color = _transparentColor;

                if (entity.UserData == "Player") color = _playerColor;
                else color = _neutralColor;

                graphics.SpriteBatch.Begin();
                graphics.SpriteBatch.Draw(graphics.Pixel, new Rectangle((int)position.X, (int)position.Y, (int)entity.Rendering.Size.X / Factor, (int)entity.Rendering.Size.Y / Factor), color);
                graphics.SpriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
