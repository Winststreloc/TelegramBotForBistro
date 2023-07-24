namespace WriteOffUley.Entity;

public class ProductSemiFinishedProduct
{
    public long ProductId { get; set; }
    public long SemiFinishedProductId { get; set; }
    public decimal Quantity { get; set; }
    public Product? Product { get; set; }
    public SemiFinishedProduct? SemiFinishedProduct { get; set; }
    
}