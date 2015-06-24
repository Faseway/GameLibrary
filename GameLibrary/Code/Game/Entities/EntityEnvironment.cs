using System;
using System.Collections;
using System.Collections.Generic;

using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Game.Entities
{
    /// <summary>
    /// Represents an entity environment.
    /// </summary>
    public class EntityEnvironment : IEnumerable<Entity>
    {
        // Properties
        /// <summary>
        /// Gets a collection of all entities.
        /// </summary>
        protected List<Entity> Entities { get; private set; }
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        public EntityFactory Factory { get; set; }

        /// <summary>
        /// Gets the number of entities.
        /// </summary>
        public int EntityCount { get { return Entities.Count; } }

        /// <summary>
        /// Gets the <see cref="Faseway.GameLibrary.Game.Entities.Entity"/> at the specified index.
        /// </summary>
        public Entity this[int index] { get { return Entities[index]; } }
        
        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityEnvironment"/> class.
        /// </summary>
        public EntityEnvironment()
        {
            Logger.Log("Initializing EntityEnvironment ...");

            Factory = new EntityFactory(this);
            Entities = new List<Entity>();
        }

        // Methods
        /// <summary>
        /// Adds an entity to the environment.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(Entity entity)
        {
            if (!Entities.Contains(entity))
            {
                Logger.Log("Added entity {0} to entity environment.", entity);

                Entities.Add(entity);
            }
        }

        /// <summary>
        /// Removes an entity from the environment.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Remove(Entity entity)
        {
            Logger.Log("Removed entity {0} to entity environment.", entity);

            Entities.Remove(entity);
        }

        /// <summary>
        /// Returns an entity with the specified name or user data.
        /// </summary>
        /// <param name="name">The specified name or user data.</param>
        /// <returns>An entity.</returns>
        public Entity Get(string name)
        {
            return Entities.Find(e => e.Name == name || e.UserData == name);
        }

        /// <summary>
        /// Clears the environment.
        /// </summary>
        public void Clear()
        {
            Logger.Log("Clearing {0} entities from entity environment.", Entities.Count);

            Entities.Clear();
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Entity> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
