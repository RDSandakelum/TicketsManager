using Microsoft.EntityFrameworkCore;
using TicketsManager.Common.Entity;

namespace TicketsManager.Common.Repository;
public interface IBudgetAllocationRepository
{
    DbSet<BudgetAllocationEntity> BudgetAllocations { get; set; }
}
