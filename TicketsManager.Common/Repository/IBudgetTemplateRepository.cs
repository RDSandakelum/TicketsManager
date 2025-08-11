using Microsoft.EntityFrameworkCore;
using TicketsManager.Common.Entity;

namespace TicketsManager.Common.Repository;
public interface IBudgetTemplateRepository
{
    DbSet<BudgetTemplateEntity> BudgetTemplates { get; set; }
}
