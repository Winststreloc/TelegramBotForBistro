using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category?>> GetAllCategory();
    Task<List<string>> GetAllCategoryNames();
    Task<Category?> GetCategoryById(long id);
    Task<Category> GetCategoryByName(string name);
}