namespace Domain.Entity;

public class TaskStatusHistory
{
    public Guid Id { get; set; }
    public Guid InternTaskId { get; set; }
    public Guid OldStatusId { get; set; }
    public Guid NewStatusId { get; set; }
    public Guid ChangedById { get; set; } // Accountid kullanabiliriz
    public DateTime ChangedAt { get; set; }
}