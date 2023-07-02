using Examples.Orm.EFCore.Factories;
using Examples.Orm.EFCore.Infrastructure;
using Examples.Orm.EFCore.Infrastructure.Configuration;
using Examples.Orm.EFCore.Models;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = Encoding.UTF8;

//Проверить работает ли сохранение связанных сущностей без добавления их в контекст ( через добавление в корневую)
//Добавить аттрибуты связи
//AppContext.SetSwitch

Host
.CreateDefaultBuilder(args)
.ConfigureServices(services => services.AddLogging())
.ConfigureServices(services => services.AddHostedService<ScopedService>())
//Password Stored in    secrets.json    or in envirement    or in vault
//!!!!Not in appsettings
//.AddDbMySql()
.AddDbPostgres()
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
        logger.Info($"context.Database.IsRelational() {context.Database.IsRelational()}");
        logger.Warn($".CanConnect() {context.Database.CanConnect()}");


        var factory = new FactoryDbObject(logger, context);
        var objectId = await factory.Create();
        await context.SaveChangesAsync();
        await RunWithIncludeAsync(context, objectId);
        await RunWithQueryAsync(context, objectId);

    }



    async Task RunWithIncludeAsync(AppDataContext context, Guid objectId)
    {
        //var o = await context.DbObjects
        var o = await context
            .Set<DbObject>()
            .Include(x => x.Area)
            .Include(x => x.ObjectType)
            .Include(x => x.Attributes)
            .Include(x => x.Childs).ThenInclude(r => r.RelationType)
            .Include(x => x.Childs).ThenInclude(r => r.ChildrenObject)
            .FirstOrDefaultAsync(x => x.DbObjectId == objectId);

        logger.Info($"dbObject");
        logger.Info($"dbObject : {o.DbObjectId},    {o.Name},   {o.Description}");
        logger.Info($"dbObject.ObjectType : {o.ObjectType.Name},    {o.ObjectType.Description}");
        logger.Info($"dbObject.Area : {o.Area.Name},    {o.Area.Description}");


        logger.Info($"Attributes");
        foreach (var a in o.Attributes)
        {
            logger.Info($"dbObject.Attribute : {a.Name},    {a.Value}");
        }

        logger.Info($"Childrens");
        foreach (var relation in o.Childs)
        {
            logger.Info($"rTypeName: {relation.RelationType.Name}, rChild : {relation.ChildrenObject.Name},    {relation.ChildrenObject.Description}");
        }

    }



    async Task RunWithQueryAsync(AppDataContext context, Guid objectId)
    {
        logger.Warn("RunWithQueryAsync");
        var query =
            (from DbObject in context.DbObjects
             where DbObject.DbObjectId == objectId
             join area in context.Areas
             on DbObject.AreaId equals area.DbSubjectAreaId

             join ObjectType in context.DbObjectTypes
             on DbObject.ObjectTypeId equals ObjectType.DbObjectTypeId

             let Attributes = context.DbObjectAttributes.Where(oAttr => DbObject.DbObjectId == oAttr.DbObjectId).ToList()
             let Childs =
             (
                 from Relation in context.DbRelations
                 where DbObject.DbObjectId == Relation.ParentObjectId
                 join RelationType in context.DbRelationTypes
                 on Relation.DbRelationTypeId equals RelationType.DbRelationTypeId

                 join ChildrenObject in context.DbObjects
                 on Relation.ChildrenObjectId equals ChildrenObject.DbObjectId

                 select new
                 {
                     Relation,
                     RelationType,
                     ChildrenObject
                 }
             ).ToList()
             select new
             {
                 DbObject,
                 area,
                 ObjectType,
                 Attributes,
                 Childs,
             });

        var o = await query.FirstOrDefaultAsync();

        logger.Info($"Object");
        logger.Info($"dbObject : {o.DbObject.DbObjectId},    {o.DbObject.Name},   {o.DbObject.Description}");
        logger.Info($"dbObject.area : {o.area.DbSubjectAreaId},    {o.area.Name},   {o.area.Description}");
        logger.Info($"dbObject.ObjectType : {o.ObjectType.DbObjectTypeId},    {o.ObjectType.Name},   {o.ObjectType.Description}");


        logger.Info($"Attributes");
        foreach (var a in o.Attributes)
        {
            logger.Info($"dbObject.Attribute : {a.Name},    {a.Value}");
        }

        logger.Info($"Childrens");
        foreach (var relation in o.Childs)
        {
            logger.Info($"rTypeName: {relation.RelationType.Name}, rChild : {relation.ChildrenObject.Name},    {relation.ChildrenObject.Description}");
        }
    }


}
