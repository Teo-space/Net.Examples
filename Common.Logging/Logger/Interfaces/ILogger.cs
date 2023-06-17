public interface ILogger
{
    void Info(string message);
    void Info(params object[] parameters);

    void Warn(string message);


    void Error(string message);
    void Error(string message, Exception exception);
    void Error(Exception exception);
}