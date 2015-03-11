using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Game.Entities
{
    /// <summary>
    /// Represents an entity factory.
    /// </summary>
    public class EntityFactory
    {
        // Properties
        /// <summary>
        /// Gets the assigned environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.EntityFactory"/> class.
        /// </summary>
        /// <param name="environment"></param>
        public EntityFactory(EntityEnvironment environment)
        {
            Logger.Log("Initializing EntityFactory ...");

            Environment = environment;
        }

        // Methods
        /// <summary>
        /// Creates an entity.
        /// </summary>
        /// <returns></returns>
        public Entity Create()
        {
            return new Entity(Environment);
        }
    }
}
