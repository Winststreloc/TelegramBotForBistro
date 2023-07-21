using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands.OpenWriteOffDay;

public class OpenWriteOffDayCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly IWriteOffRepository _writeOffRepository;

    public OpenWriteOffDayCommand(TelegramBotService telegramBot, IWriteOffRepository writeOffRepository)
    {
        _botClient = telegramBot.GetBot().Result;
        _writeOffRepository = writeOffRepository;
    }

    public override string Name => CommandNames.OpenAllWriteOffDayCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var message = "";
        decimal totalSum = 0;
        var allRecordsDay = await _writeOffRepository.GetAllWriteOffDay();
        foreach (var record in allRecordsDay)
        {
            message += record.Name + " " + record.Count + " " + record.CreatedAt.ToString("HH:mm:ss") + "\n";
            totalSum += record.Price;
        }

        message += $"\n Общая сумма: {totalSum}";
        await _botClient.SendTextMessageAsync(update.Message.Chat,
            message);
    }
}