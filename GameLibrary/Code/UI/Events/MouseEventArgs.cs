using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faseway.GameLibrary.UI.Events
{
    /// <summary>
    /// Provides data for the <see cref="Faseway.GameLibrary.UI.Widget.Mouse"/> events.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        // Properties
        /// <summary>
        /// Gets the position of the mouse during the generating mouse event.
        /// </summary>
        public Vector2 Position { get; private set; }
        /// <summary>
        /// Gets the state of the mouse during the generating mouse event.
        /// </summary>
        public MouseState Mouse { get; private set; }

        // Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.UI.Events.MouseEventArgs"/> class.
        /// </summary>
        /// <param name="position">The position of a mouse action.</param>
        /// <param name="state">The mouse state.</param>
        public MouseEventArgs(Vector2 position, MouseState state)
        {
            Position = position;
            Mouse = state;
        }
    }
}
