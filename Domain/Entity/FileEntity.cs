namespace Domain.Entity;

public class FileEntity
{
    // TODO : Isimler cakismasin diye saklarken dosyaya guid veya o tarz rastgele bir isim veririz
    public Guid Id { get; set; }
    public string RealName { get; set; } = null!;
    public string StoredName { get; set; } = null!;
    public Guid TypeId { get; set; } // filetype tablosundan cekeriz
    public string? Extension { get; set; } // uzantilara ihtiyac duysarsak diye ekledim
    public long FileSize { get; set; }
    public string FilePath { get; set; } = null!;
    public string? Description { get; set; }
    public Guid CreatedByAccountId { get; set; }
    public DateTime CreatedAt { get; set; }

}