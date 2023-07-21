using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands;

public class StartCommand : BaseCommand
{
    private readonly IUserService _userService;
    private readonly TelegramBotClient _botClient;
    private readonly IOperationService _operationService;

    public StartCommand(IUserService userService, TelegramBotService telegramBot, IOperationService operationService)
    {
        _userService = userService;
        _operationService = operationService;
        _botClient = telegramBot.GetBot().Result;
    }

    public override string Name => CommandNames.StartCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var user = await _userService.GetOrCreate(update);
        var inlineKeyboard = _operationService.GetKeyboardMarkup(update);
        await _botClient.SendTextMessageAsync(user.ChatId,
            "Добро пожаловать! Я буду вести учёт списаний в Uley!",
            replyMarkup: inlineKeyboard);
    }
}