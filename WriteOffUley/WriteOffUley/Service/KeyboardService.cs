using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Service;

public class KeyboardService : IKeyboardService
{
    private readonly IUserRepository _userRepository;

    public KeyboardService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ReplyKeyboardMarkup GetKeyboardMarkup(Update update)
    {
        ReplyKeyboardMarkup inlineKeyboard;
        if (_userRepository.ExamAdminUser(update.Message.Chat.Id))
        {
            inlineKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Добавить списание"),
                    new KeyboardButton("Удалить списание"),
                    new KeyboardButton("Посмотреть списания за день")
                },
                new[]
                {
                    new KeyboardButton("Посмотреть аналитику списаний"),
                    new KeyboardButton("Склад")
                }
            }, resizeKeyboard: true);
        }
        else
        {
            inlineKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Добавить списание"),
                new KeyboardButton("Удалить списание"),
                new KeyboardButton("Посмотреть списания за день")
            }, resizeKeyboard: true);
        }

        return inlineKeyboard;
    }

    public IEnumerable<KeyboardButton[]> CreateKeyboardButtonsInThirdColumns(List<string?> list)
    {
        List<List<string>> productSubList = new List<List<string>>();
        for (int i = 0; i < list.Count; i += 3)
        {
            productSubList.Add(list.Skip(i).Take(3).ToList()!);
        }

        return productSubList.Select(sublist => sublist.Select(p => new KeyboardButton(p)).ToArray()).ToList();
    }

    public List<KeyboardButton[]> CreateKeyboardButtonsList(List<string?> list)
    {
        return list.Select(p => new KeyboardButton[] { new KeyboardButton(p) }).ToList();
    }
}