namespace Domain.Entity;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    // Admin, Mentor, Stajyer suanlik mvp icin bunlar vardi
}