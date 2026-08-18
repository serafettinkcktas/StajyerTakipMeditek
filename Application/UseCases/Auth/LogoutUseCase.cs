using Application.Common.Models;
using Domain.Interface;

namespace Application.UseCases.Auth;

public class LogoutUseCase(IRefreshTokenRepository refreshTokenRepository)
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task<Result> LogoutAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (storedToken is null)
            return Result.Failure(ResultCode.NotFound, "Refresh token bulunamadı");

        if (storedToken.RevokedAt is not null)
            return Result.Success("Token zaten iptal edilmiş");

        await _refreshTokenRepository.RevokeAsync(storedToken.Id);
        return Result.Success("Çıkış başarılı");
    }
}