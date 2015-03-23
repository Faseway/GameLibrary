using Microsoft.Xna.Framework;

namespace Faseway.GameLibrary.Game.Handlers
{
    public interface IDrawHandler
    {
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void Draw(GameTime gameTime);
    }
}
