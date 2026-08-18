using Application.DTOs.Intern;
using Application.Interface;
using Dapper;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Repository;

public class InternRepository : BaseRepository<Intern>, IInternRepository
{
    public InternRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task<IEnumerable<InternDto>> GetAllAsync()
    {
        const string sql = @"
            SELECT
                i.Id,
                p.Name,
                p.Surname,
                p.Email,
                p.PhoneNumber,
                p.PhotoUrl,
                i.University,
                i.Department,
                i.Class,
                i.MentorId,
                mp.Name AS MentorName,
                st.Name AS StatusName,
                i.StartDate,
                i.EndDate
            FROM Interns i
            INNER JOIN UserProfiles p ON p.Id = i.ProfileId
            LEFT JOIN Mentors m ON m.Id = i.MentorId
            LEFT JOIN UserProfiles mp ON mp.Id = m.ProfileId
            INNER JOIN InternStatuses st ON st.Id = i.StatusId
            WHERE i.IsDeleted = 0";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<InternDto>(sql);
    }
}