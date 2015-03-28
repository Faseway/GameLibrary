using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Faseway.GameLibrary.Game.Handlers;

namespace Faseway.GameLibrary.Game.Entities
{
    public class EntityController : IUpdateHandler
    {
        // Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the current position.
        /// </summary>
        public Vector2 CurrentPosition
        {
            get { return Entity.Transform.Position; }
            set { Entity.Transform.Position = value; }
        }

        // Constructor
        public EntityController(Entity entity)
        {
            Entity = entity;
        }

        // Methods
        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            float speed = 2f;

            if (state.IsKeyDown(Keys.Left))
            {
                CurrentPosition = new Vector2(CurrentPosition.X - speed, CurrentPosition.Y);
                //this.CacheMovement(Facing.Left, WalkSpeed.Walking);
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                CurrentPosition = new Vector2(CurrentPosition.X + speed, CurrentPosition.Y);
                //this.CacheMovement(Facing.Right, WalkSpeed.Walking);
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y - speed);
                //this.CacheMovement(Facing.Up, WalkSpeed.Walking);
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + speed);
                //this.CacheMovement(Facing.Down, WalkSpeed.Walking);
            }
        }
    }
}
