using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services;
using App.Utils;

namespace App.Controllers;

[ApiController]
[Route("/api/v1/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authenticationService;
    private readonly ILogger<UserController> _logger;

    public AuthenticationController(AuthenticationService authenticationService, ILogger<UserController> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password) {
        var token = await _authenticationService.Login(username, password);
        return Ok(ResponseUtils.SuccessResponse(new { token = token }, "Authenticated"));
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(User user) {
        var response = await _authenticationService.SignUp(user);
        return Ok(ResponseUtils.SuccessResponse(response, "Account created"));
    }
}
