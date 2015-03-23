using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Game
{
    public class GameLoop : IComponent
    {
        // Variables
        private readonly List<IGameHandler> _handlers;
        private int _lastTick;
        private int _lastFrameRate;
        private int _frameRate;

        // Properties
        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        public long FrameIndex { get; private set; }
        /// <summary>
        /// Gets the frames per second.
        /// </summary>
        public int FramesPerSecond { get { return _lastFrameRate; } }

        /// <summary>
        /// Gets the tick handlers.
        /// </summary>
        public IEnumerable<IUpdateHandler> TickHandlers
        {
            get
            {
                return this._handlers.OfType<IUpdateHandler>().OrderBy(handler =>
                {
                    var sortable = handler as ISortableUpdateHandler;
                    if (sortable != null)
                    {
                        return sortable.TickIndex;
                    }

                    return 0;
                });
            }
        }
        /// <summary>
        /// Gets the render handlers.
        /// </summary>
        public IEnumerable<IDrawHandler> RenderHandlers
        {
            get
            {
                return this._handlers.OfType<IDrawHandler>().OrderBy(handler =>
                {
                    var sortable = handler as ISortableDrawHandler;
                    if (sortable != null)
                    {
                        return sortable.RenderIndex;
                    }

                    return 0;
                });
            }
        }

        // Constructor
        /// <summary>
        /// Initializing a new instance of the <see cref="Faseway.GameLibrary.Game.GameLoop"/> class.
        /// </summary>
        public GameLoop()
        {
            _handlers = new List<IGameHandler>();
        }

        // Methods
        #region Public 

        /// <summary>
        /// Subscribes and adds the handler to the game loop.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Subscribe(IGameHandler handler)
        {
            Logger.Log("Subscribed {0} to the game loop", handler.GetType().Name);
            _handlers.Add(handler);
        }

        /// <summary>
        /// Unsubscribes the game loop and removes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Unsubscribe(IGameHandler handler)
        {
            Logger.Log("Removed {0} from game loop", handler.GetType().Name);
            _handlers.Remove(handler);
        }

        /// <summary>
        /// Updates all game components.
        /// </summary>
        /// <param name="gameTime">A snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            OnUpdate(gameTime);
        }

        /// <summary>
        /// Draws all game components.
        /// </summary>
        /// <param name="gameTime">A snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            CalculateFrameRate();
            OnDraw(gameTime);
        }

        #endregion

        #region Protected

        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected virtual void OnUpdate(GameTime gameTime)
        {
            // Increment the frame index, since a frame has passed. A
            // frame can only pass inside a tick call, since render isn't called
            // as frequent as tick
            FrameIndex++;

            foreach (IUpdateHandler gameHandler in TickHandlers)
            {
                gameHandler.Update(gameTime);
            }
        }

        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected virtual void OnDraw(GameTime gameTime)
        {
            foreach (IDrawHandler gameHandler in RenderHandlers)
            {
                gameHandler.Draw(gameTime);
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Updates the timing properties for FramesPerSecond.
        /// </summary>
        private int CalculateFrameRate()
        {
            if (Environment.TickCount - _lastTick >= 1000)
            {
                _lastFrameRate = _frameRate;
                _frameRate = 0;
                _lastTick = Environment.TickCount;
            }
            _frameRate++;
            return _lastFrameRate;
        }

        #endregion
    }
}
