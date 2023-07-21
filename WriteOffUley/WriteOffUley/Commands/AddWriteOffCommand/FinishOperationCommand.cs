using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WriteOffUley.Entity;
using WriteOffUley.Interfaces;
using WriteOffUley.Service;

namespace WriteOffUley.Commands;

public class FinishOperationCommand : IFinishedCommand
{
    private readonly TelegramBotClient _botClient;
    private DataContext _context;
    private readonly IOperationService _operationService;
    private readonly IProductRepository _productRepository;

    public FinishOperationCommand(TelegramBotService botService, DataContext context, IOperationService operationService, IProductRepository productRepository)
    {
        _context = context;
        _operationService = operationService;
        _productRepository = productRepository;
        _botClient = botService.GetBot().Result;
    }
    
    public async Task Execute(Update update, WriteOffProduct product)
    {
        var inlineKeyboard = _operationService.GetKeyboardMarkup(update);
        if (await AddWriteOffProduct(product) > 0)
        {
            await _botClient.SendTextMessageAsync(update.Message.Chat, "Списание добавлено", replyMarkup: inlineKeyboard);
        }
        else
        {
            await _botClient.SendTextMessageAsync(update.Message.Chat, "Что-то пошло не так((", replyMarkup: inlineKeyboard);
        }
    }

    private async Task<int> AddWriteOffProduct(WriteOffProduct writeOffProduct)
    {
        if (!await _productRepository.ExistProduct(writeOffProduct.Name))
        {
            return 0;
        }
        var product = await _productRepository.GetProductByName(writeOffProduct.Name);
        var operation = new Operation()
        {
            Name = writeOffProduct.Name,
            Count = writeOffProduct.Count,
            Price = product.Price * writeOffProduct.Count
        };
        await _context.Operations.AddAsync(operation);
        return await _context.SaveChangesAsync();
    }
}