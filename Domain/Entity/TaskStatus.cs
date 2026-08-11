namespace Domain.Entity;

public class TaskStatus
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    // Oluşturuldu, Atandı, Devam Ediyor, Teslim Edildi, İnceleniyor, Revizyon, Tamamlandı
}