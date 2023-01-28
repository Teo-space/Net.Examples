namespace Net.Examles.Examples.DependencyInjection;



public interface IGenericRepository<T, TId>
{
    public T Get(TId Id);
    public Guid Add(T item);
    public void Update(TId Id, T item);
    public void Delete(TId Id);
}



