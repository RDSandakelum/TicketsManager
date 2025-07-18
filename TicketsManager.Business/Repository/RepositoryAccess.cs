using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketsManager.Common.Database;
using TicketsManager.Common.Repository;

namespace TicketsManager.Business.Repository;
public class RepositoryAccess
{
    public RepositoryAccess(ITicketsManagerDbContext ticketsManagerDbContext)
    {
        dbContext = ticketsManagerDbContext;

        budgetAllocationRepository = dbContext;
        budgetRepository = dbContext;
        budgetTemplateRepository = dbContext;
        categoryRepository = dbContext;
        expenseRepository = dbContext;
        subCategoryRepository = dbContext;
        userRepository = dbContext;

        unitOfWork = dbContext;
    }

    protected readonly ITicketsManagerDbContext dbContext;

    protected readonly IBudgetAllocationRepository budgetAllocationRepository;
    protected readonly IBudgetRepository budgetRepository;
    protected readonly IBudgetTemplateRepository budgetTemplateRepository;
    protected readonly ICategoryRepository categoryRepository;
    protected readonly IExpenseRepository expenseRepository;
    protected readonly ISubCategoryRepository subCategoryRepository;
    protected readonly IUserRepository userRepository;

    protected readonly IUnitOfWork unitOfWork;
}
