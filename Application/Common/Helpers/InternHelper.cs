using Domain.Entity;

namespace Application.Common.Helpers;

public class InternHelper
{
    /// <summary>
    /// Yeni bir stajyer oluşturur
    /// </summary>
    public Intern CreateIntern(
        Guid id,
        Guid accountId,
        Guid profileId,
        Guid? mentorId,
        string university,
        string department,
        int classLevel,
        DateTime? startDate,
        Guid statusId)
    {
        return new Intern
        {
            Id = id,
            AccountId = accountId,
            ProfileId = profileId,
            MentorId = mentorId,
            University = university,
            Department = department,
            Class = classLevel,
            StartDate = startDate,
            EndDate = null,
            StatusId = statusId
        };
    }
}