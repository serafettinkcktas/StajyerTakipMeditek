using Domain.Entity;

namespace Domain.Interface;

public interface IAccountRepository : IRepository<Account>
{
    Task<bool> IsUserExists(string email);
    Task<Account?> GetByEmailAsync(string email);
    Task<Account?> GetByIdAsync(Guid id);
    Task<bool> CreateAdmin(Account account, UserProfile profile);
    Task<bool> CreateMentor(Account account, UserProfile profile, Mentor mentor);
    Task<bool> CreateIntern(Account account, UserProfile profile, Intern intern);
}
