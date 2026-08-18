namespace Application.DTOs.Auth;

public class LoginResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string Role { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}