using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;

using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary
{
    /// <summary>
    /// Provides the seed of the library.
    /// </summary>
    public static class Seed
    {
        // Properties
        /// <summary>
        /// Gets a value indicating whether the library has been initialized.
        /// </summary>
        public static bool Initialized { get; private set; }

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        public static string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
        /// <summary>
        /// Gets the assembly build date.
        /// </summary>
        public static string BuildDate
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
                    TimeSpan.TicksPerSecond * 2 * version.Revision)); // seconds since midnight, (multiply by 2 to get original)

                // a valid date-string can now be constructed like this
                return buildDateTime.ToShortDateString();
            }
        }

        // Constructor
        /// <summary>
        /// Initializes the <see cref="Faseway.GameLibrary.Seed"/> class.
        /// </summary>
        static Seed()
        {
        }

        // Methods
        /// <summary>
        /// Initializes the library.
        /// </summary>
        public static void Initialize()
        {
            if (Initialized) return;

            // log
            Logger.Log("Initializing Game Library ({0}) on {1}", Version, Environment.OSVersion.Platform);

            // set conditions
            SetConditions();
            SetConditionsForDebug();

            Initialized = true;
        }

        private static void SetConditions()
        {

        }

        [ConditionalAttribute("DEBUG")]
        private static void SetConditionsForDebug()
        {
            Logger.Log("Initializing debug conditions ...");
        }
    }
}
