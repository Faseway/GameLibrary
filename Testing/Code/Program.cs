using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Game.Entities;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Scripting;
using Faseway.GameLibrary.Serialization;
using Faseway.GameLibrary.Localization;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Widgets;
using System.IO;

namespace Faseway.GameLibrary.Testing
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Library Test App";

            Console.WriteLine("Game Library Test App");
            Console.WriteLine();

            Seed.Initialize();
            Seed.Components.Install(new TestComp());
            Seed.Components.Install(new ScriptCompiler());
            Seed.Components.Install(new LocalizationManager());

            Seed.Components.GetAndRequire<LocalizationManager>().LoadLanguages("Lang");
            Seed.Components.GetAndRequire<LocalizationManager>().ChangeLanguage("en");

            Seed.Components.GetAndRequire<ScriptCompiler>().Compile("Scripts\\Dummy.script");
            Seed.Components.GetAndRequire<ScriptCompiler>().Compile("Scripts\\Hello.script");

            Logger.Log("Compiled {0} scripts", Seed.Components.GetAndRequire<ScriptCompiler>().CompiledCount);

            Seed.Components.GetAndRequire<ScriptCompiler>().GetCompiled("Hello").ConvertTo<CommandScript>().PushActions(null, null);

            Console.WriteLine(Seed.Components.GetAndRequire<LocalizationManager>().Get("common", "welcome"));

            using (var game = new Game.Game())
            {
                game.Environment.Add(game.Environment.Factory.Create());
                game.Environment.Add(game.Environment.Factory.Create());
                game.Environment.Add(game.Environment.Factory.Create());

                game.Campaign = new Game.Campaigns.Campaign();
                game.Campaign.AddObjective("OBJ_EAT_TOAST");
                game.Campaign.AddObjective("OBJ_HAVE_FUN");
                game.Campaign.AddObjective("OBJ_KILL_RUFFY");

                game.Campaign.SetObjectiveAccomplished("OBJ_KILL_RUFFY", true);

                Logger.Log("Campaign objectives : {0} (Accomplished : {1} Remaining : {2})", game.Campaign.ObjectiveNum, game.Campaign.ObjectivesAccomplishedNum, game.Campaign.ObjectivesNotAccomplishedNum);

                game.Run();
            }

            var container = new WidgetContainer();
            var widget = new Frame(container);
            new Box(widget);
            new Label(widget);

            var serializer = new UiSerializer();
            serializer.Serialize(container, File.Open("Data//UI//Dummy.xml", FileMode.Create, FileAccess.Write));

            Console.Read();
        }
    }

    class TestComp : IComponent
    {
    }
}
