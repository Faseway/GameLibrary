using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Logging;

using System.Xml.Linq;

namespace Faseway.GameLibrary.Rendering
{
    public class SpriteSheet
    {
        // Variables
        private readonly Dictionary<string, Rectangle> _sprites;

        // Properties
        /// <summary>
        /// Gets or sets the base texture.
        /// </summary>
        public Texture2D Texture { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Rendering.SpriteSheet" /> class.
        /// </summary>
        public SpriteSheet()
        {
            _sprites = new Dictionary<string, Rectangle>();
        }

        // Methods
        /// <summary>
        /// Adds a sprite.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sprite">The sprite rectangle.</param>
        public void Add(string name, Rectangle sprite)
        {
            if (!_sprites.ContainsKey(name))
            {
                _sprites.Add(name, sprite);
            }
            else
            {
                Logger.Log("Sprite {0} has already been added to the sheet");
            }
        }

        /// <summary>
        /// Returns a sprite.
        /// </summary>
        /// <param name="name">The sprite.</param>
        /// <returns>A <see cref="Microsoft.Xna.Framework.Rectangle"/> of the sprite.</returns>
        public Rectangle Get(string name)
        {
            if (_sprites.ContainsKey(name))
            {
                return _sprites[name];
            }
            else
            {
                Logger.Log("Sprite {0} was not found in the sheet");
                return Rectangle.Empty;
            }
        }

        /// <summary>
        /// Determinates whether a sprite exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True, if the sprite exists. Otherwise, false.</returns>
        public bool Contains(string name)
        {
            return _sprites.ContainsKey(name);
        }

        /// <summary>
        /// Removes a sprite.
        /// </summary>
        /// <param name="name">The sprite.</param>
        public void Remove(string name)
        {
            _sprites.Remove(name);
        }
    }
}
