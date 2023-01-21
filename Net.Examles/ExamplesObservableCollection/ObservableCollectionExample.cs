using Cryptography;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Net.Examles.ExamplesObservableCollection;


public class ObservableCollectionExample : BackgroundService
{
    private readonly ILogger<ObservableCollectionExample> logger;

    public ObservableCollectionExample(ILogger<ObservableCollectionExample> logger)
    {
        this.logger = logger;
    }


    void print(string message) => logger.Info(message);


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        ObservableCollection<string> peoples = new ObservableCollection<string>(new string[] { "Tom", "Bob", "Sam" });
        peoples.CollectionChanged += OnCollectionChanged;

        peoples.Add("John");
        peoples.Insert(0, "Jack");
        peoples[0] = "Ron";
        peoples.Remove("Ron");
        peoples.Clear();
    }


    void OnCollectionChanged(object? o, NotifyCollectionChangedEventArgs args)
    {
        print($"CollectionChanged: {args.Action}");
        print($"NewItems: {args?.NewItems?.Count ?? 0}. {args?.NewItems?.OfType<string>().ToDelimitedString(", ")}");
        print($"OldItems: {args?.OldItems?.Count ?? 0}. {args?.OldItems?.OfType<string>().ToDelimitedString(", ")}");

    }
}
