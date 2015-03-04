namespace Faseway.GameLibrary.Logging
{
    public interface ILogger
    {
        // Properties
        string Name { get; }
        string LogFile { get; }
        LoggerType Type { get; }

        // Methods
        void Close();
        void Log(string value);
        void Log(string format, params object[] args);
    }
}
