using Dapper;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Repository;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task<bool> IsUserExists(string email)
    {
        const string sql = @"
            SELECT CASE WHEN EXISTS (
                SELECT 1
                FROM Accounts
                WHERE Email = @Email
            ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(sql, new { Email = email });
    }

    public async Task<bool> CreateMentor(Account account, UserProfile profile, Mentor mentor)
    {
        const string insertAccountSql = @"
            INSERT INTO Accounts (Id, Email, Password, RoleId)
            VALUES (@Id, @Email, @Password, @RoleId);";

        const string insertProfileSql = @"
            INSERT INTO UserProfiles (Id, AccountId, Name, Surname, Email)
            VALUES (@Id, @AccountId, @Name, @Surname, @Email);";

        const string insertMentorSql = @"
            INSERT INTO Mentors (Id, AccountId, ProfileId)
            VALUES (@Id, @AccountId, @ProfileId);";

        using var connection = _connectionFactory.CreateConnection();

        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }

        using var transaction = connection.BeginTransaction();

        try
        {
            await connection.ExecuteAsync(insertAccountSql, account, transaction);
            await connection.ExecuteAsync(insertProfileSql, profile, transaction);
            await connection.ExecuteAsync(insertMentorSql, mentor, transaction);

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}