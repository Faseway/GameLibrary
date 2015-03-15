using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Rendering;
using Faseway.GameLibrary.UI.Base;

using Microsoft.Xna.Framework.Graphics;

namespace Faseway.GameLibrary.UI
{
    /// <summary>
    /// Provides a container for user interface widgets.
    /// </summary>
    public class WidgetContainer : IWidgetContainer
    {
        // Properties
        public List<Widget> Widgets { get; set; }
        public GraphicsDevice Graphics
        {
            get { return Seed.Components.GetAndRequire<RenderingFactory>().Graphics; }
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.UI.WidgetContainer"/> class.
        /// </summary>
        public WidgetContainer()
        {
            Widgets = new List<Widget>();
        }

        // Methods
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
