using System;
using System.Collections.Generic;
using System.Linq;

using Faseway.GameLibrary.Components;

namespace Faseway.GameLibrary.Game.Scenes
{
    /// <summary>
    /// Provides a scene manager.
    /// </summary>
    public class SceneManager : SceneContainer, IComponent
    {
        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Scenes.SceneManager"/> class.
        /// </summary>
        public SceneManager()
            : base()
        {
        }

        // Methods
        /// <summary>
        /// Adds the specified scenes.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public void Add(params Scene[] scenes)
        {
            Add((IEnumerable<Scene>)scenes);
        }

        /// <summary>
        /// Adds the specified scenes.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public void Add(IEnumerable<Scene> scenes)
        {
            foreach (Scene scene in scenes)
            {
                base.Add(scene);
            }
        }
    }
}
