using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketsManager.Common.Entity;
using TicketsManager.Common.Enum;

namespace TicketsManager.DataAccess.EntityConfigurations
{
    internal class SubCategoryEntityConfiguration : IEntityTypeConfiguration<SubCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<SubCategoryEntity> builder)
        {
            builder.HasKey(sc => sc.SubCategoryId);

            builder.Property(sc => sc.SubCategoryName)
                .HasMaxLength(500)
                .IsRequired(true);

            builder.Property(sc => sc.SubCategoryType)
                .HasConversion(
                v => v.ToString(),
                v => (CategoryType)Enum.Parse(typeof(CategoryType), v))
                .IsRequired(true);

            builder.HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(sc => sc.Expenses)
                .WithOne(e => e.SubCategory)
                .HasForeignKey(e => e.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
