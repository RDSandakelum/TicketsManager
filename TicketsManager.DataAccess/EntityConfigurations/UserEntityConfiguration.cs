using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketsManager.Common.Entity;

namespace TicketsManager.DataAccess.EntityConfigurations;
internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.FirstName)
            .HasMaxLength(250)
            .IsRequired(true);

        builder.Property(u => u.LastName)
            .HasMaxLength(250);

        builder.Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(u => u.NormalizedUsername)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.HasIndex(u => u.NormalizedUsername)
            .IsUnique();

        builder.Property(u => u.Email)
            .HasMaxLength(200)
            .IsRequired(true);

        builder.Property(u => u.NormalizedEmail)
            .HasMaxLength(200)
            .IsRequired(true);

        builder.HasIndex(u => u.NormalizedEmail)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(500)
            .IsRequired(true);

        builder.Property(u => u.PasswordSalt)
            .HasMaxLength(500)
            .IsRequired(true);

        builder.HasMany(u => u.BudgetTemplates)
            .WithOne(bt => bt.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Budgets)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.Expenses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
