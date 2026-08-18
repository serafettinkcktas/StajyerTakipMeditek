using Application.DTOs.Auth;
using Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(
    LoginUseCase loginUseCase,
    RefreshTokenUseCase refreshTokenUseCase,
    LogoutUseCase logoutUseCase) : ControllerBase
{
    private readonly LoginUseCase _loginUseCase = loginUseCase;
    private readonly RefreshTokenUseCase _refreshTokenUseCase = refreshTokenUseCase;
    private readonly LogoutUseCase _logoutUseCase = logoutUseCase;

    /// <summary>
    /// Email ve şifre ile giriş yapar, access + refresh token döner.
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _loginUseCase.LoginAsync(request);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Geçerli bir refresh token ile yeni token çifti üretir.
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _refreshTokenUseCase.RefreshAsync(request.RefreshToken);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Refresh token'ı iptal ederek çıkış yapar.
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _logoutUseCase.LogoutAsync(request.RefreshToken);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}