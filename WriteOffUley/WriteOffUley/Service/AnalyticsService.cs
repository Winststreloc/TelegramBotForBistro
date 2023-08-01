using System.Text;
using Telegram.Bot.Types;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Service;

public class AnalyticsService : IAnalyticsService
{
    private readonly IUserService _userService;
    private readonly IWriteOffService _writeOffService;
    private readonly IOperationRepository _operationRepository;


    public AnalyticsService(IUserService userService, IOperationRepository operationRepository, IWriteOffService writeOffService)
    {
        _userService = userService;
        _operationRepository = operationRepository;
        _writeOffService = writeOffService;
    }
    
    public async Task<string> GetAnalytics(Update update, int days)
    {
        var users = await _userService.GetOrCreate(update);
        var message = new StringBuilder($"Списания за последние {days} дн.: \n");
        var productSemiFinishedProducts = await _writeOffService.CalculateAllWriteOffProductsByDay(days);
        var operations = await _operationRepository.GetOperationsByDay(days);
        var totalSum = operations.Sum(operation => operation.Price);
        foreach (var product in productSemiFinishedProducts)
        {
            message.AppendLine($"{product.Name} || {product.Quantity} г.");
        }
        

        message.AppendLine($"Итоговая сумма списаний: {totalSum} за {days} дней");
        return message.ToString();
    }

    public async Task<string> GetWriteOffs(Update update, int days)
    {
        var users = await _userService.GetOrCreate(update);
        var message = new StringBuilder($"Списания за последние {days} дн.: \n");
        var operations = await _operationRepository.GetOperationsByDay(days);
        var totalSum = operations.Sum(operation => operation.Price);
        message.AppendLine(await GetOperationsByDayFormatted(operations));
        message.AppendLine($"Итоговая сумма списаний: {totalSum} за {days} дней");
        return message.ToString();
    }
    
    public async Task<string> GetOperationsByDayFormatted(List<Operation> operations)
    {
        var groupedOperations = operations.GroupBy(o => o.CreatedAt.Date)
            .Select(g => new
            {
                Date = g.Key.ToString("dd.MM"),
                Operations = string.Join("\n", g.Select(op => op.Name))
            })
            .ToList();

        var result = string.Join(Environment.NewLine, groupedOperations.Select(g =>
            $"{g.Date} {g.Operations}"));

        return result;
    }
}