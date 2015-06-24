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
        public Vector2 Size 
        {
            get { return new Vector2(SpriteSheet.Get("Entity").Width, SpriteSheet.Get("Entity").Height); }
        }

        public bool TempTrigger { get; set; }

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
            var animation = Entity.GetComponent<AnimationComponent>();

            if (!EntityController.ShowDebugHelper)
            {
                graphics.DrawRectangle(new Rectangle(
                    (int)Entity.Transform.Position.X,
                    (int)Entity.Transform.Position.Y,
                    (int)Size.X,
                    (int)Size.Y),
                    TempTrigger ? Color.Red : Color.Green);
            }

            if (animation.Current == AnimationComponent.DefaultAnimationName)
            {
                graphics.DrawTexture(SpriteSheet.Texture, Entity.Transform.Position, SpriteSheet.Get("Entity"), Color.White);
            }
            else
            {
                graphics.DrawTexture(SpriteSheet.Texture, Entity.Transform.Position, SpriteSheet.Get(animation.Current), Color.White);
            }

            base.Draw(gameTime);
        }
    }
}
