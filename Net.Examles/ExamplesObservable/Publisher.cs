using System.Collections.Concurrent;
using System.Reactive;


namespace Net.Observable.Reactive.Examples.ObservableTests.Base;

public class Publisher<T> : IObservable<T>, IDisposable
{
    protected ConcurrentBag<IObserver<T>> observers = new();


    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }

        return new Unsubscriber<T>(observers, observer);
    }

    public void Send(IObserver<T> observer, T value)
    {
        observer.OnNext(value);
    }


    public void Publish(T value)
    {
        foreach (var observer in observers)
        {
            try
            {
                observer.OnNext(value);
            }
            catch (Exception ex)
            {
                observer.OnError(ex);
            }
        }
    }


    public void Completed()
    {
        foreach (var observer in observers.ToArray())
        {
            if (observers.Contains(observer))
            {
                observer.OnCompleted();
            }
        }

        observers.Clear();
    }

    public void Dispose() => Completed();

}




class Unsubscriber<T> : IDisposable
{
    protected ConcurrentBag<IObserver<T>> observers;
    protected IObserver<T> observer;

    public Unsubscriber(ConcurrentBag<IObserver<T>> observers, IObserver<T> observer)
    {
        this.observers = observers;
        this.observer = observer;
    }

    public void Dispose()
    {
        if (observer != null && observers.Contains(observer))
        {
            observers.TryTake(out var observer);
        }
    }
}
