using Faseway.GameLibrary.Game.Entities.Components;

namespace Faseway.GameLibrary.Game.Entities
{
    public class Person : Entity
    {
        // Properties
        /// <summary>
        /// Gets the health component.
        /// </summary>
        public HealthComponent Health
        {
            get { return GetComponent<HealthComponent>(); }
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Person"/> class.
        /// </summary>
        public Person(EntityEnvironment environment)
            : base(environment)
        {
            AddComponent(new HealthComponent());
        }
    }
}
