using Domain.Entity;

namespace Domain.Interface;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetRoleByNameAsync(string name);
    Task<Role?> GetRoleByIdAsync(Guid id);
    Task<bool> CreateRoleAsync(Role role);
    Task<IEnumerable<Role>> GetAllRolesAsync();
}