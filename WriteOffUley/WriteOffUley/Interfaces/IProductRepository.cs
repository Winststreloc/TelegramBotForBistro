using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IProductRepository
{
    Task<List<string>> GetAllProduct();
    Task<Product?> GetProductByName(string name);
    Task<Product?> GetProductById(long id);
    Task<List<Product?>> GetProductByCategoryId(long id);
    Task<List<Product?>> GetProductByCategoryName(string name);
    Task<List<string?>> GetProductNameByCategoryName(string name);
    Task<bool> ExistProduct(string name);
}