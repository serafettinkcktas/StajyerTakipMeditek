using Domain.Entity;

namespace Application.Interface;

public interface ITokenService
{
    string GenerateAccessToken(Account account, string roleName);
    string GenerateRefreshToken();
}