using Domain.Entity;

namespace Domain.Interface;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetRoleByNameAsync(string name);
    Task<bool> CreateRoleAsync(Role role);
    Task<IEnumerable<Role>> GetAllRolesAsync();
}