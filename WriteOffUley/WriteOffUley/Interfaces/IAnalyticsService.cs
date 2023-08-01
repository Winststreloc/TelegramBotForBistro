using Telegram.Bots.Types;
using Update = Telegram.Bot.Types.Update;

namespace WriteOffUley.Interfaces;

public interface IAnalyticsService
{
    Task<string> GetAnalytics(Update update, int days);
    Task<string> GetWriteOffs(Update update, int days);
}