using Microsoft.Xna.Framework;

using Faseway.GameLibrary.Game.Handlers;

namespace Faseway.GameLibrary.Game.Entities.Components
{
    public class EntityComponent : IGameHandler
    {
        // Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Components.EntityComponent"/> class.
        /// </summary>
        public EntityComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Components.EntityComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public EntityComponent(Entity entity)
        {
            Entity = entity;
        }

        // Methods
        /// <summary>
        /// Attaches the component to the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AttachToEntity(Entity entity)
        {
            Entity = entity;
            Entity.Components.Add(this);

            OnAttached(entity);
        }

        /// <summary>
        /// Detaches the component from the specified entity.
        /// </summary>
        public void DetachFromEntity()
        {
            if (Entity != null)
            {
                Entity entity = Entity;

                Entity.Components.Remove(this);
                Entity = null;

                OnDetached(entity);
            }
        }

        /// <summary>
        /// Called when the component got attached to an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void OnAttached(Entity entity)
        {
        }

        /// <summary>
        /// Called when the component got detached to an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void OnDetached(Entity entity)
        {
        }

        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
