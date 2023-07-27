using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands.DeleteWriteOffCommand;

public class SelectDeleteWriteOffCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly IOperationRepository _operationRepository;
    private readonly IWriteOffService _writeOffService;

    public SelectDeleteWriteOffCommand(TelegramBotService botClient, IUserService userService, IOperationRepository operationRepository, IWriteOffService writeOffService)
    {
        _userService = userService;
        _operationRepository = operationRepository;
        _writeOffService = writeOffService;
        _botClient = botClient.GetBot().Result;
    }

    public override string Name => CommandNames.SelectDeleteWriteOffCommand;
    public override async Task ExecuteAsync(Update update)
    {
        var user = await _userService.GetOrCreate(update);
        var deleteString = update.CallbackQuery?.Data;
        if (deleteString != null)
        {
            var operationId = long.Parse(deleteString);
            await _writeOffService.DeleteWriteOff(operationId);
            if (await _operationRepository.DeleteOperation(operationId))
            {
                await _botClient.SendTextMessageAsync(user.ChatId, "Операция удалена.", ParseMode.Markdown);
            }
            else
            {
                await _botClient.SendTextMessageAsync(user.ChatId, "Что-то пошло не так((", ParseMode.Markdown);
            }
        }
        else
        {
            await _botClient.SendTextMessageAsync(user.ChatId, "Что-то пошло не так((", ParseMode.Markdown);
        }
        
    }
}