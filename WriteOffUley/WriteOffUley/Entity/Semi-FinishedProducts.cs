namespace WriteOffUley.Entity;

public class SemiFinishedProducts : BaseEntity
{
    public string Name { get; set; }
    public bool LiquidOrSolid { get; set; }
    public ICollection<ProductSemiFinshedProduct> ProductSemiFinshedProducts { get; set; }
}