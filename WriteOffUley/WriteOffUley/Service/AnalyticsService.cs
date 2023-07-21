using Telegram.Bot.Types;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Service;

public class AnalyticsService : IAnalyticsService
{
    private readonly IUserService _userService;
    private readonly IKeyboardService _keyboardService;

    public AnalyticsService(IUserService userService, KeyboardService keyboardService)
    {
        _userService = userService;
        _keyboardService = keyboardService;
    }
    
    public async Task<string> GetAnalytics(Update update, int days)
    {
        var users = await _userService.GetOrCreate(update);
        throw new NotImplementedException();
    }
}