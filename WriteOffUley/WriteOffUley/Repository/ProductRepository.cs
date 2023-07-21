using Microsoft.EntityFrameworkCore;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Repository;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetAllProduct()
    {
        return await _context.Products.Select(product => product.Name).ToListAsync();
    }

    public async Task<Product?> GetProductByName(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<Product?> GetProductById(long id)
    {
        return await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product?>> GetProductByCategoryId(long id)
    {
        return await _context.Products.Where(p => p.CategoryId == id).ToListAsync();
    }

    public async Task<List<Product?>> GetProductByCategoryName(string name)
    {
        var categoryId = await _context.Categories.SingleOrDefaultAsync(c => c.Name == name);
        return await _context.Products.Where(p => p.CategoryId == categoryId.Id).ToListAsync();
    }

    async Task<List<string?>> IProductRepository.GetProductNameByCategoryName(string name)
    {
        var categoryId = await _context.Categories.SingleOrDefaultAsync(c => c.Name == name);
        return await _context.Products.Where(p => p.CategoryId == categoryId.Id).Select(p => p.Name).ToListAsync();
    }

    public async Task<bool> ExistProduct(string name)
    {
        return await _context.Products.AnyAsync(p => p.Name == name);
    }
}