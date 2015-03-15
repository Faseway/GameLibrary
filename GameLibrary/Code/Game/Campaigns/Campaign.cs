using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Extra;
using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Game.Campaigns
{
    public class Campaign
    {
        // Variables
        private readonly List<Objective> _objectives;

        // Properties
        /// <summary>
        /// Gets the total number of all objectives.
        /// </summary>
        public int ObjectiveNum { get { return _objectives.Count; } }
        /// <summary>
        /// Gets the total number of all accomplished objectives.
        /// </summary>
        public int ObjectivesAccomplishedNum { get { return _objectives.FindAll(obj => obj.Accomplished).Count; } }
        /// <summary>
        /// Gets the total number of all not accomplished objectives.
        /// </summary>
        public int ObjectivesNotAccomplishedNum { get { return _objectives.FindAll(obj => !obj.Accomplished).Count; } }

        // Events
        /// <summary>
        /// Represents the method that handles an event.
        /// </summary>
        public event EventHandler<ObjectiveChangedEventArgs> ObjectiveChanged;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Campaigns.Campaign"/> class.
        /// </summary>
        public Campaign()
        {
            _objectives = new List<Objective>();

            ObjectiveChanged += (s, e) => { Logger.Log("Objective {0} accomplished state changed to {1}", e.Name, e.Accomplished); };
        }

        // Methods
        /// <summary>
        /// Adds an objective.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="state">The state.</param>
        public void AddObjective(string name, bool state = false)
        {
            if (!HasObjective(name))
            {
                _objectives.Add(new Objective(name, state));

                Logger.Log("Objective {0} has been added", name);
            }
            else
            {
                Logger.Log("Objective {0} has already been added", name);
            }
        }

        /// <summary>
        /// Determinantes whether an objective exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>true, if the objective exists. Otherwise, false.</returns>
        public bool HasObjective(string name)
        {
            return _objectives.Find(obj => obj.Name == name) == null ? false : true;
        }

        /// <summary>
        /// Determinantes whether an objective has been accomplished.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>true, if the objective has been accomplished. Otherwise, false.</returns>
        public bool IsObjectiveAccomplished(string name)
        {
            return _objectives.Find(obj => obj.Name == name).Accomplished;
        }

        /// <summary>
        /// Changes the accomplished state of an objective.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="state">The state.</param>
        public void SetObjectiveAccomplished(string name, bool state)
        {
            var objective = _objectives.Find(obj => obj.Name == name);
            if (objective != null)
            {
                objective.Accomplished = state;
                ObjectiveChanged.SafeInvoke(this, new ObjectiveChangedEventArgs(objective));
            }
        }

        /// <summary>
        /// Returns a copy of all objectives.
        /// </summary>
        /// <returns>An array of all objectives.</returns>
        public Objective[] GetObjectives()
        {
            return _objectives.ToArray();
        }

        /// <summary>
        /// Removes an objective.
        /// </summary>
        /// <param name="name">The name.</param>
        public void RemoveObjective(string name)
        {
            _objectives.Remove(_objectives.Find(obj => obj.Name == name));

            Logger.Log("Objective {0} has been removed", name);
        }

    }
}
