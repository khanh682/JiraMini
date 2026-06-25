using JiraMini.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraMini.Infrastructure.Persistence.Configurations;

public class AttachmentConfiguration
    : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("attachments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.TaskId)
            .HasColumnName("task_id")
            .IsRequired();

        builder.Property(x => x.UploadedBy)
            .HasColumnName("uploaded_by")
            .IsRequired();

        builder.Property(x => x.FileName)
            .HasColumnName("file_name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.FilePath)
            .HasColumnName("file_path")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.UploadedAt)
            .HasColumnName("uploaded_at")
            .IsRequired();

        builder.HasOne<TaskItem>()
            .WithMany()
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UploadedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}