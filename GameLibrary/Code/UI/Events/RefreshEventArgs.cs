using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Faseway.GameLibrary.UI.Events
{
    public class RefreshEventArgs : EventArgs
    {
        // Properties
        public GameTime Time { get; private set; }

        // Constructor
        public RefreshEventArgs(GameTime gameTime)
        {
            Time = gameTime;
        }
    }
}
