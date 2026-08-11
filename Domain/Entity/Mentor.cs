namespace Domain.Entity;

public class Mentor
{
    public Mentor(Guid id, Guid accountId, Guid profileId)
    {
        Id = id;
        AccountId = accountId;
        ProfileId = profileId;
        IsDeleted = false;
        InternCount = 0;
    }

    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid ProfileId { get; set; }
    public int InternCount { get; set; } = 0;
    public bool IsDeleted { get; set; } = false;
}