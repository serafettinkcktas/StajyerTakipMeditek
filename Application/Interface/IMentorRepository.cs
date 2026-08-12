using Application.DTOs.Mentor;

namespace Application.Interface;

public interface IMentorRepository
{
    Task<IEnumerable<MentorDto>> GetAllAsync();
}