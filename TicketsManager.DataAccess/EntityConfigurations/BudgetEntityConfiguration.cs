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
    internal class BudgetEntityConfiguration : IEntityTypeConfiguration<BudgetEntity>
    {
        public void Configure(EntityTypeBuilder<BudgetEntity> builder)
        {
            builder.HasKey(b => b.BudgetId);

            builder.Property(b => b.BudgetName)
                .HasMaxLength(500)
                .IsRequired(true);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Budgets)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.BudgetTemplate)
                .WithMany(bt => bt.Budgets)
                .HasForeignKey(b => b.BudgetTemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.BudgetAllocations)
                .WithOne(ba => ba.Budget)
                .HasForeignKey(ba => ba.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.BudgetExpenses)
                .WithOne(e => e.Budget)
                .HasForeignKey(e => e.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
