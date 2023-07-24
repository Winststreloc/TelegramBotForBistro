using Microsoft.EntityFrameworkCore;
using WriteOffUley.Entity;

namespace WriteOffUley;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Operation> Operations { get; set; }

    public DbSet<SemiFinishedProduct> SemiFinishedProducts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSemiFinishedProduct> ProductSemiFinishedProducts { get; set; }
    public DbSet<StorageRecord> Storage { get; set; }
    public DbSet<WriteOffSemiFishedProduct> WriteOffSemiFishedProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductSemiFinishedProduct>()
            .HasKey(psfp => new { ProudctId = psfp.ProductId, psfp.SemiFinishedProductId });
        modelBuilder.Entity<ProductSemiFinishedProduct>()
            .HasOne(p => p.Product)
            .WithMany(psfp => psfp.ProductSemiFinshedProducts)
            .HasForeignKey(p => p.ProductId);
        modelBuilder.Entity<ProductSemiFinishedProduct>()
            .HasOne(psfp => psfp.SemiFinishedProduct)
            .WithMany(p => p.ProductSemiFinshedProducts)
            .HasForeignKey(psfp => psfp.SemiFinishedProductId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}