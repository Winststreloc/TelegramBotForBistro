using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using WriteOffUley.Interfaces;

namespace WriteOffUley.Controllers;

[ApiController]
[Route("api/message/update")]
public class TelegramBotController : ControllerBase
{
    private readonly ICommandExecutor _commandExecutor;

    public TelegramBotController(ICommandExecutor commandExecutor)
    {
        _commandExecutor = commandExecutor;
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody]object update)
    {
        var upd = JsonConvert.DeserializeObject<Update>(update.ToString() ?? string.Empty);

        Console.WriteLine("request enter");
        if (upd?.Message?.Chat == null && upd?.CallbackQuery == null)
        {
            return Ok();
        }
        try
        {
            await _commandExecutor.Execute(upd);
        }
        catch (Exception e)
        {
            return Ok();
        }

        return Ok();
    }
}