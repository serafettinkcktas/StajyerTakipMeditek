namespace Domain.Entity;

public class PasswordResetCode
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string ResetCode { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;
}