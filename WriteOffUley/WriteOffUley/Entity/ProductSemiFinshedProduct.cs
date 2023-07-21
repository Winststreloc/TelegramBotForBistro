namespace WriteOffUley.Entity;

public class ProductSemiFinshedProduct
{
    public long ProudctId { get; set; }
    public long SemiFinishedProductId { get; set; }
    public Product Product { get; set; }
    public SemiFinishedProducts SemiFinishedProducts { get; set; }
}