namespace Examples.Orm.EFCore.Models;


/// <summary>
/// Тип объекта
/// </summary>
internal class DbObjectType
{

    public Guid DbObjectTypeId { get; set; }

    //Сделать уникальным
    public string Name { get; set; }
    public string Description { get; set; }

}
