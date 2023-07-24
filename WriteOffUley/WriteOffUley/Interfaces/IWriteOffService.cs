using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IWriteOffService
{
    Task CreateWriteOff(WriteOffProduct product);
    Task DeleteWriteOff(long productId);
}