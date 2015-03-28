using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.Rendering;


namespace Faseway.GameLibrary.Game.Env
{
    public class Map : IGameHandler
    {
        // Properties
        /// <summary>
        /// Gets the texture of the map.
        /// </summary>
        public Texture2D Texture { get; private set; }
        /// <summary>
        /// Gets the name of the map.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the boundaries of the map.
        /// </summary>
        public Rectangle Bounds 
        {
            get { return Texture.Bounds; }
        }
        /// <summary>
        /// Gets the size of the map.
        /// </summary>
        public Vector2 Size
        {
            get { return new Vector2(Texture.Bounds.Width, Texture.Bounds.Height); }
        }

        // Constructor
        public Map(string name)
        {
            Texture = Seed.Components.GetAndRequire<XnaReference>()
                .GetAndRequire<ContentManager>()
                .Load<Texture2D>("Textures\\Maps\\" + name);

            Name = name;
        }

        // Methods
        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            var graphics = Seed.Components.GetAndRequire<Graphics2D>();
            graphics.DrawTexture(Texture, Vector2.Zero, Texture.Bounds, Color.White);
        }
    }
}
