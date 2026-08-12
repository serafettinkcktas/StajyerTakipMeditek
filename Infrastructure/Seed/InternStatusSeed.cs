using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Seed;

public class InternStatusSeed(IInternStatusRepository internStatusRepository)
{
    private readonly IInternStatusRepository _internStatusRepository = internStatusRepository;

    private static readonly string[] DefaultStatuses = { "Aktif", "Tamamlandı", "Ayrıldı" };

    /// <summary>
    /// Uygulama ayağa kalkarken stajyer durumlarını kontrol eder, yoksa ekler.
    /// </summary>
    public async Task SeedAsync()
    {
        foreach (var statusName in DefaultStatuses)
        {
            var exists = await _internStatusRepository.GetByNameAsync(statusName);
            if (exists is null)
            {
                var status = new InternStatus
                {
                    Id = Guid.NewGuid(),
                    Name = statusName
                };
                await _internStatusRepository.CreateStatusAsync(status);
            }
        }
    }
}