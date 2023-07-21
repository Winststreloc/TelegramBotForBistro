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

    public FinishOperationCommand(TelegramBotService botService, IKeyboardService keyboardService, IOperationRepository operationRepository)
    {
        _keyboardService = keyboardService;
        _operationRepository = operationRepository;
        _botClient = botService.GetBot().Result;
    }
    
    public async Task Execute(Update update, WriteOffProduct product)
    {
        var inlineKeyboard = _keyboardService.GetKeyboardMarkup(update);
        if (await _operationRepository.AddOperation(product, update.Message.Chat.Id))
        {
            await _botClient.SendTextMessageAsync(update.Message.Chat, "Списание добавлено", replyMarkup: inlineKeyboard);
        }
        else
        {
            await _botClient.SendTextMessageAsync(update.Message.Chat, "Что-то пошло не так((", replyMarkup: inlineKeyboard);
        }
    }
}