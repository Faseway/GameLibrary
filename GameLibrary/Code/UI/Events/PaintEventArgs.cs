using System;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Faseway.GameLibrary.UI.Events
{
    /// <summary>
    /// Provides data for the <see cref="Faseway.GameLibrary.UI.Widget.Paint"/> event.
    /// </summary>
    public class PaintEventArgs : EventArgs
    {
        // Properties
        /// <summary>
        /// Gets the graphics used to paint.
        /// </summary>
        public GraphicsDevice Graphics { get; private set; }
        /// <summary>
        /// Gets the game time.
        /// </summary>
        public GameTime Time { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.UI.Events.PaintEventArgs"/> class.
        /// </summary>
        /// <param name="graphics">The graphics used to paint the item.</param>
        /// <param name="time">The game time.</param>
        public PaintEventArgs(GraphicsDevice graphics, GameTime time)
        {
            Graphics = graphics;
            Time = time;
        }
    }
}
