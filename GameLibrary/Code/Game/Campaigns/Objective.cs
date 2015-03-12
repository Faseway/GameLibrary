using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Game.Campaigns
{
    /// <summary>
    /// Represents a objective for campaigns or missions.
    /// </summary>
    public class Objective
    {
        // Properties
        /// <summary>
        /// Gets or sets a value indicating whether the objective has been accomplished.
        /// </summary>
        public bool Accomplished { get; set; }
        /// <summary>
        /// Gets the name of the objective.
        /// </summary>
        public string Name { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Campaigns.Objective"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Objective(string name)
            : this(name, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Campaigns.Objective"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="state">The state.</param>
        public Objective(string name, bool state)
        {
            Name = name;
            Accomplished = state;
        }

        // Methods
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("{{ {0}, {1} }}", Name, Accomplished ? "Accomplished" : "Not Accomplished");
        }
    }
}
