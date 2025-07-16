using TicketsManager.Common.Repository;

namespace TicketsManager.Common.Database;
public interface ITicketsManagerDbContext : IUserRepository, IBudgetAllocationRepository, IBudgetRepository, IBudgetTemplateRepository, ICategoryRepository, 
                                            IExpenseRepository, ISubCategoryRepository, IUnitOfWork
{
}
