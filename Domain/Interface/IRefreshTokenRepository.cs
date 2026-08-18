using Domain.Entity;

namespace Domain.Interface;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task CreateAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task RevokeAsync(Guid id);
}