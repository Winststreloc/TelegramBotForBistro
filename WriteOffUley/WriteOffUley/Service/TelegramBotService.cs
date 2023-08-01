using Telegram.Bot;

namespace WriteOffUley.Service;

public class TelegramBotService
{
    private readonly IConfiguration _configuration;
    private TelegramBotClient _botClient;

    public TelegramBotService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TelegramBotClient> GetBot()
    {
        if (_botClient != null)
        {
            return _botClient;
        }
            
        _botClient = new TelegramBotClient(_configuration["Token"]);

        var hook = $"{_configuration["UrlNGROK"]}api/message/update";
        await _botClient.SetWebhookAsync(hook);

        return _botClient;
    }
}