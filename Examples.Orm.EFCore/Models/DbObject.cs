namespace Examples.Orm.EFCore.Models;


/// <summary>
/// Универсальный объект
/// </summary>
internal class DbObject
{
    public Guid DbObjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public Guid ObjectTypeId { get; set; }
    public DbObjectType ObjectType { get; set; }


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}

    //public DateTime CreatedBy { get; set;}
    //public DateTime UpdatedBy { get; set;}

    public Guid AreaId { get; set; }
    public DbSubjectArea Area { get; set; }




    public List<DbObjectAttribute> Attributes { get; set;} = new();


    public DbRelation Parent { get; set; } = new();

    public List<DbRelation> Childrens { get; set;} = new();





}
