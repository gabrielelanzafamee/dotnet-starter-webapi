using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers;

[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(UserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<User>>> List() {
        return Ok(await _userService.GetUsersAsync());
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [Authorize]
    [HttpPost("")]
    public async Task<ActionResult<User>> Create(User user) {
        var result = await _userService.CreateUserAsync(user);
        return Ok(result);
    }
}
