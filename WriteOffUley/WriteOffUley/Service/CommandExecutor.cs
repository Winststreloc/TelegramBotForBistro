﻿using Telegram.Bot.Types;
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
                    await ExecuteCommand(CommandNames.SelectCategoryCommand, update);
                    return;
                case "Удалить списание":
                    await ExecuteCommand(CommandNames.DeleteWriteOffCommand, update);
                    return;
                case "Посмотреть списания за день":
                    await ExecuteCommand(CommandNames.OpenAllWriteOffDayCommand, update);
                    return;
                case "Посмотреть граммовки списаний":
                    await ExecuteCommand(CommandNames.AnalyticsCommand, update);
                    return;
                case "Посмотреть списания":
                    await ExecuteCommand(CommandNames.SelectWriteOffsCommand, update);
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
            case CommandNames.SelectCategoryCommand:
            {
                if (update.Message.Text == CategoryProductNames.AddProduct)
                {
                    await ExecuteCommand(CommandNames.CreateNewProduct, update);
                    break;
                }
                await ExecuteCommand(CommandNames.SelectProductCommand, update);
                break;
            }
            case CommandNames.SelectProductCommand:
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
            case CommandNames.DeleteWriteOffCommand:
            {
                await ExecuteCommand(CommandNames.SelectDeleteWriteOffCommand, update);
                break;
            }
            case CommandNames.AnalyticsCommand:
            {
                await ExecuteCommand(CommandNames.SelectAnalyticsCommand, update);
                break;
            }
            case CommandNames.SelectWriteOffsCommand:
            {
                await ExecuteCommand(CommandNames.GetWriteOffsCommand, update);
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