﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands.AnaliticsWriteOffCommand;

public class AnalyticsCommand : BaseCommand
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly IUserService _userService;

    public AnalyticsCommand(TelegramBotService telegramBot, IUserService userService)
    {
        _telegramBotClient = telegramBot.GetBot().Result;
        _userService = userService;
    }

    public override string Name => CommandNames.AnalyticsCommand;
    public override async Task ExecuteAsync(Update update)
    {
        var user = await _userService.GetOrCreate(update);

        var inlineKeyboard = new InlineKeyboardMarkup(new []
        {
            new [] 
            {
                new InlineKeyboardButton{Text = "Списания за 1", CallbackData = "analytic-1"},          
                new InlineKeyboardButton{Text = "Списания за 3", CallbackData = "analytic-3"},                                                                
                new InlineKeyboardButton{Text = "Списания за 7", CallbackData = "analytic-7"},
            },
            new [] 
            {
                new InlineKeyboardButton{Text = "Списания за 14", CallbackData = "analytic-14"},          
                new InlineKeyboardButton{Text = "Списания за 30", CallbackData = "analytic-30"},                                                                
                new InlineKeyboardButton{Text = "Списания за 365", CallbackData = "analytic-365"},
            }
        });

        await _telegramBotClient.SendTextMessageAsync(user.ChatId, "Выберите количество дней за которые нужны списания", 
            ParseMode.Markdown, replyMarkup:inlineKeyboard);
    }
}