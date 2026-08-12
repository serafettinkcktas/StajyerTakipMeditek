using Application.Common.Models;
using Application.DTOs.Mentor;
using Application.Interface;

namespace Application.UseCases.Admin;

public class GetMentorsUseCase(IMentorRepository mentorRepository)
{
    private readonly IMentorRepository _mentorRepository = mentorRepository;

    /// <summary>
    /// Tüm aktif mentorları listeler.
    /// </summary>
    public async Task<Result<IEnumerable<MentorDto>>> GetMentorsAsync()
    {
        var mentors = await _mentorRepository.GetAllAsync();
        return Result<IEnumerable<MentorDto>>.Success(mentors, "Mentorlar başarıyla listelendi");
    }
}