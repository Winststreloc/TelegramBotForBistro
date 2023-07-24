namespace WriteOffUley.Entity;

public class WriteOffSemiFishedProduct : BaseEntity
{
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public long? UserId { get; set; }
    public long SemiFinishedProductId { get; set; }
}