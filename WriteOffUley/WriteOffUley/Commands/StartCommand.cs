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
    private readonly IKeyboardService _keyboardService;

    public StartCommand(IUserService userService, TelegramBotService telegramBot, IKeyboardService keyboardService)
    {
        _userService = userService;
        _keyboardService = keyboardService;
        _botClient = telegramBot.GetBot().Result;
    }

    public override string Name => CommandNames.StartCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var user = await _userService.GetOrCreate(update);
        var inlineKeyboard = _keyboardService.GetKeyboardMarkup(update);
        await _botClient.SendTextMessageAsync(user.ChatId,
            "Добро пожаловать! Я буду вести учёт списаний в Uley! " +
            "\nЕсли какого-то продукта нет, то вы можете его нажав на кнопку 'Добавить списание', а потом 'Добавить'" +
            "\nЕсли вы нажали что-то не то - введите команду '/start'",
            replyMarkup: inlineKeyboard);
    }
}