namespace WriteOffUley.Entity;

public class SemiFinishedProducts : BaseEntity
{
    public string Name { get; set; }
    public decimal Weight { get; set; }
    public ICollection<ProductSemiFinshedProduct> ProductSemiFinshedProducts { get; set; }
}