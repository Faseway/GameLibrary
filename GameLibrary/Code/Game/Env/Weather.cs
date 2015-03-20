using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Game.Env
{
    public class Weather : IGameHandler
    {
        // Properties
        /// <summary>
        /// Gets a value indicating whether the weather effect rain is enabled.
        /// </summary>
        public bool IsRainEnabled { get; private set; }

        // Constructor
        /// <summary>
        /// Initializing a new instance of the <see cref="Faseway.GameLibrary.Game.Env.Weather"/> class.
        /// </summary>
        public Weather()
        {
        }

        // Methods
        /// <summary>
        /// Enables the rain effect.
        /// </summary>
        public void EnableRain()
        {
            IsRainEnabled = true;
            ChangeEffect("rain", true);
        }

        /// <summary>
        /// Disables the rain effect.
        /// </summary>
        public void DisableRain()
        {
            IsRainEnabled = false;
            ChangeEffect("rain", false);
        }

        /// <summary>
        /// Changes the specified weather effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="enablement">The enablement.</param>
        private void ChangeEffect(string effect, bool enablement)
        {
            Logger.Log("Weather effect {0} {1}", effect, enablement ? "enabled" : "disabled");
        }

        public void Update(float elapsed)
        {
            throw new System.NotImplementedException();
        }

        public void Draw()
        {
            throw new System.NotImplementedException();
        }
    }
}
