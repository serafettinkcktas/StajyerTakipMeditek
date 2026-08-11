namespace Domain.Entity;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; } // gecersiz sayildigi tarihi tutariz
    public DateTime CreatedAt { get; set; }
}