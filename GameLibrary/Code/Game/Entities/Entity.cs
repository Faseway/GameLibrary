using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Faseway.GameLibrary.Game.Entities;
using Faseway.GameLibrary.Game.Entities.Components;
using Faseway.GameLibrary.Game.Handlers;

namespace Faseway.GameLibrary.Game.Entities
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    public class Entity : IGameHandler
    {
        // Properties
        /// <summary>
        /// Gets or sets a value indicating whether the entity is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a custom user data of the entity.
        /// </summary>
        public string UserData { get; set; }

        /// <summary>
        /// Gets the assigned environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }

        /// <summary>
        /// Gets a collection of all entity components.
        /// </summary>
        public List<EntityComponent> Components { get; private set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public TransformComponent Transform
        {
            get { return GetComponent<TransformComponent>(); }
        }
        /// <summary>
        /// Gets or sets the rendering.
        /// </summary>
        public RenderComponent Rendering
        {
            get { return GetComponent<RenderComponent>(); }
        }
        /// <summary>
        /// Gets or sets the animation.
        /// </summary>
        public AnimationComponent Animation
        {
            get { return GetComponent<AnimationComponent>(); }
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Entity"/> class.
        /// </summary>
        public Entity(EntityEnvironment environment)
        {
            IsEnabled = true;
            IsVisible = true;

            Components = new List<EntityComponent>();
            AddComponent(new TransformComponent());
            AddComponent(new RenderComponent());
            AddComponent(new AnimationComponent());

            Environment = environment;
            Environment.Add(this);
        }

        // Methods
        /// <summary>
        /// Enables the entity.
        /// </summary>
        public void Enable()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Disables the entity.
        /// </summary>
        public void Disable()
        {
            IsEnabled = false;
        }

        /// <summary>
        /// Shows the entity.
        /// </summary>
        public void Show()
        {
            IsVisible = true;
        }

        /// <summary>
        /// Hides the entity.
        /// </summary>
        public void Hide()
        {
            IsVisible = false;
        }

        #region Entity Component Implementation

        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void AddComponent(EntityComponent component)
        {
            component.AttachToEntity(this);
        }

        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void RemoveComponent(EntityComponent component)
        {
            if (Components.Contains(component))
            {
                component.DetachFromEntity();
            }
        }

        /// <summary>
        /// Gets the component of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public EntityComponent GetComponent(Type type)
        {
            return Components.FirstOrDefault(type.IsInstanceOfType);
        }

        /// <summary>
        /// Gets a specific component by a specified type.
        /// </summary>
        public T GetComponent<T>() where T : EntityComponent
        {
            return Components.FirstOrDefault(component => component is T) as T;
        }

        #endregion

        #region IGameHandler Implementation

        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                foreach (var component in Components)
                {
                    component.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            if (IsEnabled && IsVisible)
            {
                foreach (var component in Components)
                {
                    component.Draw(gameTime);
                }
            }
        }

        #endregion
    }
}
