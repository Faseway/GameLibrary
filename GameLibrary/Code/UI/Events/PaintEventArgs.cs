using System;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Faseway.GameLibrary.UI.Events
{
    public class PaintEventArgs : EventArgs
    {
        // Properties
        public GraphicsDevice Graphics { get; private set; }

        // Constructor
        public PaintEventArgs(GraphicsDevice graphics)
        {
            Graphics = graphics;
        }
    }
}
