using Microsoft.Xna.Framework.Graphics;

namespace Faseway.GameLibrary.Rendering
{
    public static class Extension
    {
        /// <summary>
        /// Wraps a specified text.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text to wrap.</param>
        /// <param name="length">The length.</param>
        /// <returns>A wrapped string.</returns>
        public static string Wrap(this SpriteFont font, string text, int length)
        {
            var line = string.Empty;
            var wrapped = string.Empty;

            foreach (var word in text.Split(' '))
            {
                if (font.MeasureString(line + word).Length() > length)
                {
                    wrapped = wrapped + line + '\n';
                    line = string.Empty;
                }

                line = line + word + ' ';
            }

            return wrapped + line;
        }
    }
}
