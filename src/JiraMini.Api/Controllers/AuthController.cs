using JiraMini.Application.Features.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace JiraMini.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterHandler _registerHandler;

    public AuthController(RegisterHandler registerHandler)
    {
        _registerHandler = registerHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result =
            await _registerHandler.HandleAsync(request);

        return Ok(result);
    }
}