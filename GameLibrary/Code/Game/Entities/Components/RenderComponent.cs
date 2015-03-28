using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Rendering;

namespace Faseway.GameLibrary.Game.Entities.Components
{
    public class RenderComponent : EntityComponent
    {
        // Properties
        public SpriteSheet SpriteSheet { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Components.RenderComponent"/> class.
        /// </summary>
        public RenderComponent()
            : base()
        {
            SpriteSheet = new SpriteSheet();
        }

        // Methods
        public override void Draw(GameTime gameTime)
        {
            var graphics = Seed.Components.GetAndRequire<Graphics2D>();
            graphics.DrawTexture(SpriteSheet.Texture, Entity.Transform.Position, SpriteSheet.Get("Entity"), Color.White);

            base.Draw(gameTime);
        }
    }
}
