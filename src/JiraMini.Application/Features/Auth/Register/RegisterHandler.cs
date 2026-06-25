using JiraMini.Domain.Entities;
using JiraMini.Domain.Enums;
using JiraMini.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JiraMini.Application.Features.Auth.Register;

public class RegisterHandler
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterHandler(
    IApplicationDbContext dbContext,
    IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResponse> HandleAsync(RegisterRequest request)
    {
        // Validate
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            throw new Exception("Tên không được để trống");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new Exception("Email không được để trống");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new Exception("Password không được để trống");
        }

        // Check email duplicate
        var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (existingUser is not null)
        {
            throw new Exception("Email đã tồn tại ! Vui lòng sử dụng email khác");
        }

        // Hash password
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        // Create user
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        return new RegisterResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }
}