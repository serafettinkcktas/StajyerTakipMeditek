using Application.DTOs.Mentor;
using Application.Interface;
using Dapper;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Repository;

public class MentorRepository : BaseRepository<Mentor>, IMentorRepository
{
    public MentorRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task<IEnumerable<MentorDto>> GetAllAsync()
    {
        const string sql = @"
            SELECT m.Id, m.AccountId, p.Name, p.Surname, p.Email, p.PhoneNumber, m.InternCount
            FROM Mentors m
            INNER JOIN UserProfiles p ON p.Id = m.ProfileId
            WHERE m.IsDeleted = 0";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<MentorDto>(sql);
    }
}