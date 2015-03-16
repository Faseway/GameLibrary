using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Components;

namespace Faseway.GameLibrary.Game
{
    public class GameLoop : IComponent
    {
        // Variables
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
        /// Loop, loop!
        /// </summary>
        public void Loop()
        {
            FrameIndex++;

            CalculateFrameRate();
        }

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
    }
}
