using Net.Examles.ExamplesObservable.Base;
using Net.Examples.ExamplesObservable;

namespace Net.Examles.ExamplesObservable
{
    public class LocationSubscriber : Subscriber<Location>
    {
        private readonly ILogger<ObservableExample> Logger;

        public LocationSubscriber(string name, ILogger<ObservableExample> logger) : base(name)
        {
            this.Logger = logger;
        }
        public override void OnNext(Location value)
        {
            Logger.Info($"OnNext  {value}");
        }

        public override void OnError(Exception ex)
        {
            Logger.Info($"OnError  {ex}");
        }

        public override void AfterComplited()
        {
            Logger.Info($"AfterComplited");
        }
    }

}
