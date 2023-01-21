
using Net.Observable.Reactive.Examples.ObservableTests.Base;
using Net.Observable.Reactive.Examples.ObservableTests;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Net.Examles.ExamplesObservable;



public class LocationPublisher : Publisher<Location>
{
}


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




public class ObservableExample : BackgroundService
{
    private readonly ILogger<ObservableExample> logger;

    public ObservableExample(ILogger<ObservableExample> logger)
    {
        this.logger = logger;
    }


    void print(string message) => logger.Info(message);


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        LocationPublisher publisher = new LocationPublisher();

        LocationSubscriber subscriber = new LocationSubscriber("GPS", logger);
        subscriber.Subscribe(publisher);


        publisher.Publish(new Location(47.6456, -122.1312));
        publisher.Publish(new Location(47.6677, -122.1199));

        subscriber.OnCompleted();
        publisher.Publish(new Location(47.6677, -122.1222));


        publisher.Completed();
    }


}
