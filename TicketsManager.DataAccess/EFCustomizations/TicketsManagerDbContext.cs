using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManager.Common.Entity;
using TicketsManager.Common.Database;

namespace TicketsManager.DataAccess.EFCustomizations;
public partial class TicketsManagerDbContext : DbContext, ITicketsManagerDbContext
{
    public TicketsManagerDbContext(DbContextOptions<TicketsManagerDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketsManagerDbContext).Assembly);
    }

    public Task<bool> SaveChangesAsync()
    {
        return Task.FromResult(true);
    }

    public DbSet<UserEntity> UserEntities { get; set; }
    public DbSet<BudgetTemplateEntity> BudgetTemplates { get; set; }
    public DbSet<BudgetEntity> Budgets { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<SubCategoryEntity> SubCategories { get; set; }
    public DbSet<BudgetAllocationEntity> BudgetAllocations { get; set; }
    public DbSet<ExpenseEntity> Expenses { get; set; }

}
