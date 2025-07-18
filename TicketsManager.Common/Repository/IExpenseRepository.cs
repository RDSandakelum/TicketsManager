using Microsoft.EntityFrameworkCore;
using TicketsManager.Common.Entity;

namespace TicketsManager.Common.Repository;
public interface IExpenseRepository
{
    DbSet<ExpenseEntity> Expenses { get; set; }
}
