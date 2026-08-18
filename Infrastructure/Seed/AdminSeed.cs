using Application.Common.Helpers;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Seed;

public class AdminSeed(IAccountRepository accountRepository, IRoleRepository roleRepository)
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    private const string AdminEmail = "admin@stajyertakip.com";
    private const string AdminPassword = "Admin123!";
    private const string AdminName = "Admin";
    private const string AdminSurname = "Admin";

    /// <summary>
    /// Uygulama ilk açıldığında varsayılan admin hesabını kontrol eder, yoksa oluşturur.
    /// </summary>
    public async Task SeedAsync()
    {
        var exists = await _accountRepository.IsUserExists(AdminEmail);
        if (exists)
            return;

        var adminRole = await _roleRepository.GetRoleByNameAsync("Admin");
        if (adminRole is null)
            return; // Roller seed edilmediyse admin oluşturulamaz

        var accountId = Guid.NewGuid();
        var profileId = Guid.NewGuid();

        var passwordHash = PasswordHelper.Hash(AdminPassword);

        var account = new Account(accountId, AdminEmail, passwordHash, adminRole.Id);
        var profile = new UserProfile(profileId, accountId, AdminName, AdminSurname, AdminEmail);

        await _accountRepository.CreateAdmin(account, profile);
    }
}