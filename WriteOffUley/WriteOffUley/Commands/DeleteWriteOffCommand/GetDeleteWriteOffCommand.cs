﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Repository;
using WriteOffUley.Service;

namespace WriteOffUley.Commands.DeleteWriteOffCommand;

public class GetDeleteWriteOffCommand : BaseCommand
{
    private readonly TelegramBotClient _botClient;
    private readonly IOperationRepository _operationRepository;

    public GetDeleteWriteOffCommand(IOperationRepository operationRepository, TelegramBotService botService)
    {
        _operationRepository = operationRepository;
        _botClient = botService.GetBot().Result;
    }

    public override string Name => CommandNames.DeleteWriteOffCommand;
    public override async Task ExecuteAsync(Update update)
    {
        var writeOffs = await _operationRepository.GetOperationsAllDay();
        var inlineKeyboardButtons = writeOffs.Select(p => new InlineKeyboardButton
        {
            Text = $"{p.Name} {p.Count}",
            CallbackData = p.Id.ToString()
        }).ToArray();

        var rows = new List<IEnumerable<InlineKeyboardButton>>();
        var rowSize = 3;

        for (int i = 0; i < inlineKeyboardButtons.Length; i += rowSize)
        {
            var rowButtons = inlineKeyboardButtons.Skip(i).Take(rowSize);
            rows.Add(rowButtons);
        }

        var inlineKeyboard = new InlineKeyboardMarkup(rows);

        await _botClient.SendTextMessageAsync(update.Message.Chat.Id,
            "Выберите нужное списание",
            replyMarkup: inlineKeyboard);
    }

}