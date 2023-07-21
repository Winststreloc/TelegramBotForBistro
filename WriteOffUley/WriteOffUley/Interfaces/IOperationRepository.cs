using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IOperationRepository
{
    Task<Operation> GetOperation(long operationId);
    Task<Operation?> GetLastOperation();
    Task<List<Operation>> GetAllOperations();
    Task<List<Operation>> GetOperationsAllDay();
    Task<bool> AddOperation(WriteOffProduct writeOffProduct, long userId);
    Task<bool> DeleteOperation(Operation operation);
    Task<bool> DeleteOperation(long operationId);
}