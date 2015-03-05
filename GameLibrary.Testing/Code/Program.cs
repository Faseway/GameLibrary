using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Scripting;

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

            Seed.Components.GetAndRequire<ScriptCompiler>().Compile("Scripts\\Dummy.script");
            Seed.Components.GetAndRequire<ScriptCompiler>().Compile("Scripts\\Hello.script");

            Logger.Log("Compiled {0} scripts", Seed.Components.GetAndRequire<ScriptCompiler>().CompiledCount);

            Seed.Components.GetAndRequire<ScriptCompiler>().GetCompiled("Hello").ConvertTo<CommandScript>().PushActions(null, null);

            Console.Read();
        }
    }

    class TestComp : IComponent
    {
    }
}
