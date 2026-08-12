using Domain.Entity;

namespace Application.Common.Helpers;

public class UserProfileHelper
{
    /// <summary>
    /// Yeni bir kullanıcı profili oluşturur
    /// </summary>
    public UserProfile CreateUserProfile(Guid id, Guid accountId, string name, string surname, string email)
    {
        return new UserProfile(id, accountId, name, surname, email);
    }
}