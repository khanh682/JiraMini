using JiraMini.Domain.Entities;

namespace JiraMini.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}