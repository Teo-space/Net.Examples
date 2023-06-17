using System.Linq.Expressions;


namespace Examples.GenericRepository;


public interface IGenericRepository<TEntity, TId>
    : IGenericSelect<TEntity, TId>, IGenericSave<TEntity, TId>, IGenericRemove<TEntity, TId>
    where TEntity : class
{

}

public interface IGenericSelect<TEntity, TId> where TEntity : class
{
    public IEnumerable<TEntity> GetAll();
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicateWhere);
    public TEntity? GetById(TId Id);
    public int Count();
}

public interface IGenericSave<TEntity, TId> where TEntity : class
{
    public bool Create(TEntity entity);
    public bool Update(TEntity entity);
    public bool Save(TEntity entity);
}

public interface IGenericRemove<TEntity, TId> where TEntity : class
{
    public bool RemoveByid(Guid Id);
    public bool RemoveRange(params TEntity[] entities);
    public bool RemoveWhere(Expression<Func<TEntity, bool>> predicateWhere);
}