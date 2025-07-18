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
    public TicketsManagerDbContext(DbContextOptions<TicketsManagerDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketsManagerDbContext).Assembly);
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await base.SaveChangesAsync();
        }
        catch
        {
            //Log the exception or handle it as needed
            throw;
        }
        
    }

    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<BudgetTemplateEntity> BudgetTemplates { get; set; }
    public virtual DbSet<BudgetEntity> Budgets { get; set; }
    public virtual DbSet<CategoryEntity> Categories { get; set; }
    public virtual DbSet<SubCategoryEntity> SubCategories { get; set; }
    public virtual DbSet<BudgetAllocationEntity> BudgetAllocations { get; set; }
    public virtual DbSet<ExpenseEntity> Expenses { get; set; }
}
