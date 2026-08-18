namespace Application.DTOs.Intern;

public class InternDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? PhotoUrl { get; set; }
    public string University { get; set; } = null!;
    public string Department { get; set; } = null!;
    public int Class { get; set; }
    public Guid? MentorId { get; set; }
    public string? MentorName { get; set; }
    public string StatusName { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}