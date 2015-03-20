using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Extra;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Entities;
using Faseway.GameLibrary.Game.Handlers;

namespace Faseway.GameLibrary.Game.Env
{
    public class World : IGameHandler
    {
        // Properties
        /// <summary>
        /// Gets the entity environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        /// <summary>
        /// Gets the weather.
        /// </summary>
        public Weather Weather { get; private set; }
        /// <summary>
        /// Gets the camera.
        /// </summary>
        public Camera Camera { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Env.World"/> class.
        /// </summary>
        public World()
        {
            Environment = new EntityEnvironment();

            Weather = new Weather();

            Camera = new Camera();
        }

        // Methods
        public void Update(float elapsed)
        {
            Weather.Update(elapsed);
            Camera.Update(elapsed);
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
