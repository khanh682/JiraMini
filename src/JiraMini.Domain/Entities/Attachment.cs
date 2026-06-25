namespace JiraMini.Domain.Entities;

public class Attachment
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public Guid UploadedBy { get; set; }

    public DateTime UploadedAt { get; set; }
}