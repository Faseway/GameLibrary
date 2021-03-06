﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Extra;
using Faseway.GameLibrary.Game.Env;


namespace Faseway.GameLibrary.Rendering
{
    public class Graphics2D : IComponent
    {
        // Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }
        /// <summary>
        /// Gets a pixel.
        /// </summary>
        public Texture2D Pixel { get; private set; }
        /// <summary>
        /// Gets the default sprite font.
        /// </summary>
        public SpriteFont Font { get; private set; }
        /// <summary>
        /// Gets a sprite batch.
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }
        /// <summary>
        /// Gets the window.
        /// </summary>
        public GameWindow Window { get; set; }

        /// <summary>
        /// Gets the viewport center.
        /// </summary>
        public Vector2 Center 
        {
            get { return new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2); }
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Rendering.Graphics2D"/> class.
        /// </summary>
        /// <param name="graphicsDevice">A reference to the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/>.</param>
        public Graphics2D(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;

            try
            {
                Pixel = new Texture2D(graphicsDevice, 1, 1);
                Pixel.SetData<Color>(new Color[] { Color.White });

                Font = Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<ContentManager>().Load<SpriteFont>("Data//Fonts//Default");

                SpriteBatch = new SpriteBatch(graphicsDevice);
            }
            catch (Exception ex)
            {
                MsgBox.Show(MsgBoxIcon.Error, "Graphics2D::Initialize", "Could not initialize Graphics2D");
                throw ex;
            }
        }

        // Methods
        public void DrawTexture(Texture2D texture, Vector2 position, Rectangle source, Color color)
        {
            var camera = Seed.Components.GetAndRequire<Camera>();

            SpriteBatch.Begin(SpriteSortMode.Texture, null, null, null, null, null, camera.Transformation); // Matrix.CreateTranslation(20, 0, 0)
            SpriteBatch.Draw(texture, position, source, color);
            SpriteBatch.End();
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            //SpriteBatch.Begin();
            SpriteBatch.Draw(Pixel, new Rectangle((int)start.X, (int)start.Y, (int)end.X, 1), color);
            //SpriteBatch.End(); 
        }

        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            var camera = Seed.Components.GetAndRequire<Camera>();

            SpriteBatch.Begin(SpriteSortMode.Texture, null, null, null, null, null, camera.Transformation);
            SpriteBatch.Draw(Pixel, rectangle, color);
            SpriteBatch.End();
        }
    }
}
