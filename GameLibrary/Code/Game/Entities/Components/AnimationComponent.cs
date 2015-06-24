using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Faseway.GameLibrary.Game.Entities.Components
{
    public class AnimationComponent : EntityComponent
    {
        // Variables
        private string _pauseAnimationCache;

        // Properties
        /// <summary>
        /// Gets a collection of all animations.
        /// </summary>
        public List<string> Animations { get; private set; }
        /// <summary>
        /// Gets the current animation.
        /// </summary>
        public string Current { get; private set; }

        /// <summary>
        /// Gets a value idicating whether the current animation is paused.
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// Gets or sets the pause time.
        /// </summary>
        public float PauseTime { get; set; }

        // Constants
        public const string DefaultAnimationName = "None";
        public const string IdleAnimationName = "Idle";

        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Game.Entities.Components.AnimationComponent"/> class.
        /// </summary>
        public AnimationComponent()
            : base()
        {
            Animations = new List<string>();
            Animations.Add(DefaultAnimationName);

            Play(DefaultAnimationName);
        }

        // Methods
        /// <summary>
        /// Plays the specified animation.
        /// </summary>
        /// <param name="name">The animation.</param>
        public void Play(string name)
        {
            if (Animations.Contains(name))
            {
                Current = name;
            }
        }

        /// <summary>
        /// Pauses the current animation.
        /// </summary>
        public void Pause()
        {
            IsPaused = true;

            _pauseAnimationCache = Current;

            Stop();
        }

        /// <summary>
        /// Pauses the current animation for the specified time.
        /// <param name="time">The specified time.</param>
        /// </summary>
        public void Pause(float time)
        {
            PauseTime = time;
            Pause();
        }

        /// <summary>
        /// Resumes the animation.
        /// </summary>
        public void Resume()
        {
            IsPaused = false;

            Play(_pauseAnimationCache);
        }

        /// <summary>
        /// Plays the idle animation.
        /// </summary>
        public void Idle()
        {
            if (Animations.Contains(IdleAnimationName))
            {
                Current = IdleAnimationName;
            }
        }

        /// <summary>
        /// Plays the default animation.
        /// </summary>
        public void Default()
        {
            if (Animations.Contains(IdleAnimationName))
            {
                Current = IdleAnimationName;
            }
            else
            {
                Current = DefaultAnimationName;
            }
        }

        /// <summary>
        /// Plays the current animation.
        /// </summary>
        public void Stop()
        {
            if (Animations.Contains(DefaultAnimationName))
            {
                Current = DefaultAnimationName;
            }
        }

        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (IsPaused && PauseTime > 0)
            {
                PauseTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (PauseTime <= 0)
                {
                    Resume();
                }
            }

            base.Update(gameTime);
        }
    }
}
