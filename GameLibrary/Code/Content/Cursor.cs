using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Rendering;

namespace Faseway.GameLibrary.Content
{
    /// <summary>
    /// Provides a class for managing different cursors.
    /// </summary>
    public class Cursor : IComponent, IGameHandler, ISortableDrawHandler
    {
        // Properties
        /// <summary>
        /// Gets a collection of all cursors with the associated name.
        /// </summary>
        public Dictionary<string, Rectangle> Cursors { get; private set; }
        /// <summary>
        /// Gets a texture containing all cursor sprites.
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Gets a rectangle representing the current cursor sprite.
        /// </summary>
        public Rectangle Current { get; private set; }
        /// <summary>
        /// Gets the current position of the cursor.
        /// </summary>
        public Vector2 Position 
        {
            get { return new Vector2(Mouse.GetState().X, Mouse.GetState().Y); }
        }
        /// <summary>
        /// Gets the index of the render.
        /// <remarks>999 to bring the cursor always on top.</remarks>
        /// </summary>
        public int RenderIndex
        {
            get { return 999; }
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Content.Cursor"/> class.
        /// </summary>
        public Cursor()
        {
            Cursors = new Dictionary<string, Rectangle>();
        }

        // Methods
        /// <summary>
        /// Adds a cursor.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cursor">The rectangle of the cursor.</param>
        public void Add(string name, Rectangle cursor)
        {
            if (!Cursors.ContainsKey(name))
            {
                Cursors.Add(name, cursor);
                //Logger.Log("Cursor {0} has been added", name);
            }
            else
            {
                Logger.Log("Cursor {0} has already been added", name);
            }
        }

        /// <summary>
        /// Changes the cursor.
        /// </summary>
        /// <param name="name">The name of the new cursor.</param>
        public void Change(string name)
        {
            if (Cursors.ContainsKey(name))
            {
                Current = Cursors[name];
            }
            else
            {
                Logger.Log("Cursor {0} was not found and could not been set", name);
            }
        }

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
            if (Texture != null)
            {
                var graphics = Seed.Components.GetAndRequire<Graphics2D>();
                graphics.SpriteBatch.Begin();
                graphics.SpriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Current.Width, Current.Height), Current, Color.White);
                graphics.SpriteBatch.End();
            }
        }
    }
}
