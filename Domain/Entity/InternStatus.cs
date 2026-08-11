namespace Domain.Entity;

public class InternStatus
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; // Aktif, Tamamlandı, Ayrıldı
}