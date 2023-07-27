using Telegram.Bot.Types;

namespace WriteOffUley.Interfaces;

public interface ICommandExecutor
{
    Task Execute(Update update);
}