using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Seed;

public class RoleSeed(IRoleRepository roleRepository)
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    private static readonly string[] DefaultRoles = { "Admin", "Mentor", "Stajyer" };

    /// <summary>
    /// Uygulama ayağa kalkarken temel rolleri kontrol eder, yoksa ekler.
    /// </summary>
    public async Task SeedAsync()
    {
        foreach (var roleName in DefaultRoles)
        {
            var exists = await _roleRepository.GetRoleByNameAsync(roleName);
            if (exists is null)
            {
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = roleName
                };
                await _roleRepository.CreateRoleAsync(role);
            }
        }
    }
}