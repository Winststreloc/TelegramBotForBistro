using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands;

public class SelectProductCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly IProductRepository _productRepository;
    private readonly IKeyboardService _keyboardService;

    public SelectProductCommand(TelegramBotService telegramBot,
        IProductRepository productRepository, IKeyboardService keyboardService)
    {
        _productRepository = productRepository;
        _keyboardService = keyboardService;
        _botClient = telegramBot.GetBot().Result;
    }

    public override string Name => CommandNames.SelectProductCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var message = update.Message;
        var list = await _productRepository.GetProductNameByCategoryName(message.Text);
        if (list.Count != 0)
        {
            var keyboard = _keyboardService.CreateKeyboardButtonsInThirdColumns(
                list);
            var inlineKeyboard = new ReplyKeyboardMarkup(keyboard);
            await _botClient.SendTextMessageAsync(update.Message.Chat,
                "Выберите нужный продукт.",
                replyMarkup: inlineKeyboard);
        }
        else
        {
            //_executor.ExecuteCommand(CommandNames.StartCommand, update);
            var inlineKeyboard = _keyboardService.GetKeyboardMarkup(update);
            await _botClient.SendTextMessageAsync(update.Message.Chat,
                "Эта категория сейчас в разработке", replyMarkup: inlineKeyboard);
        }
    }
}