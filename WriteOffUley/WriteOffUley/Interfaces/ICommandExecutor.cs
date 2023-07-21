using Telegram.Bot.Types;

namespace WriteOffUley.Interfaces;

public interface ICommandExecutor
{
    Task Execute(Update update);
    //Task ExecuteCommand(string commandName, Update update);
}