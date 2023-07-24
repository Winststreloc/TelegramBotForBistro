using Microsoft.EntityFrameworkCore;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;
using static System.DateTime;

namespace WriteOffUley.Repository;

public class OperationRepository : IOperationRepository
{
    private readonly DataContext _context;
    private readonly IProductRepository _productRepository;

    public OperationRepository(DataContext context, IProductRepository productRepository)
    {
        _context = context;
        _productRepository = productRepository;
    }

    public async Task<Operation> GetOperation(long operationId)
    {
        return await _context.Operations.SingleOrDefaultAsync(o => o.Id == operationId);
    }

    public async Task<Operation?> GetLastOperation()
    {
        return await _context.Operations.LastAsync();
    }

    public async Task<List<Operation>> GetAllOperations()
    {
        return await _context.Operations.Select(o => o).ToListAsync();
    }

    public async Task<List<Operation>> GetOperationsAllDay()
    {
        var a = await _context.Operations
            .Where(o => o.CreatedAt.Day == DateTime.Today.Day)
            .ToListAsync();
        return a;
    }

    public async Task<List<Operation>> GetOperationsByDay(int day)
    {
        var daysAgo = DateTime.Now.AddDays(-day);
        return await _context.Operations.Where(o => o.CreatedAt >= daysAgo).ToListAsync();
    }


    public async Task<bool> AddOperation(WriteOffProduct writeOffProduct, long userId)
    {
        if (!await _productRepository.ExistProduct(writeOffProduct.Name))
        {
            return false;
        }
        
        var product = await _productRepository.GetProductByName(writeOffProduct.Name);
        var operation = new Operation()
        {
            Name = writeOffProduct.Name,
            Count = writeOffProduct.Count,
            Price = product.Price * writeOffProduct.Count,
            UserId = userId,
            CreatedAt = DateTime.Now, 
            ProductId = product.Id
        };
        await _context.Operations.AddAsync(operation);
        var a = await _context.SaveChangesAsync() > 0;
        return a;
    }
    

    public async Task<bool> DeleteOperation(long operationId)
    {
        var operation = await _context.Operations.SingleAsync(o => o.Id == operationId);
        if (operation == null) return false;
        _context.Operations.Remove(operation);
        await _context.SaveChangesAsync();
        return true;
    }
}