using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketsManager.Common.Entity;

namespace TicketsManager.DataAccess.EntityConfigurations
{
    internal class BudgetAllocationEntityConfiguration : IEntityTypeConfiguration<BudgetAllocationEntity>
    {
        public void Configure(EntityTypeBuilder<BudgetAllocationEntity> builder)
        {
            builder.HasKey(ba => ba.AllocationId);

            builder.Property(ba => ba.Amount)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(ba => ba.Budget)
                .WithMany(b => b.BudgetAllocations)
                .HasForeignKey(ba => ba.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ba => ba.Category)
                .WithMany()
                .HasForeignKey(ba => ba.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ba => ba.SubCategory)
                .WithOne()
                .HasForeignKey<BudgetAllocationEntity>(ba => ba.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
