using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands;

public class SelectCategoryCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IKeyboardService _keyboardService;

    public SelectCategoryCommand(TelegramBotService telegramBot, ICategoryRepository categoryRepository, IKeyboardService keyboardService)
    {
        _categoryRepository = categoryRepository;
        _keyboardService = keyboardService;
        _botClient = telegramBot.GetBot().Result;
    }

    public override string Name => CommandNames.SelectCategoryCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var categories = await _categoryRepository.GetAllCategoryNames();
        var keyboard = _keyboardService.CreateKeyboardButtonsInThirdColumns(categories);
        const string message = "Для добавления нового списания выберите категорию товара \n";
        var inlineKeyboard = new ReplyKeyboardMarkup(keyboard)
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(update.Message.Chat.Id, message, ParseMode.Markdown,
            replyMarkup: inlineKeyboard);
    }
}