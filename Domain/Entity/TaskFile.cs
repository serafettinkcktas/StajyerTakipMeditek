namespace Domain.Entity;

public class TaskFile
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid FileId { get; set; }// gorevler icin dosya eklenebilir
}