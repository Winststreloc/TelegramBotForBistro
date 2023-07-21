using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands.OpenWriteOffDay;

public class OpenWriteOffDayCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly IOperationRepository _operationRepository;

    public OpenWriteOffDayCommand(TelegramBotService telegramBot, IOperationRepository operationRepository)
    {
        _botClient = telegramBot.GetBot().Result;
        _operationRepository = operationRepository;
    }

    public override string Name => CommandNames.OpenAllWriteOffDayCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var message = "";
        decimal totalSum = 0;
        var allRecordsDay = await _operationRepository.GetOperationsAllDay();
        foreach (var record in allRecordsDay)
        {
            message += record.Name + " |" + record.Count + " |" + record.CreatedAt.ToString("HH:mm") + "\n";
            totalSum += record.Price;
        }

        message += $"\nОбщая сумма: {totalSum}";
        await _botClient.SendTextMessageAsync(update.Message.Chat,
            message);
    }
}