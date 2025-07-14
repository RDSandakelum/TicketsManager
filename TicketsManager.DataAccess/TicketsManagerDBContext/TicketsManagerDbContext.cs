using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManager.Common.Entity;

namespace TicketsManager.DataAccess.TicketsManagerDBContext;
public class TicketsManagerDbContext : DbContext
{
    public TicketsManagerDbContext(DbContextOptions<TicketsManagerDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketsManagerDbContext).Assembly);
    }

    public DbSet<UserEntity> UserEntities { get; set; }
    public DbSet<BudgetTemplateEntity> BudgetTemplates { get; set; }
    public DbSet<BudgetEntity> Budgets { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<SubCategoryEntity> SubCategories { get; set; }
    public DbSet<BudgetAllocationEntity> BudgetAllocations { get; set; }
    public DbSet<ExpenseEntity> Expenses { get; set; }

}
