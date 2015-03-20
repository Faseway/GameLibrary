using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Entities;
using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.Components;

namespace Faseway.GameLibrary.Game.Env
{
    public class Camera : IGameHandler
    {
        // Properties
        /// <summary>
        /// Gets or sets the min.
        /// </summary>
        public Vector2 Min { get; set; }
        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        public Vector2 Max { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Camera"/> is clamped.
        /// </summary>
        public bool Clamped { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public Entity Target { get; set; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the display offset.
        /// </summary>
        public Vector2 DisplayOffset { get; private set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets the distance.
        /// </summary>
        public Vector2 Distance
        {
            get { return DisplayOffset - Position; }
        }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Env.Camera"/> class.
        /// </summary>
        public Camera()
        {
            Clamped = true;
        }

        // Methods
        /// <summary>
        /// Moves the camera instantly.
        /// </summary>
        /// <param name="position">The position.</param>
        public void MoveTo(Vector2 position)
        {
            Position = position;
            DisplayOffset = position;
        }

        public void Update(float elapsed)
        {
            if (Target != null && !Target.IsEnabled)
            {
                //Position = Target.Position;
            }

            Vector2 screenCenter = new Vector2(GraphicsDevice.DisplayMode.Width / 2, GraphicsDevice.DisplayMode.Height / 2);
            Vector2 min = Min + screenCenter;
            Vector2 max = new Vector2(Max.X - screenCenter.X, Max.Y - screenCenter.Y);

            DisplayOffset += (Position - DisplayOffset) * 0.15f;
            if (Clamped)
            {
                DisplayOffset = Vector2.Clamp(DisplayOffset, min, max);
            }
        }

        public void Draw()
        {
            throw new System.NotImplementedException();
        }
    }
}
