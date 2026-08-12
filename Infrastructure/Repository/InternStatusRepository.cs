using Dapper;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Repository;

public class InternStatusRepository : BaseRepository<InternStatus>, IInternStatusRepository
{
    public InternStatusRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task<InternStatus?> GetByNameAsync(string name)
    {
        const string sql = @"
            SELECT Id, Name
            FROM InternStatuses
            WHERE Name = @Name";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<InternStatus>(sql, new { Name = name });
    }

    public async Task<bool> CreateStatusAsync(InternStatus status)
    {
        const string sql = @"
            INSERT INTO InternStatuses (Id, Name)
            VALUES (@Id, @Name)";

        using var connection = _connectionFactory.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sql, status);
        return affectedRows > 0;
    }
}
