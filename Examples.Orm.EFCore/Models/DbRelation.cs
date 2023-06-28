namespace Examples.Orm.EFCore.Models;


/// <summary>
/// Связи между объектами
/// </summary>
internal class DbRelation
{
    public Guid DbRelationId { get; set; }

    //public string RelationType { get; set; }




    public Guid ParentObjectId { get; set; }
    public DbObject ParentObject { get; set; }


    public Guid ChildrenObjectId { get; set; }
    public DbObject ChildrenObject { get; set; }



}



