using Dapper;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Repository;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task CreateAsync(RefreshToken refreshToken)
    {
        const string sql = @"
            INSERT INTO RefreshTokens (Id, AccountId, Token, ExpiresAt, RevokedAt, CreatedAt)
            VALUES (@Id, @AccountId, @Token, @ExpiresAt, @RevokedAt, @CreatedAt)";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, refreshToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        const string sql = @"
            SELECT Id, AccountId, Token, ExpiresAt, RevokedAt, CreatedAt
            FROM RefreshTokens
            WHERE Token = @Token";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<RefreshToken>(sql, new { Token = token });
    }

    public async Task RevokeAsync(Guid id)
    {
        const string sql = @"
            UPDATE RefreshTokens
            SET RevokedAt = @Now
            WHERE Id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id, Now = DateTime.UtcNow });
    }
}