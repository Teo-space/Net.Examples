using Examples.Orm.EFCore.Factories;
using Examples.Orm.EFCore.Infrastructure;
using Examples.Orm.EFCore.Models;
using Microsoft.EntityFrameworkCore;

Host
.CreateDefaultBuilder(args)
.ConfigureServices(services => services.AddLogging())
.ConfigureServices(services => services.AddHostedService<ScopedService>())
.ConfigureServices(services => services.AddDbContext<AppDataContext>(options =>
{
    //options.LogTo(Console.WriteLine);
    options.UseInMemoryDatabase("Application");

    //options.UseSqlite($"Data Source=AppDataContext.db");
    //options.UseNpgsql("User ID=postgres;Password=111111111;Host=localhost;Port=5432;Database=postgres;providerName=PostgreSQL")
}))
.Build()
.Run()
;


internal class ScopedService(IServiceScopeFactory serviceScopeFactory, 
    ILogger<ScopedService> logger, 
    IServiceProvider serviceProvider) 
    

    : ScopedServiceBase(serviceScopeFactory)
{

    public override async Task Scope(IServiceProvider serviceProvider, CancellationToken token)
    {
        logger.Info("Scope");

        using AppDataContext context = serviceProvider.GetRequiredService<AppDataContext>();

        var factory = new DbObjectFactory(logger, context);
        var objectId = await factory.Create();

        {
            var o = await context.DbObjects
                                            .Include(x => x.Area)
                                            .Include(x => x.ObjectType)
                                            .Include(x => x.Childrens)
                                                    .ThenInclude(r => r.RelationType)
                                            .Include(x => x.Childrens)
                                                    .ThenInclude(r => r.ChildrenObject)
                                            .SingleOrDefaultAsync(x => x.DbObjectId == objectId);

            logger.Info($"dbObject : {o.DbObjectId},    {o.Name},   {o.Description}");
            logger.Info($"dbObject.ObjectType : {o.ObjectType.Name},    {o.ObjectType.Description}");
            logger.Info($"dbObject.Area : {o.Area.Name},    {o.Area.Description}");

            foreach (var a in o.Attributes)
            {
                logger.Info($"dbObject.Attribute : {a.Name},    {a.Value}");
            }

            foreach (var relation in o.Childrens)
            {
                logger.Info($"rTypeName: {relation.RelationType.Name}, rChild : {relation.ChildrenObject.Name},    {relation.ChildrenObject.Description}");
            }
        }
        {
            logger.Info($"Query");

            var query =
            (from DbObject in context.DbObjects
             join area in context.Areas 
             on DbObject.AreaId equals area.DbSubjectAreaId

             join ObjectType in context.DbObjectTypes 
             on DbObject.ObjectTypeId equals ObjectType.DbObjectTypeId

             join ChildRelation in context.DbRelations 
             on DbObject.DbObjectId equals ChildRelation.ParentObjectId into ChildRelations
             from ChildRelation in ChildRelations.DefaultIfEmpty()

             join RelationType in context.DbRelationTypes 
             on ChildRelation.DbRelationTypeId equals RelationType.DbRelationTypeId into RelationTypes
             from RelationType in RelationTypes.DefaultIfEmpty()

             join ChildrenObject in context.DbObjects 
             on ChildRelation.ChildrenObjectId equals ChildrenObject.DbObjectId into ChildrenObjects
             from ChildrenObject in ChildrenObjects.DefaultIfEmpty()

             select new
             {
                 DbObject,
                 area,
                 ObjectType,
                 ChildRelation,
                 RelationType,
                 ChildrenObject
             }
            ).ToList();

            foreach(var o in query)
            {
                logger.Info($"Object");
                logger.Info($"dbObject : {o.DbObject.DbObjectId},    {o.DbObject.Name},   {o.DbObject.Description}");
                logger.Info($"dbObject.area : {o.area.DbSubjectAreaId},    {o.area.Name},   {o.area.Description}");
                logger.Info($"dbObject.ObjectType : {o.ObjectType.DbObjectTypeId},    {o.ObjectType.Name},   {o.ObjectType.Description}");

                //Can be null
                {
                    logger.Info($"relation : {o?.ChildRelation?.ParentObjectId},    {o?.ChildRelation?.ChildrenObjectId}");
                    logger.Info($"ChildrenObject : {o?.ChildrenObject?.DbObjectId},    {o?.ChildrenObject?.Name},   {o?.ChildrenObject?.Description}");
                }

            }
        }



        {
            logger.Info($"Total Objects");
            foreach (var d in context.DbObjects)
            {
                logger.Info($"dbObject : {d.DbObjectId},    {d.Name},   {d.Description}");
            }

            logger.Info($"Total DbRelations");
            foreach (var r in context.DbRelations)
            {
                logger.Info($"DbRelation : {r.DbRelationId},    {r.ParentObjectId},     {r.ChildrenObjectId}");
            }
        }



    }


}
