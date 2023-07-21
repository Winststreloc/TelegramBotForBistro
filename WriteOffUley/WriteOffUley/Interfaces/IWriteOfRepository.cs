using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IWriteOffRepository
{
    Task<List<Operation>> GetAllWriteOffDay();
}