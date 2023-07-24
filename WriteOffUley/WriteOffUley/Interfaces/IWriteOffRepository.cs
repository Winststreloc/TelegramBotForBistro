using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IWriteOffRepository
{
    Task<List<SemiFinishedProduct>> GetAllSemiFinishedProductsByProduct(Product product);
    Task<List<WriteOffSemiFishedProduct>> GetAllWriteOffProductsByDay(int day);
    Task<List<ProductSemiFinishedProduct>> GetAllRecipe(long productId);
    Task<SemiFinishedProduct?> GetSemiFinishedProduct(long semiFinishedProductId);
    Task<bool> AddWriteOff(SemiFinishedProduct semiFinishedProduct, int count, decimal quantity);
    Task<bool> RemoveWriteOff(SemiFinishedProduct semiFinishedProduct, int count, decimal quantity);
}