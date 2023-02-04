using Net.Examles.ExamplesObservable.Base;
using Net.Examples.ExamplesObservable;


namespace Net.Examles.ExamplesObservable;



public record ObservableExample(ILogger<ObservableExample> logger) : Handler
{
    void print(string message) => logger.Info(message);


    public async Task Handle(CancellationToken token)
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
