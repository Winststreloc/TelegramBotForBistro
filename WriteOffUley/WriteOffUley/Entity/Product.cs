namespace WriteOffUley.Entity;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<ProductSemiFinshedProduct> ProductSemiFinshedProducts { get; set; }
    public Category Category { get; set; }
    public long CategoryId { get; set; }
}