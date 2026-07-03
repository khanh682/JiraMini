using JiraMini.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JiraMini.Application.Features.Auth.Login;

public class LoginHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponse> Handle(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user is null)
        {
            throw new Exception("Email hoặc mật khẩu không đúng.");
        }

        var isValidPassword = _passwordHasher.VerifyPassword(
            request.Password,
            user.PasswordHash);

        if (!isValidPassword)
        {
            throw new Exception("Email hoặc mật khẩu không đúng.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new LoginResponse(
            token,
            DateTime.UtcNow.AddHours(1));
    }
}