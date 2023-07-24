using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands.AnaliticsWriteOffCommand;

public class SelectAnalyticsCommand : BaseCommand
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly IUserService _userService;
    private readonly IAnalyticsService _analyticsService;

    public SelectAnalyticsCommand(TelegramBotService botClient, IUserService userService, IAnalyticsService analyticsService)
    {
        _userService = userService;
        _analyticsService = analyticsService;
        _telegramBotClient = botClient.GetBot().Result;
    }

    public override string Name => CommandNames.SelectAnalyticsCommand;
    public override async Task ExecuteAsync(Update update)
    {
        var user = await _userService.GetOrCreate(update);
        var daysString = update.CallbackQuery?.Data?.Replace("analytic-", "") ?? "0";
        var days = int.Parse(daysString);

        var message = await _analyticsService.GetAnalytics(update, days);
        await _telegramBotClient.SendTextMessageAsync(user.ChatId, message, ParseMode.Markdown);
    }
}