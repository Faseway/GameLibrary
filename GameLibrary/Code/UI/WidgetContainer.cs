using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Rendering;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Base;

namespace Faseway.GameLibrary.UI
{
    /// <summary>
    /// Provides a container for user interface widgets.
    /// </summary>
    public class WidgetContainer : IWidgetContainer
    {
        // Properties
        /// <summary>
        /// Gets a collection of all widgets.
        /// </summary>
        public List<Widget> Widgets { get; set; }
        /// <summary>
        /// Gets or sets the screen position.
        /// </summary>
        public virtual Vector2 ScreenPosition { get; set; }

        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice Graphics
        {
            get { return Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager Content
        {
            get { return Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<ContentManager>(); }
        }
        /// <summary>
        /// Gets the graphics 2d.
        /// </summary>
        public Graphics2D Graphics2D 
        {
            get { return Seed.Components.GetAndRequire<Graphics2D>(); }
        }
        /// <summary>
        /// Gets the window.
        /// </summary>
        public GameWindow Window
        {
            get { return Seed.Components.GetAndRequire<Graphics2D>().Window; }
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.UI.WidgetContainer"/> class.
        /// </summary>
        public WidgetContainer()
        {
            Widgets = new List<Widget>();
            ScreenPosition = Vector2.Zero;
        }

        // Methods
        /// <summary>
        /// Returns a widget at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="Faseway.GameLibrary.UI.Widget"/> at the specified index.</returns>
        public Widget Get(int index)
        {
            return Widgets[index];
        }

        /// <summary>
        /// Returns a widget at the specified index.
        /// </summary>
        /// <typeparam name="T">The widget type.</typeparam>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="Faseway.GameLibrary.UI.Widget"/> at the specified index.</returns>
        public T Get<T>(int index) where T : Widget
        {
            return (T)Convert.ChangeType(Get(index), typeof(T));
        }

        /// <summary>
        /// Returns a widget with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The <see cref="Faseway.GameLibrary.UI.Widget"/> with the specified name.</returns>
        public Widget Get(string name)
        {
            return Widgets.Find(widget => widget.Name == name);
        }

        /// <summary>
        /// Returns a widget with the specified name.
        /// </summary>
        /// <typeparam name="T">The widget type.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The <see cref="Faseway.GameLibrary.UI.Widget"/> with the specified name.</returns>
        public T Get<T>(string name) where T : Widget
        {
            return (T)Convert.ChangeType(Get(name), typeof(T));
        }

        /// <summary>
        /// Updates all widgets.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            Widgets.ForEach(widget => widget.Update(gameTime));
        }

        /// <summary>
        /// Draws all widgets.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// </summary>
        public virtual void Draw(GameTime gameTime)
        {
            Widgets.ForEach(widget => widget.Draw(gameTime));
        }
    }
}
