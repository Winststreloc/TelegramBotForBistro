namespace WriteOffUley.Entity;

public class Operation : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
    public long? UserId { get; set; }
}