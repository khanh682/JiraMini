using JiraMini.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraMini.Infrastructure.Persistence.Configurations;

public class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(x => x.Role)
            .HasColumnName("role");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}