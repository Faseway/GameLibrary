using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary;

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

            Console.Read();
        }
    }
}
