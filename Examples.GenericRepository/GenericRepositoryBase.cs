using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Examples.GenericRepository;


class GenericRepositoryBase<TEntity, TId>(DbContext context)
    : IGenericRepository<TEntity, TId>

    where TEntity : class
{
    public IEnumerable<TEntity> GetAll() => context.Set<TEntity>().ToList().AsReadOnly();


    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicateWhere) =>
        context.Set<TEntity>().Where(predicateWhere).ToList().AsReadOnly();

    public TEntity? GetById(TId Id)
        => context.Find<TEntity>(Id);

    public int Count()
        => context.Set<TEntity>().Count();




    public bool Create(TEntity entity)
    {
        context.Add(entity);
        return context.SaveChanges() > 0;
    }


    public bool Update(TEntity entity)
    {
        context.Update(entity);
        return context.SaveChanges() > 0;
    }

    public bool Save(TEntity entity)
    {
        context.Attach(entity);
        return context.SaveChanges() > 0;
    }


    public bool RemoveByid(Guid Id)
    {
        var e = context.Find<TEntity>(Id);
        if (e != default)
        {
            //context.Set<Entity>().Remove(e);
            context.Remove(e);
            return context.SaveChanges() > 0;
        }
        return false;
    }

    public bool RemoveRange(params TEntity[] entities)
    {
        context.RemoveRange(entities);
        //context.Set<Entity>().RemoveRange
        return context.SaveChanges() > 0;
    }


    public bool RemoveWhere(Expression<Func<TEntity, bool>> predicateWhere)
    {
        var entitiesToRemove = context.Set<TEntity>().Where(predicateWhere).ToArray();
        if (entitiesToRemove.Length > 0)
        {
            context.RemoveRange(entitiesToRemove);
            return context.SaveChanges() > 0;
        }
        return true;
    }




}

