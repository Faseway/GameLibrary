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
        private static readonly List<ILogger> _loggers;
        private static readonly object _locker;

        // Properties
        /// <summary>
        /// Gets a value indicating whether the <see cref="Faseway.GameLibrary.Logging.Logger"/> has been initialized.
        /// </summary>
        public static bool Initialized { get; private set; }
        /// <summary>
        /// Gets the base directory of the <see cref="Faseway.GameLibrary.Logging.Logger"/>.
        /// </summary>
        public static string BaseDirectory { get; private set; }
        /// <summary>
        /// Gets a catched log.
        /// </summary>
        public static string CatchedLog
        {
            get
            {
                var logger = (ConsoleLogger)GetLogger("CONSOLE");
                if (logger != null)
                {
                    return logger.CatchedLog.ToString();
                }

                return string.Empty;
            }
        }

        // Constants
        public const string FILE_LOG = "FileLog.txt";
        public const string CONNECT_LOG = "ConnectLog.txt";
        public const string GAME_RESULT_LOG = "GameResultLog.txt";
        public const string HACK_LOG = "HackLog.txt";
        public const string PACKET_HACK_LOG = "PacketHackLog.txt";
        public const string GM_HACK_LOG = "GmHackLog.txt";
        public const string LEVEL_UP_LOG = "LevelUpLog.txt";
        public const string ITEM_BUY_LOG = "ItemBuyLog.txt";
        public const string ITEM_EXPIRE_LOG = "ItemExpireLog.txt";
        public const string ERR_LOG = "ErrorLog.txt";

        // Constructor
        /// <summary>
        /// Initializes a <see cref="Faseway.GameLibrary.Logging.Logger"/> instance.
        /// </summary>
        static Logger()
        {
            _loggers = new List<ILogger>();
            _locker = new object();
        }

        // Methods
        /// <summary>
        /// Initializes the <see cref="Faseway.GameLibrary.Logging.Logger"/>.
        /// </summary>
        public static void Initialize()
        {
            if (Initialized) return;

            Initialize(null);

            Initialized = true;
        }

        /// <summary>
        /// Initializes the <see cref="Faseway.GameLibrary.Logging.Logger"/>.
        /// <param name="directory">The base directory of the Logger.</param>
        /// </summary>
        public static void Initialize(string directory)
        {
            if (Initialized) return;

            BaseDirectory = string.IsNullOrEmpty(directory) ? "LogFiles" : directory;

            AddLogger(LoggerType.Console, "CONSOLE");
            AddLogger(LoggerType.File, "FILELOG", Logger.FILE_LOG);

            Initialized = true;
        }

        /// <summary>
        /// Adds a new <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        /// <param name="loggerType">The <see cref="Faseway.GameLibrary.Logging.LoggerType"/>.</param>
        /// <param name="name">The name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        public static void AddLogger(LoggerType loggerType, string name)
        {
            AddLogger(loggerType, name, null);
        }

        /// <summary>
        /// Adds a new <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        /// <param name="loggerType">The <see cref="Faseway.GameLibrary.Logging.LoggerType"/>.</param>
        /// <param name="name">The name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        /// <param name="logFile">The path of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        public static void AddLogger(LoggerType loggerType, string name, string logFile)
        {
            if (!ContainsLogger(name))
            {
                if (loggerType == LoggerType.Console)
                {
                    _loggers.Add(new ConsoleLogger("CONSOLE"));
                }
                else if (loggerType == LoggerType.File)
                {
                    _loggers.Add(new FileLogger(name, BaseDirectory + "\\" + logFile));
                }
                else
                {
                    if (!ContainsLogger("CONSOLE"))
                    {
                        _loggers.Add(new ConsoleLogger("CONSOLE"));
                        _loggers.Add(new FileLogger(name, BaseDirectory + "\\" + logFile));
                    }
                    else
                    {
                        _loggers.Add(new FileLogger(name, BaseDirectory + "\\" + logFile));
                    }
                }
            }
            else
            {
                Logger.Log("Logger {0} already exists", name);
            }
        }

        /// <summary>
        /// Gets a <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        /// <returns>Retuns a <see cref="Faseway.GameLibrary.Logging.ILogger"/> instance.</returns>
        public static ILogger GetLogger(string name)
        {
            return _loggers.Find(logger => logger.Name == name);
        }

        /// <summary>
        /// Removes a <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        public static void RemoveLogger(string name)
        {
            foreach (ILogger logger in _loggers.Where(logger => logger.Name == name))
            {
                logger.Close();
                _loggers.Remove(logger);
            }
        }

        /// <summary>
        /// Checks whether a specific <see cref="Faseway.GameLibrary.Logging.ILogger"/> exists.
        /// </summary>
        /// <param name="name">The name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        /// <returns>Returns a <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</returns>
        public static bool ContainsLogger(string name)
        {
            return _loggers.Any(logger => logger.Name == name);
        }

        /// <summary>
        /// Closes all <see cref="Faseway.GameLibrary.Logging.ILogger"/> instances.
        /// </summary>
        public static void CloseAll()
        {
            _loggers.ForEach(logger => logger.Close());
            _loggers.Clear();
        }

        /// <summary>
        /// Prints to the standard output stream.
        /// </summary>
        /// <param name="value">The value to print.</param>
        public static void Print(string value)
        {
            lock (_locker)
            {
                // log to console
                var logger = GetLogger("CONSOLE");
                if (logger != null)
                {
                    logger.Log(value);
                }
            }
        }

        /// <summary>
        /// Prints a formatted string to the standard output stream.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to print using format.</param>
        public static void Print(string format, params object[] args)
        {
            Print(string.Format(format, args));
        }

        /// <summary>
        /// Writes to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public static void Log(string value)
        {
            lock (_locker)
            {
                // log to console
                var logger = GetLogger("CONSOLE");
                if (logger != null)
                {
                    logger.Log(value);
                }

                // log to file
                logger = GetLogger("FILELOG");
                if (logger != null)
                {
                    logger.Log(value);
                }
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

        /// <summary>
        /// Writes to the error output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public static void Error(string value)
        {
            var logger = GetLogger("ERR_LOG");
            if (logger != null)
            {
                Log(value);
                logger.Log(value);
            }
        }

        /// <summary>
        /// Writes a formatted string to the error output stream.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to write using format.</param>
        public static void Error(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }
    }
}
