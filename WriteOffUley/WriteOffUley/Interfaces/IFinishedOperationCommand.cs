using Telegram.Bot.Types;
using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IFinishedOperationCommand
{
    Task Execute(Update update, WriteOffProduct product);
}