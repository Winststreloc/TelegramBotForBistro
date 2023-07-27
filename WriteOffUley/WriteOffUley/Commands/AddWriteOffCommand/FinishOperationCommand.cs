using Telegram.Bot;
using Telegram.Bot.Types;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands;

public class FinishOperationCommand : IFinishedOperationCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly IKeyboardService _keyboardService;
    private readonly IOperationRepository _operationRepository;
    private readonly IWriteOffService _writeOffService;

    public FinishOperationCommand(TelegramBotService botService, IKeyboardService keyboardService, IOperationRepository operationRepository, IWriteOffService writeOffService)
    {
        _keyboardService = keyboardService;
        _operationRepository = operationRepository;
        _writeOffService = writeOffService;
        _botClient = botService.GetBot().Result;
    }
    
    public string Name => CommandNames.FinishOperationCommand;
    
    public async Task Execute(Update update, WriteOffProduct product)
    {
        var inlineKeyboard = _keyboardService.GetKeyboardMarkup(update);
        if (await _operationRepository.AddOperation(product, update.Message.Chat.Id))
        {
            await _writeOffService.CreateWriteOff(product);
            await _botClient.SendTextMessageAsync(update.Message.Chat, "Списание добавлено", replyMarkup: inlineKeyboard);
        }
        else
        {
            await _botClient.SendTextMessageAsync(update.Message.Chat, "Что-то пошло не так((", replyMarkup: inlineKeyboard);
        }
    }
}