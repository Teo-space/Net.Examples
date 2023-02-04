public interface Handler
{
    public Task Handle(CancellationToken token);
}