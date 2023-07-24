namespace WriteOffUley.Entity;

public class SemiFinishedProduct : BaseEntity
{
    public string Name { get; set; }
    public bool LiquidOrSolid { get; set; }
    public ICollection<ProductSemiFinishedProduct> ProductSemiFinshedProducts { get; set; }
}