using JiraMini.Domain.Entities;
using JiraMini.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using JiraMini.Application.Common.Interfaces;

namespace JiraMini.Infrastructure.Persistence;

public class AppDbContext : DbContext, IApplicationDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public DbSet<Comment> Comments => Set<Comment>();

    public DbSet<Attachment> Attachments => Set<Attachment>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
        var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var user1Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var user2Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var user3Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var user4Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                FullName = "Admin System",
                Email = "admin@jiramini.com",
                PasswordHash = "hashed_password_1",
                Role = UserRole.Admin,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 1), DateTimeKind.Utc)
            },
            new User
            {
                Id = user1Id,
                FullName = "Nguyen Van A",
                Email = "a@jiramini.com",
                PasswordHash = "hashed_password_2",
                Role = UserRole.User,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 2), DateTimeKind.Utc)
            },
            new User
            {
                Id = user2Id,
                FullName = "Tran Van B",
                Email = "b@jiramini.com",
                PasswordHash = "hashed_password_3",
                Role = UserRole.User,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 3), DateTimeKind.Utc)
            },
            new User
            {
                Id = user3Id,
                FullName = "Le Van C",
                Email = "c@jiramini.com",
                PasswordHash = "hashed_password_4",
                Role = UserRole.User,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 4), DateTimeKind.Utc)
            },
            new User
            {
                Id = user4Id,
                FullName = "Pham Van D",
                Email = "d@jiramini.com",
                PasswordHash = "hashed_password_5",
                Role = UserRole.User,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 5), DateTimeKind.Utc)
            }
        );
        var project1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var project2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var project3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
        var project4Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
        var project5Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                Id = project1Id,
                Name = "JiraMini Core",
                Description = "Core system development",
                OwnerId = adminId,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 10), DateTimeKind.Utc)
            },
            new Project
            {
                Id = project2Id,
                Name = "Authentication",
                Description = "Login and JWT",
                OwnerId = user1Id,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 11), DateTimeKind.Utc)
            },
            new Project
            {
                Id = project3Id,
                Name = "Task Management",
                Description = "Manage tasks",
                OwnerId = user2Id,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 12), DateTimeKind.Utc)
            },
            new Project
            {
                Id = project4Id,
                Name = "Notification",
                Description = "Email notifications",
                OwnerId = user3Id,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 13), DateTimeKind.Utc)
            },
            new Project
            {
                Id = project5Id,
                Name = "Reporting",
                Description = "Generate reports",
                OwnerId = user4Id,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 14), DateTimeKind.Utc)
            }
        );
        modelBuilder.Entity<TaskItem>().HasData(
    new TaskItem
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
        ProjectId = project1Id,
        AssigneeId = user1Id,
        Title = "Setup Clean Architecture",
        Description = "Create project structure",
        Status = TaskItemStatus.Todo,
        Priority = Priority.High,
        DueDate = DateTime.SpecifyKind(new DateTime(2026, 7, 1), DateTimeKind.Utc),
        CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 15), DateTimeKind.Utc)
    },
    new TaskItem
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
        ProjectId = project2Id,
        AssigneeId = user2Id,
        Title = "Implement JWT",
        Description = "Add authentication",
        Status = TaskItemStatus.InProgress,
        Priority = Priority.High,
        DueDate = DateTime.SpecifyKind(new DateTime(2026, 7, 2), DateTimeKind.Utc),
        CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 16), DateTimeKind.Utc)
    },
    new TaskItem
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
        ProjectId = project3Id,
        AssigneeId = user3Id,
        Title = "Create Task API",
        Description = "CRUD task endpoints",
        Status = TaskItemStatus.Done,
        Priority = Priority.Medium,
        DueDate = DateTime.SpecifyKind(new DateTime(2026, 7, 3), DateTimeKind.Utc),
        CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 17), DateTimeKind.Utc)
    },
    new TaskItem
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
        ProjectId = project4Id,
        AssigneeId = user4Id,
        Title = "Send Email",
        Description = "Notification service",
        Status = TaskItemStatus.Todo,
        Priority = Priority.Low,
        DueDate = DateTime.SpecifyKind(new DateTime(2026, 7, 4), DateTimeKind.Utc),
        CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 18), DateTimeKind.Utc)
    },
    new TaskItem
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
        ProjectId = project5Id,
        AssigneeId = adminId,
        Title = "Dashboard Report",
        Description = "Statistics page",
        Status = TaskItemStatus.InProgress,
        Priority = Priority.High,
        DueDate = DateTime.SpecifyKind(new DateTime(2026, 7, 5), DateTimeKind.Utc),
        CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 19), DateTimeKind.Utc)
    }
);
    }
} 