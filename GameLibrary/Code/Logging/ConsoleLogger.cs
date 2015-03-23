using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Logging
{
    public class ConsoleLogger : ILogger
    {
        // Variables
        private readonly LoggerType _type;
        private readonly object _lock;

        private readonly string _name;

        // Properties
        /// <summary>
        /// Gets the name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        public string Name { get { return _name; } }
        /// <summary>
        /// Gets the log filename of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        public string LogFile { get { return Console.Out.GetType().Name; } }
        /// <summary>
        /// Gets the <see cref="Faseway.GameLibrary.Logging.LoggerType"/> of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        public LoggerType Type { get { return _type; } }
        /// <summary>
        /// Gets a catched log.
        /// </summary>
        public StringBuilder CatchedLog { get; private set; }

        // Constructor
        /// <summary>
        /// Intitializes a new instance of the <see cref="Faseway.GameLibrary.Logging.ConsoleLogger"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        public ConsoleLogger(string name)
        {
            _type = LoggerType.Console;
            _lock = new object();
            _name = name;

            CatchedLog = new StringBuilder();
        }

        // Methods
        #region Public Methods

        /// <summary>
        /// Logs a specified value into the standart output.
        /// </summary>
        /// <param name="value">The specified value to log.</param>
        public void Log(object value)
        {
            lock (_lock)
            {
                //Console.Out.WriteLine("[{0}] > {1}", DateTime.Now.ToString("dd/MM HH:mm:ss"), value);
                Console.Out.WriteLine(value);
                Console.Out.Flush();

                CatchedLog.AppendLine(value.ToString());
            }
        }

        /// <summary>
        /// Logs a specified value into the standart output.
        /// </summary>
        /// <param name="value">The specified value to log.</param>
        public void Log(string value)
        {
            Log((object)value);
        }

        /// <summary>
        /// Logs a specified value into the standart output.
        /// </summary>
        /// <param name="format"> A composite format string.</param>
        /// <param name="param">An <see cref="System.Object"/> array containing zero or more objects to format.</param>
        public void Log(string format, params object[] param)
        {
            Log(string.Format(format, param));
        }

        /// <summary>
        /// Closes the current <see cref="Faseway.GameLibrary.Logging.ILogger"/> and releases all resources.
        /// </summary>
        public void Close()
        {
            if (Console.Out != null)
            {
                Console.Out.Flush();
                Console.Out.Close();
                Console.Out.Dispose();
            }

            GC.Collect();
        }

        #endregion
    }
}
