using Application.Command.Intern;
using Application.Common.Helpers;
using Application.Common.Models;
using Application.DTOs.Account;
using Domain.Interface;

namespace Application.UseCases.Admin;

public class AddInternUseCase(
    IAccountRepository accountRepository,
    IRoleRepository roleRepository,
    IInternStatusRepository internStatusRepository,
    AccountHelper accountHelper,
    UserProfileHelper userProfileHelper,
    InternHelper internHelper)
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IInternStatusRepository _internStatusRepository = internStatusRepository;
    private readonly AccountHelper _accountHelper = accountHelper;
    private readonly UserProfileHelper _userProfileHelper = userProfileHelper;
    private readonly InternHelper _internHelper = internHelper;

    /// <summary>
    /// Yeni bir stajyer ekler. Admin temel bilgileri girer, sistem otomatik şifre üretir.
    /// </summary>
    public async Task<Result<CreateAccountResponseDto>> AddInternAsync(CreateInternCommand command)
    {
        var exists = await _accountRepository.IsUserExists(command.Email);
        if (exists)
            return Result<CreateAccountResponseDto>.Failure(ResultCode.EmailExists, "Bu email kayıtlı");

        var role = await _roleRepository.GetRoleByNameAsync("Stajyer");
        if (role is null)
            return Result<CreateAccountResponseDto>.Failure(ResultCode.RoleNotFound, "Stajyer rolü bulunamadı");

        var status = await _internStatusRepository.GetByNameAsync("Aktif");
        if (status is null)
            return Result<CreateAccountResponseDto>.Failure(ResultCode.NotFound, "Aktif stajyer durumu bulunamadı");

        var password = PasswordHelper.Generate();
        var passwordHash = PasswordHelper.Hash(password);

        var accountId = Guid.NewGuid();
        var profileId = Guid.NewGuid();
        var internId = Guid.NewGuid();

        var account = _accountHelper.CreateAccount(accountId, command.Email, passwordHash, role.Id);
        var profile = _userProfileHelper.CreateUserProfile(profileId, accountId, command.Name, command.Surname, command.Email, command.PhoneNumber);
        var intern = _internHelper.CreateIntern(
            internId,
            accountId,
            profileId,
            command.MentorId,
            command.University,
            command.Department,
            command.Class,
            command.StartDate,
            command.EndDate,
            status.Id);

        var isCreated = await _accountRepository.CreateIntern(account, profile, intern);
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

        return Result<CreateAccountResponseDto>.Success(responseDto, "Stajyer hesabı başarıyla oluşturuldu");
    }
}