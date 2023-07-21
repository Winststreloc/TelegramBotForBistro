using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Service;

namespace WriteOffUley.Commands;

public class SelectCountProductsCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;

    public SelectCountProductsCommand(TelegramBotService botService)
    {
        _botClient = botService.GetBot().Result;
    }

    public override string Name => CommandNames.SelectCountProductsCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var inlineKeyboard = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
                new KeyboardButton("1"),
                new KeyboardButton("2"),
                new KeyboardButton("3"),
            },
            new[]
            {
                new KeyboardButton("4"),
                new KeyboardButton("5"),
                new KeyboardButton("6"),
            }
        }, resizeKeyboard: true);


        await _botClient.SendTextMessageAsync(update.Message.Chat,
            "Выберите число, либо введите его.",
            replyMarkup: inlineKeyboard);
    }
}