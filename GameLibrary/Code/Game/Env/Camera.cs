using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Entities;
using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Rendering;

namespace Faseway.GameLibrary.Game.Env
{
    public class Camera : IGameHandler, IComponent
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
        /// Gets the graphics.
        /// </summary>
        public Graphics2D Graphics
        {
            get { return Seed.Components.GetAndRequire<Graphics2D>(); }
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

        private float _zoom;

        /// <summary>
        /// Gets or sets the zoom factor.
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set 
            {
                if (value > ZoomUpperLimit)
                {
                    _zoom = ZoomUpperLimit;
                }
                else if (value < ZoomLowerLimit)
                {
                    _zoom = ZoomLowerLimit;
                }
                else
                {
                    _zoom = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        public float Rotation { get; set; }

        public World World { get; private set; }

        public Matrix Transformation 
        {
            get
            {
                var matrix =
                    Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 0))/* *
                    Matrix.CreateTranslation(new Vector3(World.Map.Texture.Bounds.Width, World.Map.Texture.Bounds.Height, 0))*/;
                
                //MsgBox.Show(DisplayOffset);
                return Matrix.CreateTranslation(DisplayOffset.X, DisplayOffset.Y, 0) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(1.25f);
            }
        }


        // Constants
        public const float ZoomUpperLimit = 1.5f;
        public const float ZoomLowerLimit = 0.5f;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Env.Camera"/> class.
        /// </summary>
        public Camera(World world)
        {
            Clamped = true;

            World = world;

            //Min = Vector2.Zero;
            //Max = Vector2.Zero;

            Position = Vector2.Zero;

            Seed.Components.Install(this);
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

        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (Target != null && Target.IsEnabled)
            {
                Position = -Target.Transform.Position;
            }

            Vector2 screenCenter = Graphics.Center;
            Vector2 min = Min + screenCenter;
            Vector2 max = new Vector2(Max.X - screenCenter.X, Max.Y - screenCenter.Y);

            //MsgBox.Show("Camera", "Min: {0} Max: {1} Center: {2}", min, max, screenCenter);

            DisplayOffset += (Position - DisplayOffset) * 0.15f;
            if (!Clamped)
            {
                DisplayOffset = Vector2.Clamp(DisplayOffset, min, max);
            }
        }

        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            //throw new System.NotImplementedException();
        }
    }
}
