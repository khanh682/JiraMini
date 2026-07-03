namespace JiraMini.Application.Features.Auth.Login;

public record LoginResponse(
    string AccessToken,
    DateTime ExpiresAt
);