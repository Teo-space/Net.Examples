namespace Examples.Orm.EFCore.Models;


/// <summary>
/// Атрибуты объекта - кастомные свойства
/// </summary>
internal class DbObjectAttribute
{
    public Guid DbObjectAttributeId { get; set; }


    public Guid DbObjectId { get; set; }
    public DbObject DbObject { get; set; }



    public string Name { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }



}
