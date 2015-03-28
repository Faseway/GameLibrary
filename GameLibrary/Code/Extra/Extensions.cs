using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Faseway.GameLibrary.Extra
{
    public static class Extensions
    {
        /// <summary>
        /// Subscribes an <see cref="System.EventHandler"/> and invokes that delegate, if the reference object exists.
        /// </summary>
        /// <param name="handler">The <see cref="System.EventHandler"/> to be invoked.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>.</param>
        public static void SafeInvoke(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler.Invoke(sender, e);
            }
        }

        /// <summary>
        /// Subscribes an <see cref="System.EventHandler"/> and invokes that delegate, if the reference object exists.
        /// </summary>
        /// <typeparam name="TEventArgs">The specified type.</typeparam>
        /// <param name="handler">The <see cref="System.EventHandler"/> to be invoked.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>.</param>
        public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if (handler != null)
            {
                handler.Invoke(sender, e);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector2 Floor(this Vector2 vector)
        {
            return new Vector2((int)vector.X, (int)vector.Y);
        }
    }
}
