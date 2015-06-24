using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Env;
using Faseway.GameLibrary.Game.Scenes;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Rendering;
using Faseway.GameLibrary.Serialization;
using Faseway.GameLibrary.Game.Entities;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Widgets;

namespace Faseway.GameLibrary.TestGame.Game.Scenes.Comp
{
    public class PlayerStatusBar : Scene
    {
        // Variables
        private SpriteSheet _spritesheet;
        private SpriteFont _font;
        private Box _box;
        private Color _bgColor;

        // Properties
        public World World { get; private set; }
        public Entity Player { get; private set; }

        // Constructor
        public PlayerStatusBar(World world)
            : base()
        {
            World = world;
            Player = World.Environment.Get("Player");

            LoadContent();

            IsVisible = true;
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
            _spritesheet = new SpriteSheet();
            _spritesheet.Texture = Content.Load<Texture2D>("Textures\\UI\\Interface");
            new SpriteSheetSerializer().Deserialize(ResourceSystem.OpenFile("Content\\Textures\\UI\\Interface.xml"), _spritesheet);

            _spritesheet.Add("PanelBrown", new Rectangle(0, 376, 100, 100));
            _spritesheet.Add("PanelBrownTopL", new Rectangle(0, 376, 8, 8));
            _spritesheet.Add("PanelBrownTopR", new Rectangle(92, 376, 8, 8));
            _spritesheet.Add("PanelBrownBottomL", new Rectangle(0, 468, 8, 8));
            _spritesheet.Add("PanelBrownBottomR", new Rectangle(92, 468, 8, 8));
            _spritesheet.Add("PanelBrownInner", new Rectangle(15, 390, 10, 10));
            _spritesheet.Add("PanelBrownTop", new Rectangle(7, 376, 86, 8));
            _spritesheet.Add("PanelBrownTopRest", new Rectangle(7, 376, 1, 8));
            _spritesheet.Add("PanelBrownBottomRest", new Rectangle(7, 468, 1, 8));

            _font = Content.Load<SpriteFont>("Data\\Fonts\\D10");
            _bgColor = new Color(210, 178, 144);

            _box = new Box(WidgetContainer)
            {
                Size = new Vector2(270, 130)
            };
            
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var graphics = Seed.Components.GetAndRequire<Graphics2D>();
            var rect = _spritesheet.Get("panel_brown.png");
            var size = new Vector2(270, 130);
            var position = new Vector2(20, graphics.Window.ClientBounds.Height - 20 - size.Y);
            
            graphics.SpriteBatch.Begin();
            graphics.SpriteBatch.Draw(_spritesheet.Texture, position, _spritesheet.Get("PanelBrownTopL"), Color.White);
            graphics.SpriteBatch.Draw(_spritesheet.Texture, position + new Vector2(size.X - 8, 0), _spritesheet.Get("PanelBrownTopR"), Color.White);
            graphics.SpriteBatch.Draw(_spritesheet.Texture, position + new Vector2(0, size.Y - 8), _spritesheet.Get("PanelBrownBottomL"), Color.White);
            graphics.SpriteBatch.Draw(_spritesheet.Texture, position + new Vector2(size.X - 8, size.Y - 8), _spritesheet.Get("PanelBrownBottomR"), Color.White);

            //graphics.SpriteBatch.Draw(_spritesheet.Texture, position + new Vector2(8, 0), _spritesheet.Get("PanelBrownTop"), Color.White);
            graphics.SpriteBatch.Draw(_spritesheet.Texture, new Rectangle((int)position.X + 8, (int)position.Y, (int)size.X - 16, 8), _spritesheet.Get("PanelBrownTopRest"), Color.White);
            graphics.SpriteBatch.Draw(_spritesheet.Texture, new Rectangle((int)position.X + 8, (int)position.Y + (int)size.Y - 8, (int)size.X - 16, 8), _spritesheet.Get("PanelBrownBottomRest"), Color.White);
            graphics.SpriteBatch.Draw(_spritesheet.Texture, new Rectangle((int)position.X + 8, (int)position.Y + 8, (int)size.X - 16, (int)size.Y - 16), _spritesheet.Get("PanelBrownInner"), Color.White);

            graphics.SpriteBatch.DrawString(_font, "Health", position + new Vector2(15, 15), _bgColor);
            graphics.SpriteBatch.DrawString(_font, "Mana", position + new Vector2(15, 70), _bgColor);

            //graphics.SpriteBatch.Draw(_spritesheet.Texture, position, rect, Color.White);
            graphics.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
