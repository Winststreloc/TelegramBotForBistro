namespace WriteOffUley.Entity;

public class ProductSemiFinshedProduct
{
    public long ProductId { get; set; }
    public long SemiFinishedProductId { get; set; }
    public decimal Quantity { get; set; }
    public Product? Product { get; set; }
    public SemiFinishedProducts? SemiFinishedProducts { get; set; }
    
}