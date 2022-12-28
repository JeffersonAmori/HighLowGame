namespace LoggerAdapter;

public interface ILoggerAdapter<out T>
{
    void LogInformation(string message);
    void LogInformation<T1>(string message, T1 param1);
    void LogInformation<T1, T2>(string message, T1 param1, T2 param2);
    void LogInformation<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3);
    void LogWarning(string message);
    void LogWarning<T1>(string message, T1 param1);
    void LogWarning<T1, T2>(string message, T1 param1, T2 param2);
    void LogWarning<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3);
    void LogError(string message);
    void LogError<T1>(string message, T1 param1);
    void LogError<T1, T2>(string message, T1 param1, T2 param2);
    void LogError<T1, T2, T3>(string message, T1 param1, T2 param2, T3 param3);
}