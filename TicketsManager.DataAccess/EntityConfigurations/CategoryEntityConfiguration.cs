using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketsManager.Common.Entity;
using TicketsManager.Common.Enum;

namespace TicketsManager.DataAccess.EntityConfigurations
{
    internal class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.CategoryName)
                .HasMaxLength(500)
                .IsRequired(true);

            builder.Property(c => c.CategoryType)
                .HasConversion(
                    v => v.ToString(),
                    v => (CategoryType)Enum.Parse(typeof(CategoryType), v))
                .IsRequired(true);

            builder.HasMany(c => c.SubCategories)
                .WithOne(sc => sc.Category)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.BudgetTemplate)
                .WithMany(bt => bt.Categories)
                .HasForeignKey(c => c.BudgetTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
