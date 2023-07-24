using Microsoft.AspNetCore.Mvc;

namespace WriteOffUley.Controllers;

[ApiController]
[Route("admin")]
public class AdminController : ControllerBase
{
    private readonly DataContext _context;

    public AdminController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("open-all-write-off")]
    public async Task<IActionResult?> OpenAllWriteOff()
    {
        return default;
    }
}