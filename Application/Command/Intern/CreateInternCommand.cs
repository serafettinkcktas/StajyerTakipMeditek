namespace Application.Command.Intern;

public class CreateInternCommand
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string University { get; set; } = null!;
    public string Department { get; set; } = null!;
    public int Class { get; set; }
    public Guid? MentorId { get; set; }
    public DateTime? StartDate { get; set; }
}