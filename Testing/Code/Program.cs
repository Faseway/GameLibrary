using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Content;
using Faseway.GameLibrary.Game;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Scripting;
using Faseway.GameLibrary.Localization;

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

            Console.Read();
        }
    }

    class TestComp : IComponent
    {
    }
}
