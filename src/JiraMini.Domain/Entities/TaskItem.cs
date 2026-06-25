using JiraMini.Domain.Enums;

namespace JiraMini.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public Guid AssigneeId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TaskItemStatus Status { get; set; }

    public Priority Priority { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime CreatedAt { get; set; }
}