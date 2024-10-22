using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAIWrapper.Data;
using OpenAIWrapper.Models;

namespace OpenAIWrapper.Controllers;

[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserController> _logger;

    public UserController(AppDbContext context, ILogger<UserController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
        return Ok(await _context.Users.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("")]
    public async Task<ActionResult<User>> CreateUser(User user) {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
}
