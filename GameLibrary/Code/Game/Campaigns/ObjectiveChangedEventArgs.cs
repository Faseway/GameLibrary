using System;
using System.Text;

namespace Faseway.GameLibrary.Game.Campaigns
{
    /// <summary>
    /// Represents a class that contain event data for an <see cref="Faseway.GameLibrary.Game.Campaigns.Objective"/>.
    /// </summary>
    public class ObjectiveChangedEventArgs : EventArgs
    {
        // Properties
        /// <summary>
        /// Gets the <see cref="Faseway.GameLibrary.Game.Campaigns.Objective"/>.
        /// </summary>
        public Objective Objective { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether the objective has been accomplished.
        /// </summary>
        public bool Accomplished { get { return Objective.Accomplished; } }
        /// <summary>
        /// Gets the name of the objective.
        /// </summary>
        public string Name { get { return Objective.Name; } }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Campaigns.ObjectiveChangedEventArgs"/> class.
        /// </summary>
        /// <param name="objective">The objective.</param>
        public ObjectiveChangedEventArgs(Objective objective)
        {
            Objective = objective;
        }
    }
}
