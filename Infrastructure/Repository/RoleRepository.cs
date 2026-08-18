using Dapper;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task<Role?> GetRoleByNameAsync(string name)
    {
        const string sql = @"
            SELECT Id, Name
            FROM Roles
            WHERE Name = @Name";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Role>(sql, new { Name = name });
    }

    public async Task<Role?> GetRoleByIdAsync(Guid id)
    {
        const string sql = @"
            SELECT Id, Name
            FROM Roles
            WHERE Id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Role>(sql, new { Id = id });
    }

    public async Task<bool> CreateRoleAsync(Role role)
    {
        const string sql = @"
            INSERT INTO Roles (Id, Name)
            VALUES (@Id, @Name)";

        using var connection = _connectionFactory.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sql, role);
        return affectedRows > 0;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        const string sql = @"
            SELECT Id, Name
            FROM Roles";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Role>(sql);
    }
}