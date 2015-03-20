using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faseway.GameLibrary.UI.Events
{
    public class MouseEventArgs : EventArgs
    {
        // Properties
        public Vector2 Position { get; private set; }
        public MouseState Mouse { get; private set; }

        // Constructors
        public MouseEventArgs(Vector2 position, MouseState state)
        {
            Position = position;
            Mouse = state;
        }
    }
}
