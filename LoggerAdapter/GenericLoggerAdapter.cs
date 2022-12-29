using Microsoft.Extensions.Logging;

namespace LoggerAdapter
{
    /// <summary>
    /// The generic Logger Adapter
    /// </summary>
    /// <typeparam name="T">The type to log.</typeparam>
    public sealed class GenericLoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        /// <summary>
        /// Default constructor that takes an <see cref="ILogger{T}"/>.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public GenericLoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">Message</param>
        public void LogInformation(string message)
        {

            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message);
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        public void LogInformation<T1>(string message, T1 param1)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, param1);
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <typeparam name="T2">Type of second parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        /// <param name="param2">Second param</param>
        public void LogInformation<T1, T2>(string message, T1 param1, T2 param2)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, param1, param2);
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <typeparam name="T2">Type of second parameter.</typeparam>
        /// <typeparam name="T3">Type of third parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        /// <param name="param2">Second param</param>
        /// <param name="param3">Third param</param>
        public void LogInformation<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, param1, param2, param3);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">Message</param>
        public void LogWarning(string message)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        public void LogWarning<T1>(string message, T1 param1)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, param1);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <typeparam name="T2">Type of second parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        /// <param name="param2">Second param</param>
        public void LogWarning<T1, T2>(string message, T1 param1, T2 param2)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, param1, param2);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <typeparam name="T2">Type of second parameter.</typeparam>
        /// <typeparam name="T3">Type of third parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        /// <param name="param2">Second param</param>
        /// <param name="param3">Third param</param>
        public void LogWarning<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, param1, param2, param3);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">Message</param>
        public void LogError(string message)
        {

            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        public void LogError<T1>(string message, T1 param1)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, param1);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <typeparam name="T2">Type of second parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        /// <param name="param2">Second param</param>
        public void LogError<T1, T2>(string message, T1 param1, T2 param2)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, param1, param2);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <typeparam name="T1">Type of first parameter.</typeparam>
        /// <typeparam name="T2">Type of second parameter.</typeparam>
        /// <typeparam name="T3">Type of third parameter.</typeparam>
        /// <param name="message">Message</param>
        /// <param name="param1">First param</param>
        /// <param name="param2">Second param</param>
        /// <param name="param3">Third param</param>
        public void LogError<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, param1, param2, param3);
        }
    }
}