using Application.Common.Helpers;
using Application.Common.Models;
using Application.DTOs.Auth;
using Application.Interface;
using Domain.Entity;
using Domain.Interface;

namespace Application.UseCases.Auth;

public class LoginUseCase(
    IAccountRepository accountRepository,
    IRoleRepository roleRepository,
    IRefreshTokenRepository refreshTokenRepository,
    ITokenService tokenService,
    JwtOptions jwtOptions)
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly JwtOptions _jwtOptions = jwtOptions;

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var account = await _accountRepository.GetByEmailAsync(request.Email);
        if (account is null)
            return Result<LoginResponseDto>.Failure(ResultCode.NotFound, "Kullanıcı bulunamadı");

        if (!PasswordHelper.Verify(request.Password, account.Password))
            return Result<LoginResponseDto>.Failure(ResultCode.Unauthorized, "Şifre hatalı");

        var role = await _roleRepository.GetRoleByIdAsync(account.RoleId);
        if (role is null)
            return Result<LoginResponseDto>.Failure(ResultCode.RoleNotFound, "Rol bulunamadı");

        var accessToken = _tokenService.GenerateAccessToken(account, role.Name);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            AccountId = account.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpireDays),
            CreatedAt = DateTime.UtcNow
        };
        await _refreshTokenRepository.CreateAsync(refreshTokenEntity);

        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Role = role.Name,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes)
        };

        return Result<LoginResponseDto>.Success(response, "Giriş başarılı");
    }
}