using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Game.Entities
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    public class Entity
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

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Entity"/> class.
        /// </summary>
        public Entity(EntityEnvironment environment)
        {
            IsEnabled = true;
            IsVisible = true;

            Environment = environment;
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
    }
}
