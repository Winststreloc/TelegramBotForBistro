using WriteOffUley.Entity;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Service;

public class WriteOffService : IWriteOffService
{
    private readonly IProductRepository _productRepository;
    private readonly IWriteOffRepository _writeOffRepository;
    private readonly IOperationRepository _operationRepository;

    public WriteOffService(IProductRepository productRepository, IWriteOffRepository writeOffRepository, IOperationRepository operationRepository)
    {
        _productRepository = productRepository;
        _writeOffRepository = writeOffRepository;
        _operationRepository = operationRepository;
    }

    public async Task<List<WriteOffSemiFishedProduct>> CalculateAllWriteOffProductsByDay(int day)
    {
        var listWriteOff = await _writeOffRepository.GetAllWriteOffProductsByDay(day);
        var calcListWriteOffByDay = new Dictionary<string, WriteOffSemiFishedProduct>();

        foreach (var writeOff in listWriteOff)
        {
            if (calcListWriteOffByDay.ContainsKey(writeOff.Name))
            {
                calcListWriteOffByDay[writeOff.Name].Quantity += writeOff.Quantity;
            }
            else
            {
                calcListWriteOffByDay.Add(writeOff.Name, writeOff);
            }
        }

        return calcListWriteOffByDay.Values.ToList();
    }

//kom
    public async Task CreateWriteOff(WriteOffProduct writeOffProduct)
    {
        var product = await _productRepository.GetProductByName(writeOffProduct.Name);
        if (product == null)
        {
            return;
        }

        var productSemiFinishedProducts = await _writeOffRepository.GetAllRecipe(product.Id);
        foreach (var psfp in productSemiFinishedProducts)
        {
            var semiFinishedProduct = await _writeOffRepository.GetSemiFinishedProduct(psfp.SemiFinishedProductId);
            if (semiFinishedProduct != null)
                await _writeOffRepository.AddWriteOff(semiFinishedProduct, writeOffProduct.Count, psfp.Quantity);
        }
    }

    public async Task DeleteWriteOff(long operationId)
    {
        var operation = await _operationRepository.GetOperation(operationId);
        var product = await _productRepository.GetProductById(operation.ProductId);
        if (product == null)
        {
            return;
        }

        var productSemiFinishedProducts = await _writeOffRepository.GetAllRecipe(product.Id);
        foreach (var psfp in productSemiFinishedProducts)
        {
            var semiFinishedProduct = await _writeOffRepository.GetSemiFinishedProduct(psfp.SemiFinishedProductId);
            await _writeOffRepository.RemoveWriteOff(semiFinishedProduct, operation.Count, psfp.Quantity);
        }
    }
}