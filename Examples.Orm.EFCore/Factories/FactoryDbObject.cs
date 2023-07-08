using Examples.Orm.EFCore.Infrastructure;
using Examples.Orm.EFCore.Models;

namespace Examples.Orm.EFCore.Factories;

internal class FactoryDbObject(ILogger<ScopedService> logger, AppDataContext context)
{

    public async Task<Guid> Create()
    {
        var ConstructorArea = new DbSubjectArea()
        {
            DbSubjectAreaId = Guid.NewGuid(),
            Name = "Конструкторский контекст",
            Description = "Конструкторский контекст",
        };

        logger.Info("ConstructorArea Add");
        //await context.AddAsync(ConstructorArea);
        //await context.SaveChangesAsync();
        logger.Info("ConstructorArea saved");


        var ArticleType = new DbObjectType()
        {
            DbObjectTypeId = Guid.NewGuid(),
            Name = "Изделие",
            Description = "Описание изделий"
        };
        logger.Info("Object type Add");
        //await context.AddAsync(ArticleType);

        var DocumentationType = new DbObjectType()
        {
            DbObjectTypeId = Guid.NewGuid(),
            Name = "Изделие",
            Description = "Описание изделий"
        };
        logger.Info("Object type Add");
        //await context.AddAsync(DocumentationType);
        //await context.SaveChangesAsync();
        logger.Info("Object types Save");


        var CompositionOfArticlesRelationType = new DbRelationType()
        {
            DbRelationTypeId = Guid.NewGuid(),
            Name = "Состав изделий",
            Description = "связи между изделиями"
        };
        logger.Info("RelationType Add");
        //await context.AddAsync(CompositionOfArticlesRelationType);
        //await context.SaveChangesAsync();
        logger.Info("RelationType Save");

        Guid objectId = Guid.NewGuid();

        DbObject dbObject = new DbObject()
        {
            DbObjectId = objectId,
            Name = "Какое то изделие",
            Description = "Описаньице объекта",
            ObjectType = ArticleType,
            Area = ConstructorArea
        };
        logger.Info("dbObject Add");
        await context.AddAsync(dbObject);
        //await context.SaveChangesAsync();
        logger.Info("dbObject Save");

        logger.Info("Attributes Add");
        await CreateAttributes(dbObject);
        //await context.SaveChangesAsync();
        logger.Info("Attributes Save");

        logger.Info("CreateChildObjects Add");
        await CreateChildObjects(dbObject, ConstructorArea, ArticleType, CompositionOfArticlesRelationType);
        //await context.SaveChangesAsync();
        logger.Info("CreateChildObjects Save");

        return objectId;
    }


    public async Task CreateAttributes(DbObject dbObject)
    {
        var a1 = new DbObjectAttribute()
        {
            DbObjectAttributeId = Guid.NewGuid(),
            //DbObjectId = dbObject.DbObjectId,
            Name = "Имя атрибута",
            Description = "Описание",
            Value = "V"
        };
        dbObject.Attributes.Add(a1);
        logger.Info("ObjectAttribute Add");
        //await context.AddAsync(a1);

        var a2 = new DbObjectAttribute()
        {
            DbObjectAttributeId = Guid.NewGuid(),
            //DbObjectId = dbObject.DbObjectId,
            Name = "Имя другого атрибута",
            Description = "Описание другого атрибута",
            Value = "AAAAAAAAA"
        };
        dbObject.Attributes.Add(a2);
        logger.Info("ObjectAttribute Add");
        //await context.AddAsync(a2);

    }


    public async Task CreateChildObjects(DbObject dbObject, DbSubjectArea area, DbObjectType objectType, DbRelationType relationType)
    {
        for (int i = 0; i < 5; i++)
        {
            var childObject = new DbObject()
            {
                DbObjectId = Guid.NewGuid(),
                Name = $"Дочерний объект {i}",
                Description = "Описаньице дочернего объекта",

                ObjectType = objectType,

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

                Area = area
            };

            var relation = new DbRelation()
            {
                DbRelationId = Guid.NewGuid(),
                RelationType = relationType,

                //ParentObjectId = dbObject.DbObjectId,
                ParentObject = dbObject,

                //ChildrenObjectId = childObject.DbObjectId,
                ChildrenObject = childObject
            };

            //await context.AddAsync(childObject);
            //await context.AddAsync(relation);

            dbObject.Childs.Add(relation);
        }


    }



}



