using JiraMini.Application.Features.Auth.Register;
using JiraMini.Application.Features.Auth.Login;
using Microsoft.AspNetCore.Authorization;
using JiraMini.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JiraMini.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterHandler _registerHandler;
    private readonly LoginHandler _loginHandler;

    public AuthController(RegisterHandler registerHandler, LoginHandler loginHandler)
    {
        _registerHandler = registerHandler;
        _loginHandler = loginHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result =
            await _registerHandler.HandleAsync(request);

        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _loginHandler.Handle(request);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("test")]
    public IActionResult Get()
    {
        return Ok("Token hợp lệ");
    }
    [Authorize]
    [HttpGet("role")]
    public IActionResult Role(
        [FromServices] ICurrentUserService currentUser)
    {
        return Ok(new
        {
            currentUser.UserId,
            currentUser.Email,
            currentUser.Role,
            currentUser.IsAuthenticated
        });
    }
}