using Microsoft.EntityFrameworkCore;
using WriteOffUley.Entity;

namespace WriteOffUley;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Category?> Categories { get; set; }
    public DbSet<Operation?> Operations { get; set; }
    
    public DbSet<SemiFinishedProducts> SemiFinishedProducts { get; set; }
    public DbSet<Product?> Products { get; set; }
    public DbSet<ProductSemiFinshedProduct> ProductSemiFinishedProducts { get; set; }
    public DbSet<StorageRecord> Storage { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductSemiFinshedProduct>()
            .HasKey(psfp => new { ProudctId = psfp.ProductId, psfp.SemiFinishedProductId});
        modelBuilder.Entity<ProductSemiFinshedProduct>()
            .HasOne(p => p.Product)
            .WithMany(psfp => psfp.ProductSemiFinshedProducts)
            .HasForeignKey(p => p.ProductId);
        modelBuilder.Entity<ProductSemiFinshedProduct>()
            .HasOne(p => p.SemiFinishedProducts)
            .WithMany(psfp => psfp.ProductSemiFinshedProducts)
            .HasForeignKey(p => p.SemiFinishedProductId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}