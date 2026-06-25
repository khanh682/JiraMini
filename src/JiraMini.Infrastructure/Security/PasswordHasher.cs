using BCrypt.Net;
using JiraMini.Application.Common.Interfaces;

namespace JiraMini.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(
        string password,
        string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(
            password,
            passwordHash);
    }
}