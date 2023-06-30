using Examples.Orm.EFCore.Infrastructure;
using Examples.Orm.EFCore.Models;

namespace Examples.Orm.EFCore.Factories;

internal class DbObjectFactory(ILogger<ScopedService> logger, AppDataContext context)
{

    public async Task<Guid> Create()
    {
        var ConstructorArea = new DbSubjectArea()
        {
            DbSubjectAreaId = Guid.NewGuid(),
            Name = "Конструкторский контекст",
            Description = "Конструкторский контекст",


        };

        await context.AddAsync(ConstructorArea);
        await context.SaveChangesAsync();
        logger.Info("ConstructorArea saved");


        var ArticleType = new DbObjectType()
        {
            DbObjectTypeId = Guid.NewGuid(),
            Name = "Изделие",
            Description = "Описание изделий"
        };

        var DocumentationType = new DbObjectType()
        {
            DbObjectTypeId = Guid.NewGuid(),
            Name = "Изделие",
            Description = "Описание изделий"
        };


        await context.AddAsync(ArticleType);
        await context.AddAsync(DocumentationType);
        await context.SaveChangesAsync();
        logger.Info("Object types saved");


        var CompositionOfArticlesRelationType = new DbRelationType()
        {
            DbRelationTypeId = Guid.NewGuid(),
            Name = "Состав изделий",
            Description = "связи между изделиями"
        };
        await context.AddAsync(CompositionOfArticlesRelationType);
        await context.SaveChangesAsync();


        Guid objectId = Guid.NewGuid();

        DbObject dbObject = new DbObject()
        {
            DbObjectId = objectId,
            Name = "Какое то изделие",
            Description = "Описаньице объекта",


            ObjectType = ArticleType,

            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,


            Area = ConstructorArea
        };

        dbObject.Attributes.Add(new DbObjectAttribute()
        {
            Name = "Имя атрибута",
            Description = "Описание",
            Value = "V"
        });

        dbObject.Attributes.Add(new DbObjectAttribute()
        {
            Name = "Имя другого атрибута",
            Description = "Описание другого атрибута",
            Value = "AAAAAAAAA"
        });

        await context.AddAsync(dbObject);
        await context.SaveChangesAsync();
        logger.Info("dbObject saved");

        //Create children objects
        {
            for (int i = 0; i < 5; i++)
            {
                var childObject = new DbObject()
                {
                    DbObjectId = Guid.NewGuid(),
                    Name = $"Дочерний объект {i}",
                    Description = "Описаньице дочернего объекта",

                    ObjectType = ArticleType,

                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,

                    Area = ConstructorArea
                };
                await context.AddAsync(childObject);


                var relation = new DbRelation()
                {
                    DbRelationId = Guid.NewGuid(),
                    RelationType = CompositionOfArticlesRelationType,
                    ParentObject = dbObject,
                    ChildrenObject = childObject
                };
                await context.AddAsync(relation);


                dbObject.Childrens.Add(relation);
            }

            await context.SaveChangesAsync();
        }


        return objectId;
    }

}



