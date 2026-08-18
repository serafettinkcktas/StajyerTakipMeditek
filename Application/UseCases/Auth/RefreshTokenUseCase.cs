using Application.Common.Models;
using Application.DTOs.Auth;
using Application.Interface;
using Domain.Entity;
using Domain.Interface;

namespace Application.UseCases.Auth;

public class RefreshTokenUseCase(
    IRefreshTokenRepository refreshTokenRepository,
    IAccountRepository accountRepository,
    IRoleRepository roleRepository,
    ITokenService tokenService,
    JwtOptions jwtOptions)
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly JwtOptions _jwtOptions = jwtOptions;

    public async Task<Result<LoginResponseDto>> RefreshAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (storedToken is null)
            return Result<LoginResponseDto>.Failure(ResultCode.Unauthorized, "Geçersiz refresh token");

        if (storedToken.RevokedAt is not null)
            return Result<LoginResponseDto>.Failure(ResultCode.Unauthorized, "Refresh token iptal edilmiş");

        if (storedToken.ExpiresAt < DateTime.UtcNow)
            return Result<LoginResponseDto>.Failure(ResultCode.Unauthorized, "Refresh token süresi dolmuş");

        var account = await _accountRepository.GetByIdAsync(storedToken.AccountId);
        if (account is null)
            return Result<LoginResponseDto>.Failure(ResultCode.NotFound, "Kullanıcı bulunamadı");

        var role = await _roleRepository.GetRoleByIdAsync(account.RoleId);
        if (role is null)
            return Result<LoginResponseDto>.Failure(ResultCode.RoleNotFound, "Rol bulunamadı");

        var accessToken = _tokenService.GenerateAccessToken(account, role.Name);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        await _refreshTokenRepository.RevokeAsync(storedToken.Id);

        var newRefreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            AccountId = account.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpireDays),
            CreatedAt = DateTime.UtcNow
        };
        await _refreshTokenRepository.CreateAsync(newRefreshTokenEntity);

        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            Role = role.Name,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes)
        };

        return Result<LoginResponseDto>.Success(response, "Token yenilendi");
    }
}