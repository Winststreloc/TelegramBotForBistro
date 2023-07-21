using Microsoft.EntityFrameworkCore;
using Telegram.Bots.Types;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Repository;

public class WriteOffRepository : IWriteOffRepository
{
    private readonly DataContext _context;

    public WriteOffRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Operation>> GetAllWriteOffDay()
    {
        return  await _context.Operations.Where(o => o.CreatedAt.Day == DateTime.Today.Day).ToListAsync();
    }
    
}