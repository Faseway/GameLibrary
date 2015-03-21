using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faseway.GameLibrary.UI.Events
{
    /// <summary>
    /// Provides data for the <see cref="Faseway.GameLibrary.UI.Widget.KeyPress"/> event.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        // Properties
        /// <summary>
        /// Gets the keyboard state.
        /// </summary>
        public KeyboardState Keyboard { get; private set; }

        /// <summary>
        /// Gets the key code.
        /// </summary>
        public Keys KeyCode { get; private set; }
        /// <summary>
        /// Gets the key state.
        /// </summary>
        public KeyState KeyState { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.UI.Events.KeyEventArgs"/> class.
        /// </summary>
        /// <param name="keyboard">The keyboard state.</param>
        /// <param name="keyCode">The key code.</param>
        /// <param name="keyState">The key state.</param>
        public KeyEventArgs(KeyboardState keyboard, Keys keyCode, KeyState keyState)
        {
            Keyboard = keyboard;
            KeyCode = keyCode;
            KeyState = keyState;
        }
    }
}
