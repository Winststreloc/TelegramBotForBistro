﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands;

public class ChoiceProductCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOperationService _operationService;

    public ChoiceProductCommand(TelegramBotService telegramBot, ICategoryRepository categoryRepository, IOperationService operationService)
    {
        _categoryRepository = categoryRepository;
        _operationService = operationService;
        _botClient = telegramBot.GetBot().Result;
    }

    public override string Name => CommandNames.ChoiceProductCommand;

    public override async Task ExecuteAsync(Update update)
    {
        var categories = await _categoryRepository.GetAllCategoryNames();
        var keyboard = _operationService.CreateKeyboardButtonsInThirdColumns(categories);
        const string message = "Для добавления нового списания выберите категорию товара \n";
        var inlineKeyboard = new ReplyKeyboardMarkup(keyboard);
        inlineKeyboard.ResizeKeyboard = true;

        await _botClient.SendTextMessageAsync(update.Message.Chat.Id, message, ParseMode.Markdown,
            replyMarkup: inlineKeyboard);
    }
}