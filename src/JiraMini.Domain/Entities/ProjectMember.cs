using JiraMini.Domain.Enums;

namespace JiraMini.Domain.Entities;

public class ProjectMember
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public Guid UserId { get; set; }

    public ProjectRole Role { get; set; }

    public DateTime JoinedAt { get; set; }
}