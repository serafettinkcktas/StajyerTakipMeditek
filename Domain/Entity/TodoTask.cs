namespace Domain.Entity;

public class TodoTask // tasks
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid CreatedByMentorId { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid StatusId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}