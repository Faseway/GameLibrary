using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Game.Scenes
{
    /// <summary>
    /// Provides a scene.
    /// </summary>
    public abstract class Scene : SceneContainer, IEnumerable<Scene>
    {
        // Properties
        /// <summary>
        /// Gets the parent.
        /// </summary>
        public SceneContainer Parent { get; internal set; }
        /// <summary>
        /// Gets a value indicating whether this scene is loaded.
        /// </summary>
        public bool IsLoaded { get; set; }
        /// <summary>
        /// Gets a value indicating whether this scene is loading.
        /// </summary>
        public bool IsLoading { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this scene is visible.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this scene is paused.
        /// </summary>
        public bool IsPaused { get; set; }
        /// <summary>
        /// Gets a value indicating the index during a game tick.
        /// </summary>
        public virtual int TickIndex
        {
            get { return 0; }
        }
        /// <summary>
        /// Gets a value indicating the index during the rendering process.
        /// </summary>
        public virtual int RenderIndex
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        protected SceneManager SceneManager
        {
            get { return Seed.Components.GetAndRequire<SceneManager>(); }
        }

        // Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Scenes.Scene"/> class.
        /// </summary>
        protected Scene()
        {
            IsPaused = true;
            IsVisible = false;
        }

        // Methods
        /// <summary>
        /// Transitions the scene to the specified one.
        /// </summary>
        /// <param name="scene">The scene.</param>
        protected void TransitionTo(Scene scene)
        {
            SceneManager.Add(scene);
            Remove();
            scene.IsPaused = false;
            scene.IsVisible = true;
        }

        protected void ChangeScene(Scene scene)
        {
            this.IsPaused = true;
            this.IsVisible = false;

            scene.IsPaused = false;
            scene.IsVisible = true;
        }

        /// <summary>
        /// Brings the scene to the front.
        /// </summary>
        public void BringToFront()
        {
            SceneManager.Remove(this);
            SceneManager.Add(this);
        }

        /// <summary>
        /// Removes this scene.
        /// </summary>
        public void Remove()
        {
            if (Parent != null)
            {
                Parent.Remove(this);
            }
        }

        /// <summary>
        /// Loads the content if needed.
        /// </summary>
        public void LoadContentIfNeeded()
        {
            if (IsLoaded || IsLoading) return;

            LoadContent();
        }

        /// <summary>
        /// Loads the content including textures, brushes, fonts, pens etc.
        /// </summary>
        /// <param name="loader">The content loader.</param>
        public virtual void LoadContent()
        {
#warning To-do: Add content loader.
            IsLoaded = true;
        }

        /// <summary>
        /// Called when the scene was added to a parent.
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// Called when the scene gets removed from it's parent.
        /// </summary>
        public virtual void OnLeave()
        {
        }

        #region IEnumerable<Scene> Member

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Scene> GetEnumerator()
        {
            return Scenes.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

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
