namespace Domain.Entity;

public class TaskSubmission
{
    public Guid Id { get; set; }
    public Guid InternTaskId { get; set; }
    public string? Description { get; set; }
    public Guid? FileId { get; set; }
    public string? GithubUrl { get; set; }
    public string? DemoUrl { get; set; }
    public int RevisionCount { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}