using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Campaigns;
using Faseway.GameLibrary.Game.Entities;
using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Game
{
    public class Game : IDisposable
    {
        // Properties
        /// <summary>
        /// Gets the entity environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        /// <summary>
        /// Gets or sets the campaign.
        /// </summary>
        public Campaign Campaign { get; set; }

        // Constructor
        public Game()
        {
            Logger.Log("Initializing Game ...");

            Environment = new EntityEnvironment();
        }

        // Methods
        public void Run()
        {
            Logger.Log("Running Game ...");
        }

        public void Dispose()
        {
            Logger.Log("Destroying Game ...");

            Environment.Clear();
        }
    }
}
