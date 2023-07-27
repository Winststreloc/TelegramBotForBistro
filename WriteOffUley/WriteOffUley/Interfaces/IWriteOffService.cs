using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IWriteOffService
{
    Task<List<WriteOffSemiFishedProduct>> CalculateAllWriteOffProductsByDay(int day);

    Task CreateWriteOff(WriteOffProduct product);
    Task DeleteWriteOff(long productId);
}