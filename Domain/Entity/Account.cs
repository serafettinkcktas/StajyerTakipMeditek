namespace Domain.Entity;

public class Account
{
    public Account(Guid id, string email, string password, Guid roleId)
    {
        Id = id;
        Email = email;
        Password = password;
        RoleId = roleId;
        IsDeleted = false;
    }

    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; // hash olarak tutulur
    public Guid RoleId { get; set; } // stajyer , admin , mentor
    public bool IsDeleted { get; set; } = false;
}