using Domain.Entity;

namespace Application.Common.Helpers;

public class MentorHelper
{
    /// <summary>
    /// Yeni bir mentor oluşturur
    /// </summary>
    public Mentor CreateMentor(Guid id, Guid accountId, Guid profileId)
    {
        return new Mentor(id, accountId, profileId);
    }
}