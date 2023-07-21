using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Service;

public class OperationService : IOperationService
{
    private readonly DataContext _context;
    private readonly IUserRepository _userRepository;

    public OperationService(DataContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<Operation> GetLast(long userId)
    {
        return await _context.Operations.OrderBy(x => x.CreatedAt)
            .LastOrDefaultAsync(x => x.UserId == userId && !x.IsFinished);
    }

    public async Task<List<Operation>> GetOperations(long userId, DateTime byDate)
    {
        return await _context.Operations.OrderBy(x => x.CreatedAt).Where(x => x.CreatedAt >= byDate).ToListAsync();
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
                    new KeyboardButton("Посмотреть аналитику списаний")
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

    public List<KeyboardButton[]> CreateKeyboardButtonsInThirdColumns(List<string?> list)
    {
        List<List<string>> productSublists = new List<List<string>>();
        for (int i = 0; i < list.Count; i += 3)
        {
            productSublists.Add(list.Skip(i).Take(3).ToList());
        }

        return productSublists.Select(sublist => sublist.Select(p => new KeyboardButton(p)).ToArray()).ToList();
    }

    public List<KeyboardButton[]> CreateKeyboardButtonsList(List<string?> list)
    {
        return list.Select(p => new KeyboardButton[] { new KeyboardButton(p) }).ToList();
    }

    public async Task<bool> DeleteOperation(long id)
    {
        var operation = await _context.Operations.SingleAsync(o => o.Id == id);
        if (operation == null) return false;
        _context.Operations.Remove(operation);
        await _context.SaveChangesAsync();
        return true;

    }
}