using Domain.Entity;

namespace Application.Common.Helpers;

public class AccountHelper
{
    /// <summary>
    /// Yeni bir account oluşturur
    /// </summary>
    public Account CreateAccount(Guid id, string email, string passwordHash, Guid roleId)
    {
        return new Account(id, email, passwordHash, roleId);
    }
}