using Application.Command.Mentor;
using Application.Common.Helpers;
using Application.Common.Models;
using Application.DTOs.Account;
using Domain.Interface;

namespace Application.UseCases.Admin;

public class AddMentorUseCase(IAccountRepository accountRepository, IRoleRepository roleRepository, AccountHelper accountHelper, UserProfileHelper userProfileHelper, MentorHelper mentorHelper)
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly AccountHelper _accountHelper = accountHelper;
    private readonly UserProfileHelper _userProfileHelper = userProfileHelper;
    private readonly MentorHelper _mentorHelper = mentorHelper;

    /// <summary>
    /// Yeni bir mentor ekler. Admin sadece ad, soyad ve email girer.
    /// Sistem rastgele bir şifre oluşturur ve geriye döner.
    /// </summary>
    public async Task<Result<CreateAccountResponseDto>> AddMentorAsync(CreateMentorCommand command)
    {
        var exists = await _accountRepository.IsUserExists(command.Email);
        if (exists)
            return Result<CreateAccountResponseDto>.Failure(ResultCode.EmailExists, "Bu email kayıtlı");

        var role = await _roleRepository.GetRoleByNameAsync("Mentor");
        if (role is null)
            return Result<CreateAccountResponseDto>.Failure(ResultCode.RoleNotFound, "Mentor rolü bulunamadı");

        var password = PasswordHelper.Generate();
        var passwordHash = PasswordHelper.Hash(password);

        var accountId = Guid.NewGuid();
        var profileId = Guid.NewGuid();
        var mentorId = Guid.NewGuid();

        var account = _accountHelper.CreateAccount(accountId, command.Email, passwordHash, role.Id);
        var profile = _userProfileHelper.CreateUserProfile(profileId, accountId, command.Name, command.Surname, command.Email);
        var mentor = _mentorHelper.CreateMentor(mentorId, accountId, profileId);

        var isCreated = await _accountRepository.CreateMentor(account, profile, mentor);
        if (!isCreated)
            return Result<CreateAccountResponseDto>.Failure(ResultCode.UnexpectedError, "Hesap oluşturulamadı");

        var responseDto = new CreateAccountResponseDto
        {
            Id = accountId,
            Name = command.Name,
            Surname = command.Surname,
            Email = command.Email,
            GeneratedPassword = password
        };

        return Result<CreateAccountResponseDto>.Success(responseDto, "Hesap başarıyla oluşturuldu");
    }
}