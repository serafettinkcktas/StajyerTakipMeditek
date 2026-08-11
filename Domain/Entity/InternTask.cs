namespace Domain.Entity;

public class InternTask
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid InternId { get; set; }
    public Guid StatusId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}