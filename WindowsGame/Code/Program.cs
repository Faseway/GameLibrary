using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using Faseway.GameLibrary;
using Faseway.GameLibrary.TestGame.Game;
using Faseway.GameLibrary.Logging;

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

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);

            using (var game = new TestGame.Game.TestGame())
            {
                game.Window.Title = "Faseway Windows Test Game";
                //game.IsMouseVisible = true;
                game.Graphics.PreferredBackBufferWidth = 1024;
                game.Graphics.PreferredBackBufferHeight = 768;
                //game.Graphics.IsFullScreen = true;

                game.Run();
            }
        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                var trace = new StackTrace(exception);
                var callerMethodName = trace.GetFrame(1).GetMethod().Name;
                var callerClassName = trace.GetFrame(1).GetMethod().ReflectedType.Name;

                Logger.Error("Unhandled exception of type {0} has been thrown", exception.GetType());

                if (e.ExceptionObject.GetType() == typeof(ContentLoadException))
                {
                    var ex = e.ExceptionObject as ContentLoadException;
                    
                    Logger.Error(ex.Message);
                    Logger.Error(ex.StackTrace);
                    MsgBox.Show(MsgBoxIcon.Error, callerClassName + "::" + callerMethodName, ex.Message);
                }
            }
            else
            {
                Logger.Error("Fatal error");
                MsgBox.Show(MsgBoxIcon.Error, "Fatal error", "Unhandled exception has been thrown");
            }
            Logger.Error("------------------------------------------------");
        }
    }
#endif
}

