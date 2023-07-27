using Telegram.Bot;
using Telegram.Bot.Types;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands.CreateNewProductForWriteOff;

public class CreateNewProduct : BaseCommand
{
    private readonly TelegramBotClient _telegramBot;
    private readonly IKeyboardService _keyboardService;

    public CreateNewProduct(TelegramBotService telegramBot, IKeyboardService keyboardService)
    {
        _keyboardService = keyboardService;
        _telegramBot = telegramBot.GetBot().Result;
    }

    public override string Name => CommandNames.CreateNewProduct;

    public override async Task ExecuteAsync(Update update)
    {
        var inlineKeyboard = _keyboardService.GetKeyboardMarkup(update);
        await _telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Эта фича в разработке",
            replyMarkup: inlineKeyboard);
    }
}