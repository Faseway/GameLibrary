using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.UI.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            get { return Seed.Components.GetAndRequire<XnaReference>().Graphics; }
        }
        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager Content
        {
            get { return Seed.Components.GetAndRequire<XnaReference>().Content; }
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
            return (T)Convert.ChangeType(Widgets[index], typeof(T));
        }

        /// <summary>
        /// Updates all widgets.
        /// </summary>
        public virtual void Update(float elapsed)
        {
            Widgets.ForEach(widget => widget.Update(elapsed));
        }

        /// <summary>
        /// Draws all widgets.
        /// </summary>
        public virtual void Draw()
        {
            Widgets.ForEach(widget => widget.Draw());
        }
    }
}
