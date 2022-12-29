namespace LoggerAdapter;

/// <summary>
/// The generic Logger Adapter
/// </summary>
/// <typeparam name="T">The type to log.</typeparam>
public interface ILoggerAdapter<out T>
{
    /// <summary>
    /// Logs an information message.
    /// </summary>
    /// <param name="message">Message</param>
    void LogInformation(string message);
    /// <summary>
    /// Logs an information message.
    /// </summary>
    /// <typeparam name="T1">Type of first parameter.</typeparam>
    /// <param name="message">Message</param>
    /// <param name="param1">First param</param>
    void LogInformation<T1>(string message, T1 param1);
    /// <summary>
    /// Logs an information message.
    /// </summary>
    /// <typeparam name="T1">Type of first parameter.</typeparam>
    /// <typeparam name="T2">Type of second parameter.</typeparam>
    /// <param name="message">Message</param>
    /// <param name="param1">First param</param>
    /// <param name="param2">Second param</param>
    void LogInformation<T1, T2>(string message, T1 param1, T2 param2);
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
    void LogInformation<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3);
    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">Message</param>
    void LogWarning(string message);
    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <typeparam name="T1">Type of first parameter.</typeparam>
    /// <param name="message">Message</param>
    /// <param name="param1">First param</param>
    void LogWarning<T1>(string message, T1 param1);
    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <typeparam name="T1">Type of first parameter.</typeparam>
    /// <typeparam name="T2">Type of second parameter.</typeparam>
    /// <param name="message">Message</param>
    /// <param name="param1">First param</param>
    /// <param name="param2">Second param</param>
    void LogWarning<T1, T2>(string message, T1 param1, T2 param2);
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
    void LogWarning<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3);
    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">Message</param>
    void LogError(string message);
    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <typeparam name="T1">Type of first parameter.</typeparam>
    /// <param name="message">Message</param>
    /// <param name="param1">First param</param>
    void LogError<T1>(string message, T1 param1);
    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <typeparam name="T1">Type of first parameter.</typeparam>
    /// <typeparam name="T2">Type of second parameter.</typeparam>
    /// <param name="message">Message</param>
    /// <param name="param1">First param</param>
    /// <param name="param2">Second param</param>
    void LogError<T1, T2>(string message, T1 param1, T2 param2);
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
    void LogError<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3);
}