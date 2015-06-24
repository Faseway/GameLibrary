using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Game.Entities.Components
{
    public class HealthComponent : EntityComponent
    {
        // Properties
        /// <summary>
        /// Gets a value idicating whether the entity is alive.
        /// </summary>
        public bool IsDie
        {
            get { return HealthPoint <= 0; }
        }

        /// <summary>
        /// Gets or sets the health point of the entity.
        /// </summary>
        public int HealthPoint { get; private set; }
        /// <summary>
        /// Gets or sets the maximum health point of the entity.
        /// </summary>
        public int MaxHealthPoint { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Components.HealthComponent"/> class.
        /// </summary>
        public HealthComponent()
            : base()
        {
            MaxHealthPoint = 1000;
            Regen();
        }

        // Methods
        /// <summary>
        /// Regenerates the health point.
        /// </summary>
        public void Regen()
        {
            HealthPoint = MaxHealthPoint;
        }

        /// <summary>
        /// Increases the health point.
        /// </summary>
        /// <param name="factor">The factor.</param>
        public void Increase(int factor)
        {
            HealthPoint += factor;
            if (HealthPoint > MaxHealthPoint)
            {
                HealthPoint = MaxHealthPoint;
            }
        }

        /// <summary>
        /// Decreases the health point.
        /// </summary>
        /// <param name="factor">The factor.</param>
        public void Decrease(int factor)
        {
            HealthPoint -= factor;
        }
    }
}
