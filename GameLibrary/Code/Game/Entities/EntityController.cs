using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.Game.Entities.Components;

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

        public static bool ShowDebugHelper { get; set; }

        // Constructor
        public EntityController(Entity entity)
        {
            Entity = entity;
        }

        // Methods
        public bool Collide(Vector2 delta)
        {
            var environment = Entity.Environment;
            foreach (var entity in environment)
            {
                if (entity == Entity) continue;

                var other = new Rectangle(
                    (int)entity.Transform.Position.X,
                    (int)entity.Transform.Position.Y,
                    (int)entity.Rendering.Size.X,
                    (int)entity.Rendering.Size.Y);
                var local = new Rectangle(
                    (int)delta.X,
                    (int)delta.Y,
                    (int)Entity.Rendering.Size.X,
                    (int)Entity.Rendering.Size.Y);

                if (other.Intersects(local))
                {
                    entity.Rendering.TempTrigger = true;
                    Audio.Audio2D.PlayEffect();
                    return true;
                }
                else
                {
                    entity.Rendering.TempTrigger = false;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 delta = CurrentPosition;
            float speed = 2f;

            if (state.IsKeyDown(Keys.A))
            {
                delta = new Vector2(CurrentPosition.X - speed, CurrentPosition.Y);
                Entity.GetComponent<AnimationComponent>().Play("PlayerLeftIdle");

                //this.CacheMovement(Facing.Left, WalkSpeed.Walking);
            }
            else if (state.IsKeyDown(Keys.D))
            {
                delta = new Vector2(CurrentPosition.X + speed, CurrentPosition.Y);
                Entity.GetComponent<AnimationComponent>().Play("PlayerRightIdle");

                //this.CacheMovement(Facing.Right, WalkSpeed.Walking);
            }
            else if (state.IsKeyDown(Keys.W))
            {
                delta = new Vector2(CurrentPosition.X, CurrentPosition.Y - speed);
                Entity.GetComponent<AnimationComponent>().Play("PlayerUpIdle");

                //this.CacheMovement(Facing.Up, WalkSpeed.Walking);
            }
            else if (state.IsKeyDown(Keys.S))
            {
                delta = new Vector2(CurrentPosition.X, CurrentPosition.Y + speed);
                Entity.GetComponent<AnimationComponent>().Play("PlayerDownIdle");

                //this.CacheMovement(Facing.Down, WalkSpeed.Walking);
            }

            if (!Collide(delta))
            {
                CurrentPosition = delta;
            }
        }
    }
}
