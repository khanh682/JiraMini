using JiraMini.Domain.Enums;

namespace JiraMini.Application.Features.Auth.Register;

public class RegisterResponse
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public UserRole Role { get; set; }
}