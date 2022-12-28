using Microsoft.Extensions.Logging;

namespace LoggerAdapter
{
    public class GenericLoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        public GenericLoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {

            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message);
        }

        public void LogInformation<T1>(string message, T1 param1)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, param1);
        }

        public void LogInformation<T1, T2>(string message, T1 param1, T2 param2)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, param1, param2);
        }

        public void LogInformation<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, param1, param2, param3);
        }

        public void LogWarning(string message)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message);
        }

        public void LogWarning<T1>(string message, T1 param1)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, param1);
        }

        public void LogWarning<T1, T2>(string message, T1 param1, T2 param2)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, param1, param2);
        }

        public void LogWarning<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, param1, param2, param3);
        }

        public void LogError(string message)
        {

            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message);
        }

        public void LogError<T1>(string message, T1 param1)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, param1);
        }

        public void LogError<T1, T2>(string message, T1 param1, T2 param2)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, param1, param2);
        }

        public void LogError<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, param1, param2, param3);
        }
    }
}