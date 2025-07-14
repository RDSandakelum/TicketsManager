using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManager.Common.Entity;

namespace TicketsManager.DataAccess.EntityConfigurations
{
    internal class BudgetTemplateEntityConfiguration : IEntityTypeConfiguration<BudgetTemplateEntity>
    {
        public void Configure(EntityTypeBuilder<BudgetTemplateEntity> builder)
        {
            builder.HasKey(bt => bt.TemplateId);

            builder.Property(bt => bt.TemplateName)
                .HasMaxLength(500)
                .IsRequired(true);

            builder.HasMany(bt => bt.Categories)
                .WithOne(c => c.BudgetTemplate)
                .HasForeignKey(c => c.BudgetTemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bt => bt.User)
                .WithMany(u => u.BudgetTemplates)
                .HasForeignKey(bt => bt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
