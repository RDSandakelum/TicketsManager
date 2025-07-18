using Microsoft.EntityFrameworkCore;
using TicketsManager.Common.Entity;

namespace TicketsManager.Common.Repository;
public interface IBudgetRepository
{
    DbSet<BudgetEntity> Budgets { get; set; }
}
