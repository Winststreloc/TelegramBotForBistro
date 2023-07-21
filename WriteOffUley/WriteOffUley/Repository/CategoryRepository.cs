using Microsoft.EntityFrameworkCore;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Category?>> GetAllCategory()
    {
        return await _context.Categories.Select(c => c).ToListAsync();
    }

    public async Task<List<string>> GetAllCategoryNames()
    {
        return await _context.Categories.OrderBy(c => c.Id).Select(c => c.Name).ToListAsync();
    }

    public async Task<Category?> GetCategoryById(long id)
    {
        return await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> GetCategoryByName(string name)
    {
        return await _context.Categories.SingleOrDefaultAsync(c => c.Name == name);
    }
}