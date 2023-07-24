using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IOperationRepository
{
    Task<Operation> GetOperation(long operationId);
    Task<Operation?> GetLastOperation();
    Task<List<Operation>> GetAllOperations();
    Task<List<Operation>> GetOperationsAllDay();
    Task<List<Operation>> GetOperationsByDay(int day);
    Task<bool> AddOperation(WriteOffProduct writeOffProduct, long userId);
    Task<bool> DeleteOperation(long operationId);
}