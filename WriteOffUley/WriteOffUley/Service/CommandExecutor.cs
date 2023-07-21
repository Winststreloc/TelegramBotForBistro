using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WriteOffUley.Commands;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Service;

public class CommandExecutor : ICommandExecutor
{
    private readonly List<BaseCommand> _commands;
    private BaseCommand _lastCommand;
    private string _productName;
    private readonly IFinishedOperationCommand _finishedOperationCommand;

    public CommandExecutor(IServiceProvider serviceProvider, IFinishedOperationCommand finishedOperationCommand)
    {
        _finishedOperationCommand = finishedOperationCommand;
        _commands = serviceProvider.GetServices<BaseCommand>().ToList();
    }

    public async Task Execute(Update update)
    {
        if (update?.Message?.Chat == null && update?.CallbackQuery == null)
            return;
        
        if (update.Type == UpdateType.Message)
        {
            switch (update.Message?.Text)
            {
                case "Добавить списание":
                    await ExecuteCommand(CommandNames.ChoiceProductCommand, update);
                    return;
                case "Удалить списание":
                    await ExecuteCommand(CommandNames.GetDeleteWriteOffCommand, update);
                    return;
                case "Посмотреть списания за день":
                    await ExecuteCommand(CommandNames.OpenAllWriteOffDayCommand, update);
                    return;
                case "Посмотреть аналитику списаний":
                    await ExecuteCommand(CommandNames.SelectAnalyticsCommand, update);
                    return;
                case "Склад":
                    await ExecuteCommand(CommandNames.SelectAnalyticsCommand, update);
                    return;
            }
            
        }

        if (update.Message != null && update.Message.Text.Contains(CommandNames.StartCommand))
        {
            await ExecuteCommand(CommandNames.StartCommand, update);
            return;
        }
        
        switch (_lastCommand?.Name)
        {
            case CommandNames.ChoiceProductCommand:
            {
                await ExecuteCommand(CommandNames.SelectCategoryCommand, update);
                break;
            }
            case CommandNames.SelectCategoryCommand:
            {
                _productName = update.Message.Text;
                await ExecuteCommand(CommandNames.SelectCountProductsCommand, update);
                break;
            }
            case CommandNames.SelectCountProductsCommand:
            {
                var product = new WriteOffProduct()
                {
                    Name = _productName,
                    Count = int.Parse(update.Message.Text)
                };
                await _finishedOperationCommand.Execute(update, product);
                break;
            }
            case CommandNames.GetDeleteWriteOffCommand:
            {
                await ExecuteCommand(CommandNames.SelectDeleteWriteOffCommand, update);
                break;
            }
            case null:
            {
                await ExecuteCommand(CommandNames.StartCommand, update);
                break;
            }
        }
    }

    private async Task ExecuteCommand(string commandName, Update update)
    {
        _lastCommand = _commands.First(x => x.Name == commandName);
        await _lastCommand.ExecuteAsync(update);
    }
}