using System.Text;
using Telegram.Bot.Types;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Service;

public class AnalyticsService : IAnalyticsService
{
    private readonly IUserService _userService;
    private readonly IWriteOffRepository _writeOffRepository;
    private readonly IOperationRepository _operationRepository;


    public AnalyticsService(IUserService userService, IWriteOffRepository writeOffRepository, IOperationRepository operationRepository)
    {
        _userService = userService;
        _writeOffRepository = writeOffRepository;
        _operationRepository = operationRepository;
    }
    
    public async Task<string> GetAnalytics(Update update, int days)
    {
        var users = await _userService.GetOrCreate(update);
        var message = new StringBuilder($"Списания за последние {days} дн.: \n");
        var productSemiFinishedProducts = await _writeOffRepository.GetAllWriteOffProductsByDay(days);
        var operations = await _operationRepository.GetOperationsByDay(days);
        var totalSum = operations.Sum(operation => operation.Price);
        foreach (var product in productSemiFinishedProducts)
        {
            message.AppendLine($"{product.Name} || {product.Quantity} г.");
        }
        

        message.AppendLine($"Итоговая сумма списаний: {totalSum} за {days} дней");
        return message.ToString();
    }
}