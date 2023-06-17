namespace Examples.GenericRepository;

class EntityRepository(GenericRepoDbContext context)    :   GenericRepositoryBase<Entity, Guid>(context);