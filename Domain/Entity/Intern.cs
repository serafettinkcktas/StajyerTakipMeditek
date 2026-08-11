namespace Domain.Entity;

public class Intern
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid ProfileId { get; set; }
    public Guid? MentorId { get; set; }
    public string University { get; set; } = null!;
    public string Department { get; set; } = null!;
    public int Class { get; set; }
    public DateTime? StartDate { get; set; } // zorunlu kilmadim baslangictan once bir atama yapilabilir olsun diye
    public DateTime? EndDate { get; set; }
    public Guid StatusId { get; set; }
}