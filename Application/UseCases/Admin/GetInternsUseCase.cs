using Application.Common.Models;
using Application.DTOs.Intern;
using Application.Interface;

namespace Application.UseCases.Admin;

public class GetInternsUseCase(IInternRepository internRepository)
{
    private readonly IInternRepository _internRepository = internRepository;

    /// <summary>
    /// Tüm aktif stajyerleri listeler.
    /// </summary>
    public async Task<Result<IEnumerable<InternDto>>> GetInternsAsync()
    {
        var interns = await _internRepository.GetAllAsync();
        return Result<IEnumerable<InternDto>>.Success(interns, "Stajyerler başarıyla listelendi");
    }
}