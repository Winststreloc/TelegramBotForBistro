using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IKeyboardService
{
    ReplyKeyboardMarkup GetKeyboardMarkup(Update update);
    IEnumerable<KeyboardButton[]> CreateKeyboardButtonsInThirdColumns(List<string?> list);
    List<KeyboardButton[]> CreateKeyboardButtonsList(List<string?> list);

}