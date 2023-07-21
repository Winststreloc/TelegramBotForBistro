using WriteOffUley.Interfaces;

namespace WriteOffUley.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public bool ExamAdminUser(long id)
    {
        return _context.Users.Where(u => u.ChatId == id).Any(u => u.Admin);
    }
}