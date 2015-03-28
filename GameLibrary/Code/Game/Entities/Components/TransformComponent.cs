using Microsoft.Xna.Framework;

namespace Faseway.GameLibrary.Game.Entities.Components
{
    public class TransformComponent : EntityComponent
    {
        // Properties
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        public Vector2 Scale { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Components.TransformComponent"/> class.
        /// </summary>
        public TransformComponent()
            : base()
        {
        }
    }
}
