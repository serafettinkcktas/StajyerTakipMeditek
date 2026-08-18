using Application.DTOs.Intern;

namespace Application.Interface;

public interface IInternRepository
{
    Task<IEnumerable<InternDto>> GetAllAsync();
}