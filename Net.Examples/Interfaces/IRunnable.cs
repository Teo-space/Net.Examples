internal interface IRunnable
{
    Task Run();
}

internal interface IRunnable<T>
{
    Task<T> Run();
}

