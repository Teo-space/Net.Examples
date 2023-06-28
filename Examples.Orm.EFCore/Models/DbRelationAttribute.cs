namespace Examples.Orm.EFCore.Models;

internal class DbRelationAttribute
{
    public Guid DbRelationAttributeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public DbRelationAttributeValueType ValueType { get; set; }

    public int IntegerValue { get; set; }
    public float FloatValue { get; set; }
    public double DoubleValue { get; set; }
    public string StringValue { get; set; }


}


