using Domain.Entity;

namespace Domain.Interface;

public interface IAccountRepository : IRepository<Account>
{
    Task<bool> IsUserExists(string email);
    Task<bool> CreateMentor(Account account, UserProfile profile, Mentor mentor);
}