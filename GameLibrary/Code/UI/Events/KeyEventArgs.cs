using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faseway.GameLibrary.UI.Events
{
    public class KeyEventArgs : EventArgs
    {
        // Properties
        public KeyboardState Keyboard { get; private set; }

        public Keys KeyCode { get; private set; }
        public KeyState KeyState { get; private set; }

        // Constructor
        public KeyEventArgs(KeyboardState keyboard, Keys keyCode, KeyState keyState)
        {
            Keyboard = keyboard;
            KeyCode = keyCode;
            KeyState = keyState;
        }
    }
}
