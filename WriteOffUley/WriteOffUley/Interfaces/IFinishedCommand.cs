using Telegram.Bot.Types;
using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IFinishedCommand
{
    Task Execute(Update update, WriteOffProduct product);
}