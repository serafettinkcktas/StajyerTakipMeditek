using Domain.Entity;

namespace Domain.Interface;

public interface IInternStatusRepository : IRepository<InternStatus>
{
    Task<InternStatus?> GetByNameAsync(string name);
    Task<bool> CreateStatusAsync(InternStatus status);
}
