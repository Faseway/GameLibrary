using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Logging
{
    /// <summary>
    /// Provides a global logger.
    /// </summary>
    public static class Logger
    {
        // Variables
        private static object _locker;

        // Properties

        // Constructor
        /// <summary>
        /// Initializes a <see cref="Faseway.GameLibrary.Logging.Logger"/> instance.
        /// </summary>
        static Logger()
        {
            _locker = new object();
        }

        // Methods
        /// <summary>
        /// Writes to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public static void Log(string value)
        {
            lock (_locker)
            {
                Console.WriteLine(value);
            }
        }

        /// <summary>
        /// Writes a formatted string to the standard output stream.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to write using format.</param>
        public static void Log(string format, params object[] args)
        {
            Log(string.Format(format, args));
        }
    }
}
