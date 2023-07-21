using Telegram.Bot.Types;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Service;

public class AnalyticsService : IAnalyticsService
{
    private readonly IUserService _userService;
    private readonly IOperationService _operationService;

    public AnalyticsService(IUserService userService, IOperationService operationService)
    {
        _userService = userService;
        _operationService = operationService;
    }
    
    public async Task<string> GetAnalytics(Update update, int days)
    {
        var users = await _userService.GetOrCreate(update);
        throw new NotImplementedException();
    }
}