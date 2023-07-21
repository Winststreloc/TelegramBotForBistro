using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IOperationService
{
    Task<Operation> GetLast(long userId);
    Task<List<Operation>> GetOperations(long userId, DateTime byDate);
    ReplyKeyboardMarkup GetKeyboardMarkup(Update update);
    List<KeyboardButton[]> CreateKeyboardButtonsInThirdColumns(List<string?> list);
    List<KeyboardButton[]> CreateKeyboardButtonsList(List<string?> list);
    Task<bool> DeleteOperation(long id);
}