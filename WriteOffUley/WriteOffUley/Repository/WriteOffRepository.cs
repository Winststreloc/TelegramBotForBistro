using Microsoft.EntityFrameworkCore;
using WriteOffUley.Entity;
using WriteOffUley.Entity.Dto;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Repository;

public class WriteOffRepository : IWriteOffRepository
{
    private readonly DataContext _context;

    public WriteOffRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<SemiFinishedProduct>> GetAllSemiFinishedProductsByProduct(Product product)
    {
        return await _context.SemiFinishedProducts
            .Include(sfp => sfp.ProductSemiFinshedProducts
                .Where(p => p.ProductId == product.Id))
            .ToListAsync();
    }

    public async Task<List<WriteOffSemiFishedProduct>> GetAllWriteOffProductsByDay(int day)
    {
        var daysAgo = DateTime.Now.AddDays(-day);
        return await _context.WriteOffSemiFishedProducts.Where(wosfp => wosfp.CreatedAt >= daysAgo).ToListAsync();
    }

    public async Task<List<ProductSemiFinishedProduct>> GetAllRecipe(long productId)
    {
        return await _context.ProductSemiFinishedProducts.Where(p => p.ProductId == productId).ToListAsync();
    }

    public async Task<SemiFinishedProduct?> GetSemiFinishedProduct(long semiFinishedProductId)
    {
        return await _context.SemiFinishedProducts.SingleOrDefaultAsync(p => p.Id == semiFinishedProductId);
    }

    public async Task<bool> AddWriteOff(SemiFinishedProduct semiFinishedProduct, int count, decimal quantity)
    {
        
        await _context.WriteOffSemiFishedProducts.AddAsync(new WriteOffSemiFishedProduct()
        {
            CreatedAt = DateTime.Now,
            Name = semiFinishedProduct.Name,
            SemiFinishedProductId = semiFinishedProduct.Id,
            Quantity = quantity * count
        });
        // if (WriteOffExist(semiFinishedProduct))
        // {
        //     var writeOffSemiFishedProducts =
        //         await _context.WriteOffSemiFishedProducts.SingleOrDefaultAsync(product =>
        //             product.SemiFinishedProductId == semiFinishedProduct.Id);
        //     if (writeOffSemiFishedProducts != null) writeOffSemiFishedProducts.Quantity += quantity * count;
        //     await _context.SaveChangesAsync();
        // }
        // else
        // {
        //     
        // }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveWriteOff(SemiFinishedProduct semiFinishedProduct, int count, decimal quantity)
    {
        var writeOff = await _context.WriteOffSemiFishedProducts
            .Where(wosfp => 
                            wosfp.SemiFinishedProductId == semiFinishedProduct.Id &&
                            wosfp.CreatedAt.Day == DateTime.Today.Day).FirstOrDefaultAsync();
        if (writeOff == null)
        {
            return false;
        }

        _context.Remove(writeOff);
        return await _context.SaveChangesAsync() > 0;
    }

    public bool WriteOffExist(SemiFinishedProduct semiFinished)
    {
        return _context.WriteOffSemiFishedProducts.Any(product =>
            product.Name == semiFinished.Name && product.SemiFinishedProductId == semiFinished.Id);
    }
}