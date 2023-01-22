namespace Net.Examples.ExamplesObservable;

public struct Location
{
    double latitude, longitude;

    public Location(double latitude, double longitude)
    {
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public double Latitude
    {
        get => latitude;
    }

    public double Longitude
    {
        get => longitude;
    }
}
