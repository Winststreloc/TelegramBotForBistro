using Telegram.Bot.Types;
using WriteOffUley.Entity;

namespace WriteOffUley.Interfaces;

public interface IUserService
{
    Task<AppUser> GetOrCreate(Update update);
}