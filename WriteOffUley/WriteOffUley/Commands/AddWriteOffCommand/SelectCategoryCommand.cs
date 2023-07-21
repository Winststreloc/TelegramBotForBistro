using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands;

public class SelectCategoryCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly IProductRepository _productRepository;
    private readonly IOperationService _operationService;

    public SelectCategoryCommand(TelegramBotService telegramBot,
        IProductRepository productRepository, IOperationService operationService)
    {
        _productRepository = productRepository;
        _operationService = operationService;
        _botClient = telegramBot.GetBot().Result;
    }

    public override string Name => CommandNames.SelectCategoryCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var message = update.Message;
        var list = await _productRepository.GetProductNameByCategoryName(message.Text);
        if (list.Count != 0)
        {
            var keyboard = _operationService.CreateKeyboardButtonsInThirdColumns(
                list);
            var inlineKeyboard = new ReplyKeyboardMarkup(keyboard);
            await _botClient.SendTextMessageAsync(update.Message.Chat,
                "Выберите нужный продукт.",
                replyMarkup: inlineKeyboard);
        }
        else
        {
            //_executor.ExecuteCommand(CommandNames.StartCommand, update);
            var inlineKeyboard = _operationService.GetKeyboardMarkup(update);
            await _botClient.SendTextMessageAsync(update.Message.Chat,
                "Эта категория сейчас в разработке", replyMarkup: inlineKeyboard);
        }
    }
}