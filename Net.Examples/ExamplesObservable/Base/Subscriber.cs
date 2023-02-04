namespace Net.Examles.ExamplesObservable.Base
{
    public abstract class Subscriber<T> : IObserver<T>
    {
        private IDisposable unsubscriber;
        private string Name { get; } = string.Empty;
        public Subscriber(string name)
        {
            Name = name;
        }

        public void Subscribe(IObservable<T> provider)
        {
            if (provider != null)
            {
                unsubscriber = provider.Subscribe(this);
            }
        }

        public abstract void OnNext(T value);

        public abstract void OnError(Exception ex);


        public void OnCompleted()
        {
            unsubscriber.Dispose();
            AfterComplited();
        }

        public abstract void AfterComplited();


    }
}
