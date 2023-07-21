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
    private readonly IOperationService _operationService;

    public SelectDeleteWriteOffCommand(TelegramBotService botClient, IUserService userService, IOperationService operationService)
    {
        _userService = userService;
        _operationService = operationService;
        _botClient = botClient.GetBot().Result;
    }

    public override string Name => CommandNames.SelectDeleteWriteOffCommand;
    public override async Task ExecuteAsync(Update update)
    {
        var user = await _userService.GetOrCreate(update);
        var deleteString = update.CallbackQuery?.Data;
        if (deleteString != null)
        {
            var delete = long.Parse(deleteString);
            if (await _operationService.DeleteOperation(delete))
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