using System;
using System.IO;
using System.Text;

namespace Faseway.GameLibrary.Logging
{
    /// <summary>
    /// Provides a logger for logging into a logfile.
    /// </summary>
    public class FileLogger : ILogger
    {
        // Variables
        private readonly LoggerType _type;
        private readonly object _lock;

        private readonly string _name;
        private readonly string _filename;
        private StreamWriter _stream;

        // Properties
        /// <summary>
        /// Gets the name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        public string Name { get { return _name; } }
        /// <summary>
        /// Gets the log filename of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        public string LogFile { get { return _filename; } }
        /// <summary>
        /// Gets the <see cref="Faseway.GameLibrary.Logging.LoggerType"/> of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.
        /// </summary>
        public LoggerType Type { get { return _type; } }

        // Constructor
        /// <summary>
        /// Intitializes a new instance of the <see cref="Faseway.GameLibrary.Logging.FileLogger"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        /// <param name="logFile">The log filename of the <see cref="Faseway.GameLibrary.Logging.ILogger"/>.</param>
        public FileLogger(string name, string logFile)
        {
            _type = LoggerType.File;
            _lock = new object();
            _name = name;
            _filename = logFile;

            CreateFileLogger();
        }

        // Methods
        #region Public Methods

        /// <summary>
        /// Logs a specified value into a logfile.
        /// </summary>
        /// <param name="value">The specified value to log.</param>
        public void Log(object value)
        {
            lock (_lock)
            {
                if (_stream == null) CreateFileLogger();

                if (_stream != null && _stream.BaseStream.CanWrite)
                {
                    _stream.WriteLine("[{0}] > {1}", DateTime.Now.ToString("dd/MM HH:mm:ss"), value);
                    _stream.Flush();
                }
            }
        }

        /// <summary>
        /// Logs a specified value into a logfile.
        /// </summary>
        /// <param name="value">The specified value to log.</param>
        public void Log(string value)
        {
            Log((object)value);
        }

        /// <summary>
        /// Logs a specified value into a logfile.
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
            if (_stream != null)
            {
                _stream.Flush();
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }

            GC.Collect();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a new file logger by initializing a new <see cref="System.IO.StreamWriter"/> instance.
        /// </summary>
        private void CreateFileLogger()
        {
            if (_filename.Contains("/") || _filename.Contains("\\"))
            {
                var directory = new DirectoryInfo(_filename);
                directory.Parent.Create();
            }

            _stream = new StreamWriter(_filename, true);
        }

        #endregion
    }
}
