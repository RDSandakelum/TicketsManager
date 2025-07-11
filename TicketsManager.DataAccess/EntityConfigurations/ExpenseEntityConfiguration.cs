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
    internal class ExpenseEntityConfiguration : IEntityTypeConfiguration<ExpenseEntity>
    {
        public void Configure(EntityTypeBuilder<ExpenseEntity> builder)
        {
            builder.HasKey(e => e.ExpenseId);

            builder.Property(e => e.Title)
                .HasMaxLength(500)
                .IsRequired(true);

            builder.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(e => e.Amount)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Budget)
                .WithMany(b => b.BudgetExpenses)
                .HasForeignKey(e => e.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SubCategory)
                .WithMany()
                .HasForeignKey(e => e.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
