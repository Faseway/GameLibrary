using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;

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

            Console.Read();
        }
    }

    class TestComp : IComponent
    {
    }
}
