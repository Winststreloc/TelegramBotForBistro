using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands.OpenWriteOffDay;

public class GetWriteOffsCommand : BaseCommand
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly IUserService _userService;
    private readonly IAnalyticsService _analyticsService;

    public GetWriteOffsCommand(TelegramBotService telegramBotClient, IUserService userService, IAnalyticsService analyticsService)
    {
        _telegramBotClient = telegramBotClient.GetBot().Result;
        _userService = userService;
        _analyticsService = analyticsService;
    }

    public override string Name => CommandNames.GetWriteOffsCommand;
    public override async Task ExecuteAsync(Update update)
    {
        var user = await _userService.GetOrCreate(update);
        var daysString = update.CallbackQuery?.Data?.Replace("analytic-", "") ?? "0";
        var days = int.Parse(daysString);

        var message = await _analyticsService.GetWriteOffs(update, days);
        await _telegramBotClient.SendTextMessageAsync(user.ChatId, message, ParseMode.Markdown);
    }
}