namespace EfficiencySystem.Models
{
    public class Logger : ILogger, IDisposable
    {
        private string _filePath;
        private static object _lock = new object();

        public Logger(string filePath)
        {
            _filePath = filePath;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(_filePath, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{logLevel}] {formatter(state, exception)} {Environment.NewLine}");
            }
        }

        public void Dispose() { }
    }

    public class LoggerProvider : ILoggerProvider
    {
        string path;
        public LoggerProvider(string path)
        {
            this.path = path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(path);
        }

        public void Dispose() { }
    }

    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
        {
            builder.AddProvider(new LoggerProvider(filePath));
            return builder;
        }
    }
}
