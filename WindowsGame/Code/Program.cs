using Faseway.GameLibrary;
using Faseway.GameLibrary.TestGame.Game;

namespace Faseway.GameLibrary.TestGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Seed.Initialize();

            using (var game = new TestGame.Game.TestGame())
            {
                game.Window.Title = "Faseway Windows Test Game";
                game.IsMouseVisible = true;

                game.Run();
            }
        }
    }
#endif
}

