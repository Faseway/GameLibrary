using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Game.Scenes
{
    /// <summary>
    /// Provides a scene container.
    /// </summary>
    public abstract class SceneContainer
    {
        // Properties
        /// <summary>
        /// Gets a collection of all scenes.
        /// </summary>
        protected List<Scene> Scenes { get; private set; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        protected GraphicsDevice Graphics
        {
            get { return Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the content manager.
        /// </summary>
        protected ContentManager Content
        {
            get { return Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<ContentManager>(); }
        }
        /// <summary>
        /// Gets the current state of the mouse, including mouse position and buttons pressed.
        /// </summary>
        protected MouseState Mouse
        {
            get { return Microsoft.Xna.Framework.Input.Mouse.GetState(); }
        }
        /// <summary>
        /// Gets the current keyboard state.
        /// </summary>
        protected KeyboardState Keyboard
        {
            get { return Microsoft.Xna.Framework.Input.Keyboard.GetState(); }
        }

        // Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Scenes.SceneContainer"/> class.
        /// </summary>
        protected SceneContainer()
        {
            Scenes = new List<Scene>();
        }

        // Methods
        /// <summary>
        /// Adds the specified <see cref="Faseway.GameLibrary.Game.Scenes.Scene"/>.
        /// </summary>
        /// <param name="scene">The <see cref="Faseway.GameLibrary.Game.Scenes.Scene"/>.</param>
        public void Add(Scene scene)
        {
            scene.Parent = this;
            Logger.Log("Adding scene {0} ...", scene.GetType().Name);

            scene.OnEnter();

            Scenes.Add(scene);
        }

        /// <summary>
        /// Removes the specified <see cref="Faseway.GameLibrary.Game.Scenes.Scene"/>.
        /// </summary>
        /// <param name="scene">The <see cref="Faseway.GameLibrary.Game.Scenes.Scene"/>.</param>
        public void Remove(Scene scene)
        {
            scene.Parent = null;
            Logger.Log("Removing scene {0} ...", scene.GetType().Name);

            scene.OnLeave();

            Scenes.Remove(scene);
        }

        /// <summary>
        /// Gets a scene.
        /// </summary>
        public T GetScene<T>() where T : Scene
        {
            return (T)GetScene(scene => scene is T);
        }

        /// <summary>
        /// Gets a scene.
        /// </summary>
        /// <param name="predicate">The <see cref="System.Predicate"/>.</param>
        public Scene GetScene(Predicate<Scene> predicate)
        {
            return this.Scenes.FirstOrDefault(scene => predicate(scene));
        }

        /// <summary>
        /// Orders the scenes for game tick processing.
        /// </summary>
        private IEnumerable<Scene> OrderedTickScenes()
        {
            return Scenes.Where(scene => !scene.IsPaused)
                         .OrderBy(scene => scene.TickIndex);
        }

        /// <summary>
        /// Orders the scenes for the rendering process.
        /// </summary>
        private IEnumerable<Scene> OrderedRenderScenes()
        {
            return Scenes.Where(scene => scene.IsLoaded && scene.IsVisible)
                         .OrderBy(scene => scene.RenderIndex);
        }

        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Update(float elapsed)
        {
            foreach (Scene scene in OrderedTickScenes())
            {
                scene.LoadContentIfNeeded();
                if (scene.IsLoaded)
                {
                    scene.Update(elapsed);
                }
            }
        }

        /// <summary>
        /// Handles a game render.
        /// </summary>
        public virtual void Draw()
        {
            foreach (Scene scene in OrderedRenderScenes())
            {
                scene.Draw();
            }
        }
    }
}
